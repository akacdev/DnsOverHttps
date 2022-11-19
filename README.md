# DnsOverHttps

<div align="center">
  <img width="256" height="256" src="https://raw.githubusercontent.com/actually-akac/DnsOverHttps/master/DnsOverHttps/icon.png">
</div>

<div align="center">
  <b>An async and lightweight C# library for Cloudflare's DNS over HTTPS.</b>
</div>

## Usage
Provides an easy interface for interacting with Cloudflare's DNS over HTTPS endpoints. Learn more about it [here](https://developers.cloudflare.com/1.1.1.1/encryption/dns-over-https/).

To get started, add the library into your solution with either the `NuGet Package Manager` or the `dotnet` CLI.
```rust
dotnet add package DnsOverHttps
```

For the primary class to become available, import the used namespace.
```csharp
using DnsOverHttps;
```

Need more examples? Under the `Example` directory you can find a working demo project that implements this library.

## Features
- Built for **.NET 6** and **.NET 7**
- Fully **async**
- Deep coverage of the API
- Extensive **XML documentation**
- **No external dependencies** (uses integrated HTTP and JSON)
- **Custom exceptions** (`DnsOverHttpsException`) for advanced catching
- Example project to demonstrate all capabilities of the library
- Execute DNS queries over HTTPS of any type

## Code Samples

### Initializing a new API client
```csharp
DnsOverHttpsClient dns = new();
```

### Resolving A DNS records including DNSSEC
```csharp
Response response = await dns.Resolve("discord.com", "A", true, true);
```

### Using helper methods to return the first or all answers
```csharp
Answer nsAnswer = await dns.GetFirst("example.com", "NS");
Answer[] aAnswers = await dns.GetAll("reddit.com", "A");
```

## Available Methods
- Task\<Response> **Resolve**(string name, string type = "A", bool requestDnsSec = false, bool validateDnsSec = false)
- Task\<Answer> **GetFirst**(string name, string type = "A", bool requestDnsSec = false, bool validateDnsSec = false)
- Task\<Answer[]> **GetAll**(string name, string type = "A", bool requestDnsSec = false, bool validateDnsSec = false)

## Resources
- Cloudflare: https://cloudflare.com
- 1.1.1.1: https://1.1.1.1
- Introduction: https://developers.cloudflare.com/1.1.1.1/encryption/dns-over-https

*This is a community-ran library. Not affiliated with Cloudflare.*

*Icon made by **Freepik** at [Flaticon](https://www.flaticon.com).*