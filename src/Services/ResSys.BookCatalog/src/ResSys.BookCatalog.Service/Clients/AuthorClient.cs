using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ResSys.BookCatalog.Service.Clients
{
    /// <summary>
    /// Client that communicates with an external service of an author
    /// </summary>
    public class AuthorClient
    {
        private readonly HttpClient httpClient;

        public AuthorClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Guid?> GetAuthorIdAsync(int authorRegNum)
        {
            var authorId = await httpClient.GetFromJsonAsync<Guid?>($"/authors/{authorRegNum}");

            return authorId;
        }
    }
}