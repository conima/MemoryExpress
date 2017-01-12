namespace Tests.MessageService
{
    /// <summary>
    /// A request to deliver a message to an inbox
    /// </summary>
    public class DeliverMessageRequest : ServiceRequest
    {
        /// <summary>
        /// The inbox to deliver the message to.
        /// </summary>
        public string Inbox { get; set; }

        /// <summary>
        /// The message to deliver.
        /// </summary>
        public string Message { get; set; }
    }
}