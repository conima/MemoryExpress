namespace Tests.MessageService
{
    /// <summary>
    /// A request from a client
    /// </summary>
    public interface IServiceRequest
    {
        /// <summary>
        /// Respond to a request. Response objects passed here will represent
        /// the response from the service.
        /// </summary>
        /// <param name="response">The response object to send back.</param>
        void Respond(IServiceResponse response);
    }
}