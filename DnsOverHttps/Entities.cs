using System.Text.Json.Serialization;

namespace DnsOverHttps
{
    /// <summary>
    /// Indicates the status of a query (DNS RCODE).
    /// <para>
    ///     Documentation: <a href="https://www.iana.org/assignments/dns-parameters/dns-parameters.xhtml"></a><br/>
    ///     RFC: <a href="https://www.iana.org/go/rfc6895"></a>, <a href="https://www.iana.org/go/rfc1035"></a>
    /// </para>
    /// </summary>
    public enum ResponseCode : byte
    {
        /// <summary>
        /// DNS Query completed successfully.
        /// </summary>
        NoError,
        /// <summary>
        /// DNS query resulted in a format error.
        /// </summary>
        FormatError,
        /// <summary>
        /// Server failed to complete the DNS query.
        /// </summary>
        ServerFailure,
        /// <summary>
        /// Domain name does not exist.
        /// </summary>
        NXDomain,
        /// <summary>
        /// Function not implemented.
        /// </summary>
        NotImplemented,
        /// <summary>
        /// The server refused to answer to the DNS query.
        /// </summary>
        Refused,
        /// <summary>
        /// Name that should not exist, does exist.
        /// </summary>
        YXDomain,
        /// <summary>
        /// RR Set exists when it should not.
        /// </summary>
        YXRRSet,
        /// <summary>
        /// RR Set that should exist does not.
        /// </summary>
        NXRRSet,
        /// <summary>
        /// Server not authoritative for zone/Not authorized.
        /// </summary>
        NotAuth,
        /// <summary>
        /// Name not contained in zone.
        /// </summary>
        NotZone,
        /// <summary>
        /// DSO-TYPE not implemented.
        /// </summary>
        DSOTYPENotImplemented,
        /// <summary>
        /// Bad OPT version.
        /// </summary>
        BadVersion = 16,
        /// <summary>
        /// TSIG signature failure.
        /// </summary>
        BadSignature,
        /// <summary>
        /// Key not recognized.
        /// </summary>
        BadKey,
        /// <summary>
        /// Signature out of time window.
        /// </summary>
        BadTime,
        /// <summary>
        /// Bad TKEY Mode.
        /// </summary>
        BadMode,
        /// <summary>
        /// Duplicate key name.
        /// </summary>
        BadName,
        /// <summary>
        /// Algorithm not supported.
        /// </summary>
        BadAlgorithm,
        /// <summary>
        /// Bad truncation.
        /// </summary>
        BadTruncation,
        /// <summary>
        /// Bad/missing server cookie.
        /// </summary>
        BadCookie
    }

    /// <summary>
    /// Indicates the type of a DNS resource record.
    /// <para>
    ///     Documentation: <a href="https://www.iana.org/assignments/dns-parameters/dns-parameters.xhtml"></a><br/>
    /// </para>
    /// </summary>
    public enum ResourceRecordType : byte
    {
        Reserved,
        /// <summary>
        /// A host address.
        /// </summary>
        A,
        /// <summary>
        /// An authoritative name server.
        /// </summary>
        NS,
        /// <summary>
        /// A mail destination (OBSOLETE - use MX).
        /// </summary>
        MD,
        /// <summary>
        /// A mail forwarder (OBSOLETE - use MX).
        /// </summary>
        MF,
        /// <summary>
        /// The canonical name for an alias.
        /// </summary>
        CNAME,
        /// <summary>
        /// Marks the start of a zone of authority.
        /// </summary>
        SOA,
        /// <summary>
        /// A mailbox domain name (EXPERIMENTAL).
        /// </summary>
        MB,
        /// <summary>
        /// A mail group member (EXPERIMENTAL).
        /// </summary>
        MG,
        /// <summary>
        /// A mail rename domain name (EXPERIMENTAL).
        /// </summary>
        MR,
        /// <summary>
        /// A null RR (EXPERIMENTAL).
        /// </summary>
        NULL,
        /// <summary>
        /// A well known service description.
        /// </summary>
        WKS,
        /// <summary>
        /// A domain name pointer.
        /// </summary>
        PTR,
        /// <summary>
        /// Host information.
        /// </summary>
        HINFO,
        /// <summary>
        /// Mailbox or mail list information.
        /// </summary>
        MINFO,
        /// <summary>
        /// Mail exchange.
        /// </summary>
        MX,
        /// <summary>
        /// Text strings.
        /// </summary>
        TXT,
        /// <summary>
        /// For responsible person.
        /// </summary>
        RP,
        /// <summary>
        /// For a security signature.
        /// </summary>
        SIG = 24,
        /// <summary>
        /// IPv6 Address.
        /// </summary>
        AAAA = 28,
        /// <summary>
        /// Location information.
        /// </summary>
        LOC = 29,
        /// <summary>
        /// Server selection.
        /// </summary>
        SRV = 33,
        DNAME = 39,
        IPSECKEY = 45,
        /// <summary>
        /// RRset Signature.
        /// </summary>
        RRSIG = 46,
        DNSKEY = 48,
        /// <summary>
        /// For the sender policy framework.
        /// </summary>
        SPF = 99
    }

    /// <summary>
    /// The result of a DNS over HTTPS query.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// A DNS response code.
        /// </summary>
        [JsonPropertyName("Status")]
        public ResponseCode Status { get; set; }

        /// <summary>
        /// <b><c>TC</c></b>: Whether the response is truncated due to length greater than that permitted on the transmission channel.
        /// </summary>
        [JsonPropertyName("TC")]
        public bool IsTruncated { get; set; }

        /// <summary>
        /// <b><c>RD</c></b>: Whether recursion is desired.<br/>
        /// This bit may be set in a query and is copied into the response. If RD is set, it directs the name server to pursue the query recursively. Recursive query support is optional. 
        /// </summary>
        [JsonPropertyName("RD")]
        public bool IsRecursionDesired { get; set; }

        /// <summary>
        /// <b><c>RA</c></b>: Whether recursion is available.<br/>
        /// This bit is set or cleared in a response, and denotes whether recursive query support is available in the name server.
        /// </summary>
        [JsonPropertyName("RA")]
        public bool IsRecursionAvailable { get; set; }

        /// <summary>
        /// <b><c>AD</c></b>: Whether the resolver believes the responses to be authentic - that is, validated by DNSSEC.
        /// </summary>
        [JsonPropertyName("AD")]
        public bool AuthenticData { get; set; }

        /// <summary>
        /// <b><c>CD</c></b>: Whether a security-aware resolver should disable signature validation (that is, not check DNSSEC records).
        /// </summary>
        [JsonPropertyName("CD")]
        public bool CheckingDisabled { get; set; }

        /// <summary>
        /// A DNS question sent by the client.
        /// </summary>
        [JsonPropertyName("Question")]
        public Question[] Questions { get; set; }

        /// <summary>
        /// A DNS answer sent by the server.
        /// </summary>
        [JsonPropertyName("Answer")]
        public Answer[] Answers { get; set; }

        /// <summary>
        /// A DNS authority sent by the server.
        /// </summary>
        [JsonPropertyName("Authority")]
        public Answer[] Authorities { get; set; }

        /// <summary>
        /// Additional answers sent by the server.
        /// </summary>
        [JsonPropertyName("Additional")]
        public Answer[] Additional { get; set; }

        /// <summary>
        /// An error message describing an issue with your DNS query. This is always included in the <c>400 Bad Request</c> status code.
        /// </summary>
        [JsonPropertyName("error")]
        public string Error { get; set; }

        /// <summary>
        /// An extended DNS error code message.
        /// <para>
        ///     Documentation: <a href="https://developers.cloudflare.com/1.1.1.1/infrastructure/extended-dns-error-codes/"></a>
        /// </para>
        /// </summary>
        [JsonPropertyName("Comment")]
        public string[] Comments { get; set; }
    }

    /// <summary>   
    /// A DNS question sent by the client.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// The FQDN record name requested.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The type of DNS record requested.
        /// </summary>
        [JsonPropertyName("type")]
        public ResourceRecordType Type { get; set; }
    }

    /// <summary>
    /// A DNS answer sent by the server.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// The record owner.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The type of DNS record.
        /// </summary>
        [JsonPropertyName("type")]
        public ResourceRecordType Type { get; set; }

        /// <summary>
        /// The number of seconds the answer can be stored in cache before it is considered stale.
        /// </summary>
        [JsonPropertyName("TTL")]
        public int TTL { get; set; }

        /// <summary>
        /// The value of the DNS record for the given name and type. The data will be in text for standardized record types and in HEX for unknown types.
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}