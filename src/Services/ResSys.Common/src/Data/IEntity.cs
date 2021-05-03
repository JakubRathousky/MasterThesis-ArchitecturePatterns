using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ResSys.Common
{
    /// <summary>
    /// Common interface for all entities
    /// </summary>
    public interface IEntity
    {
        [BsonId]
        Guid Id { get; set; }
    }
}