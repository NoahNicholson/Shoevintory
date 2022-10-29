using Microsoft.Data.SqlClient.Server;
using System;
using System.Runtime.Intrinsics.X86;


namespace Shoevintory.Models
{
    public class UserProfile
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImageUrl { get; set; }
        public string FirebaseUserId { get; set; }
        public int UserTypeId { get; set; }
      
    }
}
