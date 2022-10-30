using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shoevintory.Models;
using Shoevintory.Utils;
using System.Collections.Generic;

namespace Shoevintory.Repositories
{

    public interface IShoeCollectionRepository
    {
        int Create(ShoeCollection shoeCollection);
    }

    public class ShoeCollectionRepository : IShoeCollectionRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public ShoeCollectionRepository(IConfiguration config)
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
        public int Create(ShoeCollection shoeCollection)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO ShoeCollection
                                            (ShoeId, CollectionId) 
                                        OUTPUT INSERTED.ID
                                            VALUES(  @shoeId, @collectionId)";

                    //DbUtils.AddParameter(cmd, "@id", userProfile.Id);

                    DbUtils.AddParameter(cmd, "@shoeId", shoeCollection.ShoeId);
                    DbUtils.AddParameter(cmd, "@collectionId", shoeCollection.CollectionId);

                    shoeCollection.Id = (int)cmd.ExecuteScalar();

                }
            }
            return shoeCollection.Id;
        }
    }
}



