namespace Tests.MessageService
{
    /// <summary>
    /// A request to get all the messages from an inbox
    /// </summary>
    public class GetMessagesRequest : ServiceRequest
    {
        /// <summary>
        /// The inbox to retrieve messages from
        /// </summary>
        public string Inbox { get; set; }
    }
}