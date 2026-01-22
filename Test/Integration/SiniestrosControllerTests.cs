using Application.DTO.Pagination;
using Application.DTO.Response;
using Azure;
using System.Net.Http.Json;

namespace Test.Integration
{
    public class SiniestrosControllerTests
     : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public SiniestrosControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Then_Get_Siniestro_Should_Work()
        {
            // Arrange
            var createRequest = new
            {
                siniestro = new
                {
                    fechahora = DateTime.Now,
                    departamentos_id = 1,
                    ciudades_id = 1,
                    tipos_siniestro_id = 1,
                    vehiculos_involucrados = 2,
                    numero_victimas = 1,
                    descripcion = "Integration test"
                }
            };

            var postResponse = await _client.PostAsJsonAsync(
                "/api/siniestros",
                createRequest
            );

            var content = await postResponse.Content.ReadAsStringAsync();

            Assert.True(postResponse.IsSuccessStatusCode, content);
            // Act - GET
            var getResponse = await _client.GetAsync(
                "/api/siniestros?page=1&pageSize=10");

            getResponse.EnsureSuccessStatusCode();

            var result = await getResponse.Content
                .ReadFromJsonAsync<PagedResult<SiniestroResponse>>();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.TotalItems >= 1);
        }

        [Fact]
        public async Task Should_Reach_Siniestros_Endpoint()
        {
            var response = await _client.GetAsync("/api/siniestros");

            Assert.NotEqual(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
