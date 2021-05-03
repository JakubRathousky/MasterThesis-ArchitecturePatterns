using System;
using MongoDB.Driver;
using ResSys.Common;
using ResSys.Common.MongoDB;
using ResSys.ReservationSystem.Data.Entities;
using ResSys.ReservationSystem.Service.Interfaces;

namespace ResSys.ReservationSystem.Repo
{
    public class ReservationRepository : MongoRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}
