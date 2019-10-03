using System;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace cosmosdb_deleter
{
    public class CosmosDBDeleter
    {
        private DocumentClient client;

        private Uri cosmosUrl;

        public CosmosDBDeleter(Uri cosmosUrl, string cosmosKey)
        {
            this.cosmosUrl = cosmosUrl;
            this.client = new DocumentClient(cosmosUrl, cosmosKey);
        }

        public IQueryable<Document> QueryDocuments(string query)
        {
            return client.CreateDocumentQuery<Document>(cosmosUrl, query, new FeedOptions() { EnableCrossPartitionQuery = true });
        }

        public void PrintDocuments(IQueryable<Document> documents)
        {
            foreach( var d in documents)
            {
              JsonConvert.SerializeObject(d, Formatting.Indented);
            }
        }
    }
}
	