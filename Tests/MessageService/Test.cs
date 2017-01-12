using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Framework;
using Framework.MessageService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.MessageService
{
    /// <summary>
    /// This test will test the behavior of the Service class in this namespace.
    /// </summary>
    [TestClass]
    public class Test
    {
        public const string FirstMessage = "This is the first message.";
        public const string SecondMessage = "This is the second message.";

        /// <summary>
        /// When a request for messages is made to an empty inbox
        /// the system should respond with an ErrorResponse and
        /// with the message being ErrorMessages.GetMessagesNoInbox
        /// </summary>
        [TestMethod]
        public void Service_GetMessages_EmptyInbox_RespondsFailure()
        {
            var service = GetService();
            var request = new GetMessagesRequest()
                {
                    Inbox = "Bob"
                };

            var response = MakeRequest<ErrorResponse>(service, request);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(ErrorMessages.GetMessagesNoInbox, response.Message, "The message returned was not ErrorMessages.GetMessagesNoInbox");
        }

        /// <summary>
        /// When a request for message is made to a populated inbox
        /// the system should return a MessagesResponse with the messages
        /// in the same order they appeared in the Inbox
        /// </summary>
        [TestMethod]
        public void Service_GetMessages_PopulatedInbox_RespondsWithMessages()
        {
            var service = GetService(new Dictionary<string, IList<string>>
                {
                    {"Bob", new [] { FirstMessage, SecondMessage }.ToList()}
                });

            var request = new GetMessagesRequest()
            {
                Inbox = "Bob"
            };

            var response = MakeRequest<MessagesResponse>(service, request);

            // Right Messages
            Assert.AreEqual(2, response.Messages.Count());
            Assert.AreEqual(FirstMessage, response.Messages.First());
            Assert.AreEqual(SecondMessage, response.Messages.Skip(1).First());
        }

        /// <summary>
        /// When a request to clear an inbox is made, and the inbox doesn't
        /// exist then the service should respond with an ErrorResponse
        /// with the message being set to ErrorMessages.ClearMessagesNoInbox
        /// </summary>
        [TestMethod]
        public void Service_ClearMessages_EmptyInbox_RespondsFailure()
        {
            var service = GetService();
            var request = new ClearMessagesRequest()
            {
                Inbox = "Bob"
            };

            var response = MakeRequest<ErrorResponse>(service, request);

            Assert.IsFalse(response.Success);
            Assert.AreEqual(ErrorMessages.ClearMessagesNoInbox, response.Message, "The message returned was not ErrorMessages.ClearMessagesNoInbox");
        }

        /// <summary>
        /// When a request to clear an inbox is made, and the inbox exists
        /// then the service should respond with an OkResponse and delete
        /// the inbox entirely.
        /// </summary>
        [TestMethod]
        public void Service_ClearMessages_PopulatedInbox_RespondsWithOk_ClearsMessages()
        {
            var service = GetService(new Dictionary<string, IList<string>>
                {
                    {"Bob", new [] { FirstMessage, SecondMessage }.ToList()}
                });

            var request = new ClearMessagesRequest()
            {
                Inbox = "Bob"
            };

            var response = MakeRequest<OkResponse>(service, request);

            Assert.IsFalse(service.MessageStore.ContainsKey("Bob"), "Bob's mailbox was not removed. .Clear() on the inbox is not enough, it should be removed entirely.");
        }

        /// <summary>
        /// When a request to deliver a message is sent and the inbox 
        /// specified does not exist the inbox should be created, the
        /// message delivered. Once complete an OkResponse should be
        /// sent.
        /// </summary>
        [TestMethod]
        public void Service_DeliverMessage_EmptyInbox_RespondsWithOk_CreatesInbox_DeliversMessage()
        {
            var service = GetService();
            var request = new DeliverMessageRequest()
            {
                Inbox = "Bob",
                Message = FirstMessage
            };

            var response = MakeRequest<OkResponse>(service, request);

            Assert.IsTrue(service.MessageStore.ContainsKey("Bob"), "Bob's mailbox was not created.");
            Assert.AreEqual(1, service.MessageStore["Bob"].Count);
            Assert.AreEqual(FirstMessage, service.MessageStore["Bob"].First());
        }

        /// <summary>
        /// When a request to deliver a message is sent and the inbox
        /// specified does exist the message should be added to the end
        /// of the inbox (list). Once complete an OkResponse should be
        /// sent.
        /// </summary>
        [TestMethod]
        public void Service_DeliverMessage_PopulatedInbox_RespondsWithOk_DeliversMessage()
        {
            var service = GetService(new Dictionary<string, IList<string>>
                {
                    {"Bob", new[] { FirstMessage }.ToList()}
                });

            var request = new DeliverMessageRequest()
            {
                Inbox = "Bob",
                Message = SecondMessage
            };

            var response = MakeRequest<OkResponse>(service, request);

            Assert.IsTrue(service.MessageStore.ContainsKey("Bob"), "Bob's mailbox was not created.");
            Assert.AreEqual(2, service.MessageStore["Bob"].Count);
            Assert.AreEqual(FirstMessage, service.MessageStore["Bob"].First(), "The initial message was modified.");
            Assert.AreEqual(SecondMessage, service.MessageStore["Bob"].Skip(1).First(), "The added message was not where we expect it to be.");
        }

        public class UnknownRequest : ServiceRequest
        {
            // Stub
        }

        /// <summary>
        /// When an unknown request is sent, the service should respond 
        /// with an ErrorResponse and no changes to the inboxes should
        /// occur.
        /// </summary>
        [TestMethod]
        public void Service_UnknownRequest_RespondsFailure()
        {
            var service = GetService(new Dictionary<string, IList<string>>
            {
                {"Bob", new [] { FirstMessage }.ToList()}
            });

            var response = MakeRequest<ErrorResponse>(service, new UnknownRequest());
            
            Console.WriteLine(response.Message);

            Assert.IsTrue(service.MessageStore.ContainsKey("Bob"), "The inbox was removed on an unknown command.");
            Assert.AreEqual(1, service.MessageStore.Count, "A new inbox was created on an unknown command.");
            Assert.AreEqual(1, service.MessageStore["Bob"].Count, "A new message was added to an inbox on an unknown command.");
            Assert.AreEqual(FirstMessage, service.MessageStore["Bob"].First(), "A message was modified/moved on an unknown command.");
        }


        /// <summary>
        /// The service should be designed to accept requests from multiple 
        /// threads at the same time. When two messages come in roughly
        /// at the same time, they should both make it into the inbox.
        /// </summary>
        [TestMethod]
        public void Service_MultiThread_DeliverMessage()
        {
            var service = GetService();

            var barrier = new Barrier(2);
            Func<string, string, IServiceResponse> worker =
                (nm, msg) =>
                    {
                        ThreadingHelper.WriteThreadLine("Waiting for Barrier to Catch Up.");
                        barrier.SignalAndWait();
                        ThreadingHelper.WriteThreadLine("Barrier Release, Continuing.");

                        return MakeRequest(service, new DeliverMessageRequest {Inbox = "Bob", Message = msg});
                    };

            var client1 = Task.Factory.StartNew(() => worker("C1", FirstMessage));
            var client2 = Task.Factory.StartNew(() => worker("C2", SecondMessage));

            Assert.IsTrue(client1.Wait(TimeSpan.FromSeconds(10)), "Client 1 Timed out, deadlock detected.");
            Assert.IsTrue(client2.Wait(TimeSpan.FromSeconds(10)), "Client 2 Timed out, deadlock detected.");

            Assert.IsTrue(service.MessageStore.ContainsKey("Bob"), "Inbox 'Bob' was not created.");
            Assert.AreEqual(2, service.MessageStore["Bob"].Count, "Inbox 'Bob' should have 2 messages.");

            Assert.IsTrue(service.MessageStore["Bob"].Any(x => x == FirstMessage), "First Message was not found.");
            Assert.IsTrue(service.MessageStore["Bob"].Any(x => x == SecondMessage), "Second Message was not found.");
        }

        /// <summary>
        /// The service should be designed to accept requests from multiple 
        /// threads at the same time. When two clear requests come in roughly
        /// at the same time, the service should be able to clear the
        /// inbox without throwing an exception. (Response is not tested here,
        /// just the result)
        /// </summary>
        [TestMethod]
        public void Service_MultiThread_ClearMessages()
        {
            var service = GetService(new Dictionary<string, IList<string>>
                {
                    {"Bob", new[] { "Message" }.ToList()}
                });

            var barrier = new Barrier(2);
            Func<string, string, IServiceResponse> worker =
                (nm, msg) =>
                {
                    ThreadingHelper.WriteThreadLine("Waiting for Barrier to Catch Up.");
                    barrier.SignalAndWait();
                    ThreadingHelper.WriteThreadLine("Barrier Release, Continuing.");

                    return MakeRequest(service, new ClearMessagesRequest() { Inbox = "Bob" });
                };

            var client1 = Task.Factory.StartNew(() => worker("C1", FirstMessage));
            var client2 = Task.Factory.StartNew(() => worker("C2", SecondMessage));

            Assert.IsTrue(client1.Wait(TimeSpan.FromSeconds(10)), "Client 1 Timed out, deadlock detected.");
            Assert.IsTrue(client2.Wait(TimeSpan.FromSeconds(10)), "Client 2 Timed out, deadlock detected.");

            Assert.IsFalse(service.MessageStore.ContainsKey("Bob"), "Inbox 'Bob' was not cleared.");
        }

        /// <summary>
        /// The service should be designed to accept requests from multiple 
        /// threads at the same time. When a clear request comes in, followed
        /// by a get messages request (before the clear is complete) the system
        /// should return that the inbox is empty.
        /// 
        /// ClearMessages -|--Working--|-------
        /// GetMessages   -------|--Working--|-
        /// </summary>
        [TestMethod]
        public void Service_MultiThread_GetClearMessages()
        {
            var service = GetService(new Dictionary<string, IList<string>>
                {
                    {"Bob", new[] {FirstMessage}.ToList()}
                });

            var barrier = new Barrier(2);
            Action waitForBarrier = barrier.SignalAndWait;

            var client1 =
                Task.Factory
                    .StartNew(waitForBarrier)
                    .ContinueWith(x =>
                        {
                            return MakeRequest(service, new ClearMessagesRequest() {Inbox = "Bob"});
                        });
            var client2 =
                Task.Factory
                    .StartNew(waitForBarrier)
                    .ContinueWith(x =>
                        {
                            var waitHandle = Task.Delay(20);
                            waitHandle.Start();
                            waitHandle.Wait();
                        })
                    .ContinueWith(x => MakeRequest(service, new GetMessagesRequest() {Inbox = "Bob"}));

            Assert.IsTrue(client1.Wait(TimeSpan.FromSeconds(10)), "Client 1 Timed out, deadlock detected.");
            Assert.IsTrue(client2.Wait(TimeSpan.FromSeconds(10)), "Client 2 Timed out, deadlock detected.");

            Assert.IsInstanceOfType(client1.Result, typeof (OkResponse), "Was expecting an OkResponse from the Clear Messages request.");
            Assert.IsInstanceOfType(client2.Result, typeof (ErrorResponse), "Was expecting an ErrorResponse from the ");
        }

        #region Test Utility Methods

        /// <summary>
        /// Gets the service to test, optionally with a provided initial message state.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        private Service GetService(IDictionary<string, IList<string>> messages = null)
        {
            var messageStore =
                messages != null
                    ? new InboxMessageStore(messages)
                    : new InboxMessageStore();

            return InstanceProvider.GetInstance<Service>(() => new Service(messageStore));
        }

        /// <summary>
        /// Performs a request, bootstraps the response callback so we can return
        /// it for testing. Asserts that a response was required.
        /// </summary>
        /// <param name="service">The service to execute against.</param>
        /// <param name="request">The request to make.</param>
        /// <returns>The response from the request.</returns>
        private IServiceResponse MakeRequest(Service service, ServiceRequest request)
        {
            bool wasCalled = false;
            IServiceResponse response = null;
            request.OnRespond = (r) =>
                {
                    response = r;
                    wasCalled = true;
                };

            service.HandleRequest(request);

            Assert.IsTrue(
                wasCalled,
                "A response is expected. You must call .Respond(...) on the provided " +
                "request and pass it the response object that relates.");

            Assert.IsNotNull(
                response,
                "The call to handle request did not send a response.");

            return response;
        }

        /// <summary>
        /// Performs a request, bootstraps the response callback so we can return 
        /// it for testing. Requires a specific type of response and asserts
        /// the returned value is of the matching type.
        /// </summary>
        /// <param name="service">The service to execute against.</param>
        /// <param name="request">The request to make.</param>
        /// <returns>The response from the request.</returns>
        private TExpectedResponse MakeRequest<TExpectedResponse>(Service service, ServiceRequest request)
            where TExpectedResponse : IServiceResponse
        {
            var response = MakeRequest(service, request);

            Assert.IsInstanceOfType(
                response,
                typeof (TExpectedResponse),
                "The call to handle request did not return the expected response type.");

            return (TExpectedResponse) response;
        }

        #endregion
    }
}
