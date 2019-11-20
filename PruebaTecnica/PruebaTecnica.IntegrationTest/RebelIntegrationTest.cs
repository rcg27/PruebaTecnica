using Newtonsoft.Json;
using PruebaTecnica.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PruebaTecnica.IntegrationTest
{
    public class RebelIntegrationTest
    {
        [Fact]
        public async Task Test_Get()
        {
            var client = new TestClientProvider().Client;

            var response = await client.GetAsync("/Rebels");

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test_Post()
        {
            var client = new TestClientProvider().Client;
            var response = await client.PostAsync("/Rebels", new StringContent(
                JsonConvert.SerializeObject(new Rebel() {Name="name",Planet="planet"}),Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
