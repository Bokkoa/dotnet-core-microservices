using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shopservices.Api.Gateway.BookRemote;
using Shopservices.Api.Gateway.InterfaceRemote;

namespace Shopservices.Api.Gateway.ImplementRemote
{
    public class AuthorRemote : IAuthorRemote
    {

        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<AuthorRemote> _logger;

        public AuthorRemote(IHttpClientFactory httpClient, ILogger<AuthorRemote> logger ){
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, AuthorModelRemote author, string ErrorMessage)> GetAuthor(Guid AuthorId)
        {
            try{
                
                var client = _httpClient.CreateClient("AuthorService");
                var response = await client.GetAsync($"/Auhtor/{AuthorId}");

                if( response.IsSuccessStatusCode ){
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<AuthorModelRemote>( content, options);

                    return ( true, result, null);

                }

                return ( false, null, response.ReasonPhrase);

            }catch( Exception err ){
                _logger.LogError(err.ToString());
                return ( false, null, err.Message );
            }
        }
    }
}