namespace Tests.MessageService
{
    /// <summary>
    /// A response to indicate a failure
    /// </summary>
    public class ErrorResponse : IServiceResponse
    {
        public bool Success { get { return false; } }

        /// <summary>
        /// The message to accompany the error
        /// </summary>
        public string Message { get; set; }
    }
}