using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ResSys.FilmCatalog.Service.Interfaces;

namespace ResSys.FilmCatalog.Service.Clients
{
    /// <summary>
    /// Client that communicates with a service of an author
    /// </summary>
    public class AuthorClient : IAuthorClient
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