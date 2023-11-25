using System;

namespace DnsOverHttps
{
    /// <summary>
    /// An exception specific to DNS over HTTPS. You can access the exception's properties to get the context for the exception.
    /// </summary>
    public class DnsOverHttpsException : Exception
    {
        /// <summary>
        /// The DNS response that caused this exception.
        /// </summary>
        public Response Response { get; set; }

        public DnsOverHttpsException(string message) : base(message) { }
        public DnsOverHttpsException(string message, Response res) : base(message) { Response = res; }
    }
}