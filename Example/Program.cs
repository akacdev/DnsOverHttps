using System;
using System.Threading.Tasks;
using DnsOverHttps;

namespace Example
{
    public static class Program
    {
        private static readonly DnsOverHttpsClient Client = new();

        public static async Task Main()
        {
            Console.WriteLine($"> Resolving the first NS record on example.com");
            Answer? nsAnswer = await Client.ResolveFirst("example.com", ResourceRecordType.NS);

            Console.WriteLine($"Result:");
            PrintAnswer(nsAnswer);


            Console.WriteLine($"\n> Resolving all A records on reddit.com");
            Answer[] aAnswers = await Client.ResolveAll("reddit.com", ResourceRecordType.A);

            Console.WriteLine($"Result:");
            foreach (Answer answer in aAnswers) PrintAnswer(answer);
            Console.WriteLine();


            Console.WriteLine($"\n> Resolving an invalid domain");
            Answer? nxDomain = await Client.ResolveFirst("5525fe855b7366f93447cd039ab885.com", ResourceRecordType.A);
            Console.WriteLine($"Result is {(nxDomain is null ? "null" : $"not null: {nxDomain.Value.Data}")}");
            Console.WriteLine();


            Console.WriteLine($"\n> Resolving A records on discord.com with DNSSEC");
            Response response = await Client.Resolve("discord.com", ResourceRecordType.A, true, true);

            Console.WriteLine($"Result:");
            foreach (Answer answer in response.Answers) PrintAnswer(answer);


            Console.WriteLine($"\n> Resolving multiple records in parallel on github.com");
            Response[] responses = await Client.Resolve("github.com", [ResourceRecordType.A, ResourceRecordType.MX, ResourceRecordType.NS]);

            foreach (Response resp in responses)
                foreach (Answer answer in resp.Answers) PrintAnswer(answer);


            Console.WriteLine("\nDemo finished");
            Console.ReadKey();
        }

        public static void PrintAnswer(Answer? answer)
        {
            Console.WriteLine($"\tType: {answer.Value.Type}; TTL: {answer.Value.TTL}; Translation: {answer.Value.Name} => {answer.Value.Data}");
        }
    }
}