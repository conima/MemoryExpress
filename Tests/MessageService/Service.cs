using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework;

namespace Tests.MessageService
{
    /// <summary>
    /// In this test/class we are simulating a message service for a group of people. This 
    /// service is responsible for storing user's messages so they can retreive them. Additionally
    /// it'll handle delivering new messages to user's inboxes and clearing the inbox when
    /// the user wants to clean up.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// The place to store the messages. The key of the dictionary represents 
        /// the inbox name, and the list inside is the messages in the order they
        /// were received.
        /// 
        /// This collection is provided for you in the constructor of the class
        /// and you should work with the instance provided. It is designed to 
        /// simulate some kind of external storage where there's a processing /
        /// communication delay so bare that in mind when using it.
        /// </summary>
        public IDictionary<string, IList<string>> MessageStore { get; private set; }

        /// <summary>
        /// Construct the Service
        /// </summary>
        /// <param name="messageStore">The initial message store to operate on.</param>
        public Service(IDictionary<string, IList<string>> messageStore)
        {
            if (messageStore == null) throw new ArgumentNullException("messageStore");
            MessageStore = messageStore;
        }

        /// <summary>
        /// Handle a request from a client. This method may be called from many threads
        /// at the same time so it should  be written in a thread-safe manner.
        /// 
        /// This method will be passed a class representing a request with all the appropriate
        /// data on it. 
        /// 
        /// Once complete a response will be returned by calling .Respond(...) on
        /// the request.
        /// 
        /// The valid requests are:
        /// 
        ///  - GetMessagesRequest - Get the messages from someone's inbox
        ///      ON Success]             Respond With MessagesResponse with .Messages populated
        ///      ON Inbox Doesn't Exist] Respond With ErrorResponse with .Message = ErrorMessages.GetMessagesNoInbox
        ///      ON Unexpected Failure]  Respond With ErrorResponse with .Message = Description of the fault
        ///  - ClearMessagesRequest - Clear the messages from someone's inbox
        ///      ON Success]             Remove inbox entirely, Respond With OkResponse
        ///      ON Inbox Dosen't Exist] Respond With ErrorResponse with .Message = ErrorMessages.ClearMessagesNoInbox
        ///      ON Unexpected Failure]  Respond With ErrorResponse with .Message = Description of the fault
        ///  - DeliverMessageRequest - Deliver a message to someone's inbox
        ///      ON Inbox Exists]        Add message to the end of the inbox, Return OkResponse
        ///      ON Inbox Dosen't Exist] Create Inbox, Add message to the end of the inbox, Return OkResponse
        ///      ON Unexpected Failure]  Respond With ErrorResponse with .Message = Description of the fault
        /// 
        /// If an unknown request comes in, respond with an ErrorResponse.
        /// </summary>
        /// <param name="request">The request to process.</param>
        public virtual void HandleRequest(IServiceRequest request)
        {
            throw new TestUnfinishedException();
        }
    }
}
