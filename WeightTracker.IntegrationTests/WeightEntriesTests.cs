using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using WeightTrack.Data.Requests;
using WeightTrack.Data.Responses;

namespace WeightTracker.IntegrationTests
{
    public class WeightEntriesTests: IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public WeightEntriesTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        [Fact]
        public async Task Get_return_weight_entries()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("api/weightentries");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_add_new_weight_entry()
        {
            // Arrange
            var request = JsonConvert.SerializeObject(new AddItemRequest() { 
                Date = DateTime.Now,
                Note = "Test weight",
                Weight = 67.89
            });

            var stringContent = new StringContent(request, UnicodeEncoding.UTF8, "application/json");
            // Act
            var response = await _client.PostAsync("api/weightentries", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var rez = JsonConvert.DeserializeObject<AddResponse>(await response.Content.ReadAsStringAsync());
            Assert.True(rez is AddResponse && rez.NewEntrytId > 0);
        }
    }
}