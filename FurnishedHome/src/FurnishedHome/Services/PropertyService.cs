using System.Collections.Generic;
using System.Linq;

namespace FurnishedHome.Services
{
    public interface IPropertyService
    {
        Property GetPropertyById(long id);
        IEnumerable<Property> GetAllProperties();
        IEnumerable<Property> GetPropertiesByCity(object city);
    }

    public class PropertyService : IPropertyService
    {
        private List<Property> _list;
        public PropertyService()
        {
            _list = new List<Property>()
            {
            new Property() { Id = 15126, Latitude = 32.789891, Longitude = -96.798593, Price = 9500, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } },
            new Property() { Id = 13704, Latitude = 32.778781, Longitude = -96.798583, Price = 5000, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } },
            new Property() { Id = 15503, Latitude = 32.767671, Longitude = -96.798573, Price = 1800, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } },
            new Property() { Id = 15324, Latitude = 32.756561, Longitude = -96.798563, Price = 4900, Area = 2000, Bathrooms = 2, Bedrooms = 3, Furnished = false, Pets = false, Smoking = false, Zipcode = 75001, Rents = new List<Rent>() { new Rent() { Id = 0, Monthly = 9500, Term = "1month" } } }
            };
        }

        public Property GetPropertyById(long id)
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
    }

    public class Property
    {
        public long Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Price { get; set; }

        public byte Bedrooms { get; set; }

        public byte Bathrooms { get; set; }

        public bool Furnished { get; set; }

        public uint Area { get; set; }

        public bool Pets { get; set; }

        public bool Smoking { get; set; }

        public uint Zipcode { get; set; }

        public List<Rent> Rents { get; set; }
    }

    public class Rent
    {
        public int Id { get; set; }

        public int Monthly { get; set; }

        public string Term { get; set; }
    }
}
