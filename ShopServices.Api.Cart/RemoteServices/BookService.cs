using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopServices.Api.Cart.RemoteInterfaces;
using ShopServices.Api.Cart.RemoteModel;

namespace ShopServices.Api.Cart.RemoteServices
{
    public class BookService : IBookService
    {

        private readonly IHttpClientFactory _httpClient;

        private readonly ILogger<BookService> _logger;

        public BookService(IHttpClientFactory httpClient, ILogger<BookService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, BookRemote book, string ErrorMessage)> GetBook(Guid BookId)
        {
            try{
                var client = _httpClient.CreateClient("Book");

                var response = await client.GetAsync($"api/book/{BookId}");

                if(response.IsSuccessStatusCode){
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true };

                    var result = JsonSerializer.Deserialize<BookRemote>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);

            }catch(Exception e){
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}