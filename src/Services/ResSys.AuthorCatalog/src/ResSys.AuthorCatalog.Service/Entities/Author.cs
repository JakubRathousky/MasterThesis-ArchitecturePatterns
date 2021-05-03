using System;
using ResSys.Common;

namespace ResSys.AuthorCatalog.Service.Entities
{
    public class Author : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AuthorRegNum { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}