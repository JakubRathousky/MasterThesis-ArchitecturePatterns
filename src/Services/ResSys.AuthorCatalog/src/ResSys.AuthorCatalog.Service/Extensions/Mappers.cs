using ResSys.AuthorCatalog.Service.Dtos;
using ResSys.AuthorCatalog.Service.Entities;

namespace ResSys.AuthorCatalog.Service.Extensions
{
    public static class Mappers
    {
        public static AuthorDto AsDto(this Author item)
        {
            if (item == null)
                return null;
            return new AuthorDto(item.Id, item.Name, item.AuthorRegNum, item.CreatedDate);
        }
    }
}