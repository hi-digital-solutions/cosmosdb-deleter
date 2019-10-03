using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace cosmosdb_deleter
{
    public class CosmosDBDeleter
    {
        private CosmosClient cosmosClient;

        public CosmosDBDeleter(string cosmosUrl, string cosmosKey)
        {
            this.cosmosClient = new CosmosClient(cosmosUrl, cosmosKey);
        }

        public FeedIterator<Document> GetDocumentFeed(string databaseName, string containerName, string query)
        {
            var database = cosmosClient.GetDatabase(databaseName);
            var container = database.GetContainer(containerName);

            var queryDef = new QueryDefinition(query);
            var queryIterator = container.GetItemQueryIterator<Document>(queryDef);

            return queryIterator;
        }

        public async Task PrintDocuments(FeedIterator<Document> feed)
        {
            while (feed.HasMoreResults)
            {
                var response = await feed.ReadNextAsync();
                foreach (var document in response.Resource)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(document, Formatting.Indented)); 
                }
            }
        }
    }
}
	