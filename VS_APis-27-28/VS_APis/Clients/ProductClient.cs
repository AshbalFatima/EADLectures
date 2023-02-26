using Apis.Common.Models;

namespace VS_APis.Clients
{
    public class ProductClient
    {
        private readonly HttpClient _httpClient;
        public ProductClient(HttpClient client) {
            _httpClient = client;
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
        {
            var temp_list = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<Product>>("all");
            return temp_list;
        }
    }
}
