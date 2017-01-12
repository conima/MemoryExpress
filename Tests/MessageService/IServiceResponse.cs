namespace Tests.MessageService
{
    /// <summary>
    /// A response to a client
    /// </summary>
    public interface IServiceResponse
    {
        /// <summary>
        /// Success Indicator
        /// </summary>
        bool Success { get; }
    }
}