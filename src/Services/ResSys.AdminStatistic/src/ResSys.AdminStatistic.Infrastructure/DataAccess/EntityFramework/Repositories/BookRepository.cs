using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
// using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ResSys.Common;
using ResSys.AdminStatistic.Application.Repositories;
using ResSys.AdminStatistic.Domain.BookCatalog;
using ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Helpers;

namespace ResSys.AdminStatistic.Infrastructure.EntityFrameworkDataAccess.Repositories
{
    public class BookRepository : IBookReadOnlyRepository, IBookWriteOnlyRepository
    {
        protected Context context;


        public BookRepository(Context dataContext)
        {
            this.context = dataContext;
        }

        public async Task<IReadOnlyCollection<Book>> GetAllAsync()
        {
            var dbBooks = await context.Books.ToListAsync();
            return dbBooks.Select(book => book.AsDomain()).ToList();
        }

        public async Task<Book> GetAsync(Guid id)
        {
            var book = await context.Books.FindAsync(id);
            return book.AsDomain();
        }

        public async Task<Book> GetAsync(string IBAN)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.IBAN == IBAN);
            if (book == null)
                return null;
            var model = book.AsDomain();
            return model;
        }

        public async Task<Book> GetAsync(Func<Book, bool> filter)
        {
            var books = (await context.Books.ToListAsync()).Select(book => book.AsDomain()).ToList().Where(filter);
            return books.SingleOrDefault();
        }

        public async Task CreateAsync(Book book)
        {
            var model = book.AsEntity();
            await context.Books.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book entity)
        {
            Entities.Book Book = entity.AsEntity();

            context.Books.Attach(Book);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            Entities.Book entityToDelete = await context.Books.FindAsync(id);
            context.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }
    }
}