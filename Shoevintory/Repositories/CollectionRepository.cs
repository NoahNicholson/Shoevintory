using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Shoevintory.Models;
using Shoevintory.Utils;
using System;
using Collection = Shoevintory.Models.Collection;

namespace Shoevintory.Repositories
{
    public interface ICollectionRepository { int Create(Collection collection); }
    public class CollectionRepository : ICollectionRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public CollectionRepository(IConfiguration config)
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
        public int Create(Collection collection)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO Collection
                                            (Name, UserProfileId) 
                                        OUTPUT INSERTED.ID
                                            VALUES(  @name, @userProfileId)";

                    //DbUtils.AddParameter(cmd, "@id", userProfile.Id);

                    DbUtils.AddParameter(cmd, "@name", collection.Name);
                    DbUtils.AddParameter(cmd, "@userProfileId", collection.UserProfileId);



                    collection.Id = (int)cmd.ExecuteScalar();

                }
            }
            return collection.Id;
        }

    }
}

