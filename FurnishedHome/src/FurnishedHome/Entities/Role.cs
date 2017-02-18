using MongoDB.Bson;
using System.Collections.Generic;

namespace FurnishedHome.Entities
{
    public class Role
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
