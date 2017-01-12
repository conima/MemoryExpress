namespace Tests.MessageService
{
    /// <summary>
    /// A request to clear a message box.
    /// </summary>
    public class ClearMessagesRequest : ServiceRequest
    {
        /// <summary>
        /// The inbox to clear
        /// </summary>
        public string Inbox { get; set; }
    }
}