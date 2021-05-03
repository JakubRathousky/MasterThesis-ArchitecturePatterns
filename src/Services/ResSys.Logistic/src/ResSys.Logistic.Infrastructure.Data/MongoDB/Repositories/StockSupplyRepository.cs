
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using ResSys.Common.MongoDB;
using ResSys.Logistic.Domain;
using ResSys.Logistic.Domain.Interfaces;

namespace ResSys.Logistic.Infrastructure.Data.MongoDB.Repositories
{
    public class StockSupplyRepository : MongoRepository<StockSupplies>, IStockSupplyRepository
    {
        public StockSupplyRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}