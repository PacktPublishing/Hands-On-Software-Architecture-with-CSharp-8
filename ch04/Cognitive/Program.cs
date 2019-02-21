using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cognitive
{
    class Program
    {
        private static async Task<string> PostAPI(string api, string key, string region, string textToTranslate)
        {
            string result = String.Empty;

            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, api))
                {
                    request.Headers.Add("Ocp-Apim-Subscription-Key", key);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", region);

                    // five seconds for timeout
                    client.Timeout = new TimeSpan(0, 0, 5);
                    var body =  new { Text = textToTranslate } ;
                    var requestBody = JsonConvert.SerializeObject(body);    

                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                        result = await response.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        /// <summary>
        /// Check this content at:https://docs.microsoft.com/en-us/azure/cognitive-services/translator/reference/v3-0-reference
        /// </summary>
        static void Main()
        {
            var host = "https://api.cognitive.microsofttranslator.com";
            var route = "/translate?api-version=3.0&to=es";
            var subscriptionKey = "[YOUR KEY HERE]";
            var region = "centralus";
            if (subscriptionKey == "[YOUR REGION HERE]")
            {
                Console.WriteLine("Please, informe your key: ");
                subscriptionKey = Console.ReadLine();
            }
            if (region  == "[YOUR REGION HERE]")
            {
                Console.WriteLine("Please, informe your region: ");
                region = Console.ReadLine();
            }
            var translatedSentence = PostAPI(host + route, subscriptionKey, region, "Hello World!").Result;
            Console.WriteLine(translatedSentence);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
