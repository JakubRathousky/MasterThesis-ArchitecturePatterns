using ResSys.BookCatalog.Service.Dtos;
using ResSys.BookCatalog.Service.Entities;


namespace ResSys.BookCatalog.Service.Extensions
{
    public static class Extensions
    {
        public static BookDto AsDto(this Book item)
        {
            if (item == null)
                return null;
            return new BookDto(item.Id, item.IBAN, item.Name, item.Description, 
                item.NumberOfPages, item.Amount, item.PublishDate, item.CreatedDate);
        }
    }
}