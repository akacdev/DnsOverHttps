using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DnsOverHttps
{
    /// <summary>
    /// The primary class for sending DNS over HTTPS queries.
    /// </summary>
    public class DnsOverHttpsClient
    {
        private readonly HttpClient Client = new()
        {
            BaseAddress = Constants.BaseUri,
            DefaultRequestVersion = Constants.HttpVersion
        };

        /// <summary>
        /// Create a new instance of the DNS over HTTPS client.
        /// </summary>
        public DnsOverHttpsClient()
        {
            Client.DefaultRequestHeaders.UserAgent.ParseAdd(Constants.UserAgent);
            Client.DefaultRequestHeaders.Accept.ParseAdd(Constants.ContentType);
        }

        /// <summary>
        /// Resolve a name using Cloudflare's DNS over HTTPS. This helper method returns the first answer of a provided type.
        /// <para>
        ///     Alternatively, <see cref="Resolve"/> may be used to get full control over the response.
        /// </para>
        /// </summary>
        /// <param name="name">The FQDN to resolve. Example: <c>foo.bar.example.com</c></param>
        /// <param name="type">The DNS resource type to resolve. By default, this is the <c>A</c> record.</param>
        /// <param name="requestDnsSec">
        ///     Whether to request <c>DNSSEC</c> data in the response.<br/>
        ///     When requested, it will be accessible under the <see cref="Answer"/> array.
        /// </param>
        /// <param name="validateDnsSec">Whether to validate <c>DNSSEC</c> data.</param>
        /// <returns></returns>
        /// <exception cref="DnsOverHttpsException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Answer> GetFirst(string name, string type = "A", bool requestDnsSec = false, bool validateDnsSec = false)
        {
            Response res = await Resolve(name, type, requestDnsSec, validateDnsSec);
            return res.Answers?.FirstOrDefault();
        }

        /// <summary>
        /// Resolve a name using Cloudflare's DNS over HTTPS. This helper method returns all answers of a provided type.
        /// </summary>
        /// <param name="name">The FQDN to resolve. Example: <c>foo.bar.example.com</c></param>
        /// <param name="type">The DNS resource type to resolve. By default, this is the <c>A</c> record.</param>
        /// <param name="requestDnsSec">
        ///     Whether to request <c>DNSSEC</c> data in the response.<br/>
        ///     When requested, it will be accessible under the <see cref="Answer"/> array.
        /// </param>
        /// <param name="validateDnsSec">Whether to validate <c>DNSSEC</c> data.</param>
        /// <returns></returns>
        /// <exception cref="DnsOverHttpsException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Answer[]> GetAll(string name, string type = "A", bool requestDnsSec = false, bool validateDnsSec = false)
        {
            Response res = await Resolve(name, type, requestDnsSec, validateDnsSec);
            return res.Answers;
        }

        /// <summary>
        /// Resolve a name using Cloudflare's DNS over HTTPS. Use this method if you want full control over the output.
        /// <para>
        ///     Alternatively, <see cref="GetFirst"/> and <see cref="GetAll"/> may be used for better experience.
        /// </para>
        /// </summary>
        /// <param name="name">The FQDN to resolve. Example: <c>foo.bar.example.com</c></param>
        /// <param name="type">The DNS resource type to resolve. By default, this is the <c>A</c> record.</param>
        /// <param name="requestDnsSec">
        ///     Whether to request <c>DNSSEC</c> data in the response.<br/>
        ///     When requested, it will be accessible under the <see cref="Answer"/> array.
        /// </param>
        /// <param name="validateDnsSec">Whether to validate <c>DNSSEC</c> data.</param>
        /// <returns></returns>
        /// <exception cref="DnsOverHttpsException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Response> Resolve(string name, string type = "A", bool requestDnsSec = false, bool validateDnsSec = false)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name), "Name is null or empty.");
            if (string.IsNullOrEmpty(type)) type = "A";

            string url =
                $"?name={name.UrlEncode()}" +
                (type == "A" ? "" : $"&type={type.UrlEncode()}") +
                (requestDnsSec == false ? "" : $"&do=1") +
                (validateDnsSec == false ? "" : $"&cd=1");

            HttpRequestMessage req = new(HttpMethod.Get, url);
            HttpResponseMessage res = await Client.SendAsync(req);

            Response response = await res.Deseralize<Response>();
            if (res.StatusCode != HttpStatusCode.OK || !string.IsNullOrEmpty(response.Error) || !string.IsNullOrEmpty(response.Comment))
            {
                string message =
                    $"Failed to query type {type} of '{name}', received HTTP status code {res.StatusCode}." +
                    (string.IsNullOrEmpty(response.Error) ? "" : $"\nError: {response.Error}") +
                    (string.IsNullOrEmpty(response.Comment) ? "" : $"\nComment: {response.Comment}");

                throw new DnsOverHttpsException(message, response);
            }

            return response;
        }
    }
}