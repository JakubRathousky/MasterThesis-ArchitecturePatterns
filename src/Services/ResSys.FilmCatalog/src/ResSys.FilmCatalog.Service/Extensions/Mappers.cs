using ResSys.FilmCatalog.Service.Dtos;
using ResSys.FilmCatalog.Service.Entities;


namespace ResSys.FilmCatalog.Service.Extensions
{
    public static class Mappers
    {
        public static FilmDto AsDto(this Film item)
        {
            if (item == null)
                return null;
            return new FilmDto(item.Id, item.EIDR, item.Name, item.Description, item.Rating, 
                item.Amount, item.PublishDate, item.CreatedDate, item.AuthorRegNum, item.AuthorId);
        }
    }
}