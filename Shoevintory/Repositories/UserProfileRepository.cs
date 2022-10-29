using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Shoevintory.Models;
using Shoevintory.Utils;
using System;

namespace Shoevintory.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public UserProfileRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<UserProfile> GetAllUserProfiles()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT FirebaseuserId, Id, Name, DateCreated, Email, ImageUrl, UserTypeId
                      
                     FROM UserProfile 
                        ORDER BY DisplayName ASC
               
                      
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserProfile> UserProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            UserProfile UserProfile = new UserProfile 

                            {
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId")
                            };
                            UserProfiles.Add(UserProfile);
                        }

                        return UserProfiles;
                    }
                }
            }
        }

        public UserProfile GetByFirebaseUserId(string FirebaseUserId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                   SELECT FirebaseuserId, Id, Name, DateCreated, Email, ImageUrl, UserTypeId
                      
                   FROM UserProfile 
                        
                   WHERE FirebaseUserId = @firebaseUserId
                    ";

                    DbUtils.AddParameter(cmd, "@firebaseUserId", FirebaseUserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            UserProfile UserProfile = new UserProfile
                            {
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseuserId"),
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                UserTypeId = DbUtils.GetInt(reader, "UserTypeId")
                                


                            };

                            return UserProfile;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public void Add(UserProfile userProfile)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO UserProfile
                                            (FirebaseUserId, Email, UserTypeId, Name, ImageUrl, DateCreated) 
                                        OUTPUT INSERTED.ID
                                            VALUES(@firebaseUserId, @email, @userTypeId, @name, @imageUrl, @dateCreated)";

                    //DbUtils.AddParameter(cmd, "@id", userProfile.Id);
                    DbUtils.AddParameter(cmd, "@firebaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@imageUrl", userProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@dateCreated", DateTime.Now);
                    DbUtils.AddParameter(cmd, "@userTypeId", userProfile.UserTypeId);



                    userProfile.Id = (int)cmd.ExecuteScalar();

                }
            }
        }
    }
}
