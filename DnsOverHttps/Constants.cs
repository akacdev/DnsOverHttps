using System;

namespace DnsOverHttps
{
    internal class Constants
    {
        public const string Hostname = "1.1.1.1";
        public static readonly Uri BaseUri = new($"https://{Hostname}/dns-query");
        public static readonly Version HttpVersion = new(2, 0);

        public const string UserAgent = "C# DnsOverHttps Client - actually-akac/DnsOverHttps";
        public const string ContentType = "application/dns-json";
        public const int MaxPreviewLength = 500;
    }
}