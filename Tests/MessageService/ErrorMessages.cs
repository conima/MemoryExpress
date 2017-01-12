using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.MessageService
{
    /// <summary>
    /// Constant Error messages to respond with.
    /// </summary>
    public class ErrorMessages
    {
        /// <summary>
        /// The error message to return when a ClearInboxRequest is sent 
        /// and the target inbox is not available.
        /// </summary>
        public const string ClearMessagesNoInbox = "The Inbox did not exist, and couldn't be cleared.";

        /// <summary>
        /// The error message to return when a GetMessagesRequest is sent
        /// and the target inbox is not available.
        /// </summary>
        public const string GetMessagesNoInbox = "The Inbox did not exist, messages cannot be returned.";
    }
}
