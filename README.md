# DnsOverHttps

<div align="center">
  <img width="256" height="256" src="https://raw.githubusercontent.com/actually-akac/DnsOverHttps/master/DnsOverHttps/icon.png">
</div>

<div align="center">
  An async and lightweight C# library for Cloudflare's DNS over HTTPS.
</div>

## Usage
This library provides an easy interface for interacting with Cloudflare's DNS over HTTPS endpoints.

DoH is a protocol that enhances the privacy and security of DNS queries by encrypting them using HTTPS. This helps prevent unauthorized access or tampering of DNS data during transmission. Learn more about it [here](https://developers.cloudflare.com/1.1.1.1/encryption/dns-over-https/).

To get started, import the library into your solution with either the `NuGet Package Manager` or the `dotnet` CLI.
```rust
dotnet add package DnsOverHttps
```

For the primary class to become available, import the used namespace.
```csharp
using DnsOverHttps;
```

Need more examples? Under the `Example` directory you can find a working demo project that implements this library.

## Properties
- Built for **.NET 8**, **.NET 7** and **.NET 6**
- Fully **async**
- Extensive **XML documentation**
- **No external dependencies** (makes use of built-in `HttpClient` and `JsonSerializer`)
- **Custom exceptions** (`DnsOverHttpsException`) for easy debugging
- Example project to demonstrate all capabilities of the library

## Features
- Resolve one or all DNS records under a hostname
- Ask for DNSSEC validation
- Query in parallel
- Specify advanced parameters

## Code Samples

### Initializing a new API client
```csharp
DnsOverHttpsClient dns = new();
```

### Resolving A DNS records including DNSSEC
```csharp
Response response = await dns.Resolve("discord.com", ResourceRecordType.A, true, true);
```

### Using helper methods to return the first or all answers
```csharp
Answer? nsAnswer = await dns.ResolveFirst("example.com", ResourceRecordType.NS);
Answer[] aAnswers = await dns.ResolveAll("reddit.com", ResourceRecordType.A);
```

## Resources
- Cloudflare: https://cloudflare.com
- 1.1.1.1: https://1.1.1.1
- Introduction: https://developers.cloudflare.com/1.1.1.1/encryption/dns-over-https

*This is a community-ran library. Not affiliated with Cloudflare, Inc.*

*Icon made by **Freepik** at [Flaticon](https://www.flaticon.com).*