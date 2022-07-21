using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using WeightTrack.Data.Requests;
using WeightTrack.Data.Responses;

namespace WeightTracker.IntegrationTests
{
    public class TargetTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public TargetTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Get_return_target_entries()
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("api/target/GetAll");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Get_return_target_progress()
        {
            // Arrange
            var newTargetResponse = await AddNewTargetAsync(); //So that there is at least one target already save in DB
            var rez = JsonConvert.DeserializeObject<AddResponse>(await newTargetResponse.Content.ReadAsStringAsync());
            int newTargetId = rez.NewEntrytId;

            // Act
            var progressResponse = await _client.GetAsync($"api/target/GetProgress/{newTargetId}");

            // Assert
            progressResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, progressResponse.StatusCode);
            var result = JsonConvert.DeserializeObject<ProgressResponse>(await progressResponse.Content.ReadAsStringAsync());
            Assert.True(result is ProgressResponse && result.TargetId > 0);
        }

        [Fact]
        public async Task Post_add_new_target_entry()
        {
            // Arrange
            // Act
            var response = await AddNewTargetAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var rez = JsonConvert.DeserializeObject<AddResponse>(await response.Content.ReadAsStringAsync());
            Assert.True(rez is AddResponse && rez.NewEntrytId > 0);
        }

        private async Task<HttpResponseMessage> AddNewTargetAsync()
        {
            var request = JsonConvert.SerializeObject(new AddItemRequest()
            {
                Date = DateTime.Now,
                Note = "Test target",
                Weight = 50
            });

            var stringContent = new StringContent(request, UnicodeEncoding.UTF8, "application/json");

            return await _client.PostAsync("api/target", stringContent);
        }
    }
}
