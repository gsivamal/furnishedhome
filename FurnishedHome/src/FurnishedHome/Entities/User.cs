using MongoDB.Bson;

namespace FurnishedHome.Entities
{
    public class User
    {
        public ObjectId Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
