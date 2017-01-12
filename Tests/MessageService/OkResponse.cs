namespace Tests.MessageService
{
    /// <summary>
    /// An empty response to indicate success
    /// </summary>
    public class OkResponse : IServiceResponse
    {
        public bool Success { get { return true; } }
    }
}