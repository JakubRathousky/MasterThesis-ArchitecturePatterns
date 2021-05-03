using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResSys.AuthorCatalog.Service.Dtos;
using ResSys.AuthorCatalog.Service.Entities;
using ResSys.AuthorCatalog.Service.Extensions;
using ResSys.Common;


namespace ResSys.AuthorCatalog.Service
{
    public class SeedService : ISeedService
    {
        private readonly IRepository<Author> authorRepository;

        public SeedService(IRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        public async Task Seed()
        {
            var items = await authorRepository.GetAllAsync();

            if (items.Any())
                return;
            foreach (var author in this.authors)
            {
                await authorRepository.CreateAsync(author);
            }
        }
        List<Author> authors = new List<Author>(){
            new Author
            {
                Name = "Borbála Jenifer",
                AuthorRegNum = 1,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Angelina Avilius",
                AuthorRegNum = 2,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Rakesh Noor",
                AuthorRegNum = 3,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Rolf Annetta",
                AuthorRegNum = 4,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Clara Alexis",
                AuthorRegNum = 5,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Rafaela Praxiteles",
                AuthorRegNum = 6,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Rahul Walhberct",
                AuthorRegNum = 7,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Saša Natan",
                AuthorRegNum = 8,
                CreatedDate = DateTimeOffset.UtcNow
            },
            new Author
            {
                Name = "Adalet Uduakobong",
                AuthorRegNum = 9,
                CreatedDate = DateTimeOffset.UtcNow
            },
        };
    }
}