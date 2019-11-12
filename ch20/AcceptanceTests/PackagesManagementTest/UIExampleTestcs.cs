
using PackagesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.IO;

namespace PackagesManagementTest
{
    public class UIExampleTestcs:
         IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly
            WebApplicationFactory<Startup> _factory;
        public UIExampleTestcs(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TestMenu()
        {
            var client = _factory.CreateClient();
            
            //Create an angleSharp default configuration
            var config = Configuration.Default;

            //Create a new context for evaluating webpages 
            //with the given config
            var context = BrowsingContext.New(config);

            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            string source;
            using (StreamReader responseReader = new StreamReader(stream))
            {
                source = await responseReader.ReadToEndAsync();
            }
            var document = await context.OpenAsync(req =>
                req.Content(source));
            var node = document.QuerySelector("a[href=\"/ManagePackages\"]");   

            Assert.NotNull(node);
        }
    }
}
