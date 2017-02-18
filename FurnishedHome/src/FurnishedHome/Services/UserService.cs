using FurnishedHome.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace FurnishedHome.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUser(string email, string password);
        Task<User> GetUserAsync(string email, string password);
        User GetUserByEmail(string email);
        Task<User> GetUserByEmailAsync(string email);
    }
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _collection;

        public UserService(IMongoCollection<User> collection)
        {
            _collection = collection;
        }
        public void AddUser(User user)
        {
            _collection.InsertOne(user);
        }

        public async Task AddUserAsync(User user)
        {
            await _collection.InsertOneAsync(user);
        }

        public User GetUser(string email, string password)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq("Email", email) & filterBuilder.Eq("Password", password);
            return _collection.Find(filter).FirstOrDefault();
        }
        public async Task<User> GetUserAsync(string email, string password)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq("Email", email) & filterBuilder.Eq("Password", password);
            return (await _collection.FindAsync(filter)).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq("Email", email);
            return _collection.Find(filter).FirstOrDefault();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var filterBuilder = Builders<User>.Filter;
            var filter = filterBuilder.Eq("Email", email);
            return (await _collection.FindAsync(filter)).FirstOrDefault();
        }
    }
}
