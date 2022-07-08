using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shopservices.Api.Gateway.BookRemote;
using Shopservices.Api.Gateway.InterfaceRemote;

namespace Shopservices.Api.Gateway.MessageHandler
{
    public class BookHandler : DelegatingHandler
    {

        private readonly ILogger<BookHandler> _logger;
        private readonly IAuthorRemote _authorRemote;
        public BookHandler(ILogger<BookHandler> logger, IAuthorRemote authorRemote)
        {
            _logger = logger;
            _authorRemote = authorRemote;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken){

            var time = Stopwatch.StartNew();
            _logger.LogInformation("Request started");

            var response = await base.SendAsync(request, cancellationToken);

            _logger.LogInformation($"Process made in {time.ElapsedMilliseconds}ms");

            if( response.IsSuccessStatusCode){
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<BookModelRemote>(content, options);

                var authorResponse = await _authorRemote.GetAuthor(result.AuthorBook ?? Guid.Empty);

                if( authorResponse.result ){
                    var authorObject = authorResponse.author;
                    result.AuthorData = authorObject;
                    
                    var resultStr = JsonSerializer.Serialize(result);
                    response.Content =  new StringContent(resultStr, System.Text.Encoding.UTF8, "application/json");
                }
            }

            return response;
        }
    }
}