using FurnishedHome.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace FurnishedHome.Services
{
    public interface IPropertyService
    {
        void AddProperty(Property property);
        Property GetPropertyById(int id);
        IEnumerable<Property> GetAllProperties();
        IEnumerable<Property> GetPropertiesByCity(object city);
        void DeleteProperty(int id);
        void UpdateProperty(int id, Property property);
    }

    public class MongoPropertyService : IPropertyService
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public MongoPropertyService()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("FurnishedHomeDB");
        }
        public void AddProperty(Property property)
        {
            _db.GetCollection<Property>("Properties").InsertOne(property);
        }

        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Property> GetAllProperties()
        {
            return _db.GetCollection<Property>("Properties").FindSync(new BsonDocument()).ToEnumerable();
        }

        public IEnumerable<Property> GetPropertiesByCity(object city)
        {
            throw new NotImplementedException();
        }

        public Property GetPropertyById(int id)
        {
            var res = Query<Property>.EQ(p => p.Id, id);
            return _db.GetCollection<Property>("Properties").FindSync(new BsonDocument()).First();
        }

        public void UpdateProperty(int id, Property property)
        {
            throw new NotImplementedException();
        }
    }

    public class InMemoryPropertyService : IPropertyService
    {
        private List<Property> _list;

        public InMemoryPropertyService()
        {
            _list = new List<Property>()
            {
            new Property() { Id = 15126, Latitude = 32.789891, Longitude = -96.798593, Price = 9500, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } },
            new Property() { Id = 13704, Latitude = 32.778781, Longitude = -96.798583, Price = 5000, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } },
            new Property() { Id = 15503, Latitude = 32.767671, Longitude = -96.798573, Price = 1800, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } },
            new Property() { Id = 15324, Latitude = 32.756561, Longitude = -96.798563, Price = 4900, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } }
            };
        }

        public Property GetPropertyById(int id)
        {
            return _list.FirstOrDefault(item=>item.Id==id);
        }

        public IEnumerable<Property> GetAllProperties()
        {
            return _list;
        }

        public IEnumerable<Property> GetPropertiesByCity(object city)
        {
            return null;
        }

        public void AddProperty(Property property)
        {
            throw new NotImplementedException();
        }

        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateProperty(int id, Property property)
        {
            throw new NotImplementedException();
        }
    }
}
