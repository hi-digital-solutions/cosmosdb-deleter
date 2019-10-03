using System;
using CommandLine;

namespace cosmosdb_deleter
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = "";
            string containerName;
            bool dryRun;
            Uri cosmosDBUri = null;
            string cosmosKey = "";

            Console.WriteLine("Hello World!");
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                        {
                            query = o.Query;
                            containerName = o.ContainerName;
                            dryRun = o.DryRun;
                            cosmosDBUri = new Uri(o.CosmosDBUri);
                            cosmosKey = o.CosmosDBKey;
                        });

            var deleter = new CosmosDBDeleter(cosmosDBUri, cosmosKey);
            var queryResult = deleter.QueryDocuments(query);
            deleter.PrintDocuments(queryResult);

        }
    }
    public class Options
    {
        [Option('q', "query", Required = true, HelpText = "The query for CosmosDB documents.")]
        public string Query { get; set; }

        [Option('c', "container", Required = true, HelpText = "The container for CosmosDB documents.")]
        public string ContainerName { get; set; }

        [Option('d', "dry-run", Default = false, Required = false, HelpText = "Print out documents but do not delete.")]
        public bool DryRun { get; set; }

        [Option('u', "cosmos-db-uri", Required = true, HelpText = "The uri for the CosmosDB.")]
        public string CosmosDBUri { get; set; }

        [Option('k', "cosmos-db-key", Required = true, HelpText = "The key for the CosmosDB.")]
        public string CosmosDBKey { get; set; }
    }
}
