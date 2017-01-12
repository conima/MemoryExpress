using System.Collections.Generic;

namespace Tests.MessageService
{
    /// <summary>
    /// A response to provide the requested messages
    /// </summary>
    public class MessagesResponse : OkResponse
    {
        /// <summary>
        /// The messages from the inbox
        /// </summary>
        public IEnumerable<string> Messages { get; set; }
    }
}