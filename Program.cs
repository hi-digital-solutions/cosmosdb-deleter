using System;
using System.Threading.Tasks;
using CommandLine;

namespace cosmosdb_deleter
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => options = o)
                    .WithNotParsed<Options>((errs) => System.Environment.Exit(1));

            var deleter = new CosmosDBDeleter(options.CosmosDBUri, options.CosmosDBKey);
            var documentFeed = deleter.GetDocumentFeed(options.DatabaseName, options.ContainerName, options.Query);
            var task = deleter.PrintDocuments(documentFeed);
            task.Wait();

            Console.WriteLine("Done.\n");
        }
    }
    public class Options
    {
        [Option('q', "query", Required = true, HelpText = "The query for CosmosDB documents.")]
        public string Query { get; set; }

        [Option('d', "database", Required = true, HelpText = "The Cosmos DB name.")]
        public string DatabaseName { get; set; }

        [Option('c', "container", Required = true, HelpText = "The container for CosmosDB documents.")]
        public string ContainerName { get; set; }

        [Option('p', "print-only", Default = false, Required = false, HelpText = "Print out documents but do not delete.")]
        public bool PrintOnly { get; set; }

        [Option('u', "cosmos-db-uri", Required = true, HelpText = "The uri for the CosmosDB.")]
        public string CosmosDBUri { get; set; }

        [Option('k', "cosmos-db-key", Required = true, HelpText = "The key for the CosmosDB.")]
        public string CosmosDBKey { get; set; }
    }
}
