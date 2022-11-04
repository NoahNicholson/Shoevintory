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
        public List<UserShoeViewModel> GetAllUserShoes(int collectionId);
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
                                            (ShoeId, CollectionId, Size, Quantity, PurchaseDate, PurchasePrice) 
                                        OUTPUT INSERTED.ID
                                            VALUES(  @shoeId, @collectionId, @size, @quantity, @purchaseDate, @purchasePrice)";

                    //DbUtils.AddParameter(cmd, "@id", userProfile.Id);

                    DbUtils.AddParameter(cmd, "@shoeId", shoeCollection.ShoeId);
                    DbUtils.AddParameter(cmd, "@collectionId", shoeCollection.CollectionId);
                    DbUtils.AddParameter(cmd, "@size", shoeCollection.Size);
                    DbUtils.AddParameter(cmd, "@quantity", shoeCollection.Quantity);
                    DbUtils.AddParameter(cmd, "@purchaseDate", shoeCollection.PurchaseDate);
                    DbUtils.AddParameter(cmd, "@purchasePrice", shoeCollection.PurchasePrice);
                    shoeCollection.Id = (int)cmd.ExecuteScalar();

                }
            }
            return shoeCollection.Id;
        }
        public List<UserShoeViewModel> GetAllUserShoes(int collectionId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT s.Id, Name, ImageUrl, Retail, sc.Size, Quantity, PurchaseDate, PurchasePrice
                    
                    FROM Shoe s
                    JOIN ShoeCollection sc ON s.Id = sc.ShoeId
                    WHERE sc.CollectionId = @CollectionId
                     ";

                    DbUtils.AddParameter(cmd, "@CollectionId", collectionId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<UserShoeViewModel> usershoes = new List<UserShoeViewModel>();
                        while (reader.Read())
                        {
                            UserShoeViewModel usershoe = new UserShoeViewModel
                            {


                                ImageUrl = DbUtils.GetNullableString(reader, "ImageUrl"),
                                ShoeId = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Retail = DbUtils.GetDecimal(reader, "Retail"),
                                Size = DbUtils.GetInt(reader, "Size"),
                                Quantity = DbUtils.GetInt(reader, "Quantity"),
                                PurchaseDate = DbUtils.GetDateTime(reader, "PurchaseDate"),
                                PurchasePrice = DbUtils.GetDecimal(reader, "PurchasePrice"),
                                CollectionId = collectionId
                            };
                            usershoes.Add(usershoe);
                        }

                        return usershoes;
                    }
                }
            }
        }
    }
}



