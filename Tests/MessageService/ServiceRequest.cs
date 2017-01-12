using System;

namespace Tests.MessageService
{
    /// <summary>
    /// A base class for service requests that we will use to test
    /// the behavior of the service.
    /// </summary>
    public abstract class ServiceRequest : IServiceRequest
    {
        /// <summary>
        /// We'll use a delegate we set up after the fact to 
        /// optionally handle responses, this aleviates the 
        /// need to take the response delegate in the
        /// constructor for test purposes.
        /// </summary>
        public Action<IServiceResponse> OnRespond { get; set; }

        public void Respond(IServiceResponse response)
        {
            if (OnRespond != null)
                OnRespond(response);
        }
    }
}