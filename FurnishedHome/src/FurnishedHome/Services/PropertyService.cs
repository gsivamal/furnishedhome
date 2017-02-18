using FurnishedHome.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FurnishedHome.Services
{
    public interface IPropertyService
    {
        void AddProperty(Property property);
        Property GetPropertyById(ObjectId id);
        IEnumerable<Property> GetAllProperties();
        IEnumerable<Property> GetPropertiesByCity(object city);
        void DeleteProperty(ObjectId id);
        void UpdateProperty(ObjectId id, Property property);
    }

    public class MongoPropertyService : IPropertyService
    {
        private readonly IMongoCollection<Property> _collection;

        public MongoPropertyService(IMongoCollection<Property> collection)
        {
            _collection = collection;
        }
        public void AddProperty(Property property)
        {
            _collection.InsertOne(property);
        }

        public void DeleteProperty(ObjectId id)
        {
            _collection.DeleteOne(p => p.Id == id);
        }

        public IEnumerable<Property> GetAllProperties()
        {
            return _collection.FindSync(new BsonDocument()).ToEnumerable();
        }

        public IEnumerable<Property> GetPropertiesByCity(object city)
        {
            throw new NotImplementedException();
        }

        public Property GetPropertyById(ObjectId id)
        {
            return _collection.FindSync(p => p.Id == id).FirstOrDefault();
        }

        public void UpdateProperty(ObjectId id, Property property)
        {
            _collection.ReplaceOne(p=>p.Id==id,property);
        }
    }
/*
    public class InMemoryPropertyService : IPropertyService<int>
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
            return _list.FirstOrDefault(item => item.Id == id);
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
    }*/
}
