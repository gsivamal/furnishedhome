using MongoDB.Bson;
using System.Collections.Generic;

namespace FurnishedHome.Entities
{
    public class Property
    {
        public ObjectId Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Price { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public bool Furnished { get; set; }

        public int Area { get; set; }

        public bool Pets { get; set; }

        public bool Smoking { get; set; }

        public int Zipcode { get; set; }

        public List<Rent> Rents { get; set; }
    }
}
