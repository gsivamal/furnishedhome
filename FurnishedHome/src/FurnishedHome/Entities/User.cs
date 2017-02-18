using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FurnishedHome.Entities
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string SecureCode { get; set; }
        public ObjectId RoleId { get; set; }
    }
}
