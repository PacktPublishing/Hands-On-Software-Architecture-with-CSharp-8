using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;

namespace FunctionAppWWTravel
{
    public static class SendEmail
    {
        [FunctionName(nameof(SendEmail))]
        public static async Task<HttpResponseMessage> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestMessage req,
            ILogger log)
        {
            var requestData = await req.Content.ReadAsStringAsync();

            var YOUR_CONNECTION_STRING = "[YOUR_AZURE_STORAGE_ACCOUNT_CONNECTION_STRING_HERE]";

            var storageAccount = CloudStorageAccount.Parse(YOUR_CONNECTION_STRING);

            var queueClient = storageAccount.CreateCloudQueueClient();

            var messageQueue = queueClient.GetQueueReference("email");

            var message = new CloudQueueMessage(requestData);

            await messageQueue.AddMessageAsync(message);

            log.LogInformation("HTTP trigger from SendEmail function processed a request.");
            return req.CreateResponse(HttpStatusCode.OK, new { success = true }, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
    
}
