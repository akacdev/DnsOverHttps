using System;

namespace DnsOverHttps
{
    internal class Constants
    {
        /// <summary>
        /// The hostname of the Dns-over-HTTPS resolver.
        /// </summary>
        public const string Hostname = "1.1.1.1";
        /// <summary>
        /// The base URI to send requests to. 
        /// </summary>
        public static readonly Uri BaseUri = new($"https://{Hostname}/dns-query");
        /// <summary>
        /// The preferred HTTP request version to use.
        /// </summary>
        public static readonly Version HttpVersion = new(2, 0);
        /// <summary>
        /// The <c>User-Agent</c> header value to send along requests.
        /// </summary>
        public const string UserAgent = "C# DnsOverHttps Client - actually-akac/DnsOverHttps";
        /// <summary>
        /// The <c>Accept</c> header value to send along requests.
        /// </summary>
        public const string ContentType = "application/dns-json";
        /// <summary>
        /// The maximum string length when displaying a preview of a response body.
        /// </summary>
        public const int PreviewMaxLength = 500;
    }
}