using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ResSys.Common.MongoDB
{
    /// <summary>
    /// Repository for MongoDatabase connector
    /// </summary>
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        private readonly UpdateDefinitionBuilder<T> updateBuilder = Builders<T>.Update;
      
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {

            return await dbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<T> filter = filterBuilder.Eq(en => en.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task UpdateOneAsync(T entity, string filterPropPath, object filterPropVal, string updatePropPath, object updatePropValue)
        {
            if (entity == null || string.IsNullOrEmpty(filterPropPath) || string.IsNullOrEmpty(updatePropPath))
            {
                throw new ArgumentNullException(nameof(T));
            }
            var filter = filterBuilder.Eq(en => en.Id, entity.Id) & filterBuilder.Eq(filterPropPath, filterPropVal);
            var update = updateBuilder.Set(updatePropPath, updatePropValue);
            await dbCollection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(en => en.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}