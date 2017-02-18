using FurnishedHome.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnishedHome.Services
{
    public interface IRoleService
    {
        void AddRole(Role role);
        Role GetRole(string name);
        Role GetRoleById(ObjectId id);
        IEnumerable<Role> GetRoles();
        Task<Role> GetRoleAsync(string name);
    }

    public class RoleService : IRoleService
    {
        private readonly IMongoCollection<Role> _collection;

        public RoleService(IMongoCollection<Role> collection)
        {
            _collection = collection;
        }

        public void AddRole(Role role)
        {
            _collection.InsertOne(role);
        }

        public Role GetRole(string name)
        {
            var filterBuilder = Builders<Role>.Filter;
            var filter = filterBuilder.Eq("Name", name);
            return _collection.Find(filter).FirstOrDefault();
        }

        public async Task<Role> GetRoleAsync(string name)
        {
            var filterBuilder = Builders<Role>.Filter;
            var filter = filterBuilder.Eq("Name", name);
            return (await _collection.FindAsync(filter)).FirstOrDefault();
        }

        public Role GetRoleById(ObjectId id)
        {
            var filterBuilder = Builders<Role>.Filter;
            var filter = filterBuilder.Eq("_id", id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<Role> GetRoles()
        {
            return _collection.FindSync(new BsonDocument()).ToEnumerable();
        }
    }
}
