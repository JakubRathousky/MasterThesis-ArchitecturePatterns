using System;
using System.ComponentModel.DataAnnotations;

namespace ResSys.AuthorCatalog.Service.Dtos
{
    public record AuthorDto(Guid Id, string Name, int AuthorRegNum, DateTimeOffset CreatedDate);
    public record CreateAuthorDto(string Name, [Range(0, 1000)] int AuthorRegNum);
    public record UpdateAuthorDto(string Name, [Range(0, 1000)] int AuthorRegNum);
}