using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Shoevintory.Models;
using Shoevintory.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shoe = Shoevintory.Models.Shoe;

namespace Shoevintory.Repositories
{
    public interface IShoeRepository { int Create(Shoe shoe);
        public List<Shoe> GetAllShoes(int collectionId);
        public List<Shoe> GetAllShoes();
        public int Edit(Shoe shoe);
        public void Delete(int Id);
        public Shoe GetShoeById(int shoeId);
    }

    public class ShoeRepository : IShoeRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public ShoeRepository(IConfiguration config)
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
        public int Create(Shoe shoe)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO Shoe
                                            (Name, ImageUrl, Retail) 
                                        OUTPUT INSERTED.ID
                                            VALUES(  @name, @imageUrl, @retail)";

                    

                    DbUtils.AddParameter(cmd, "@name", shoe.Name);
                    DbUtils.AddParameter(cmd, "@imageUrl", shoe.ImageUrl);
                    DbUtils.AddParameter(cmd, "@retail", shoe.Retail);


                    shoe.Id = (int)cmd.ExecuteScalar();
                    

                }
            }
            return shoe.Id;
        }
        public Shoe GetShoeById(int shoeId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT s.Id, Name, ImageUrl, Retail
                    
                    FROM Shoe s
                  
                    WHERE s.Id = @ShoeId
                     ";


                    DbUtils.AddParameter(cmd, "@ShoeId", shoeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Shoe shoe = new Shoe();
                        while (reader.Read())
                        {
                            shoe = new Shoe
                            {

                              
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                Retail = DbUtils.GetDecimal(reader, "Retail"),
                            };


                           
                        }
                        return shoe;
                    }
                }
            }
        }
                    public void Delete(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        DELETE FROM Shoe
                                        WHERE Id = @Id
                        ";                                       

                   

                    DbUtils.AddParameter(cmd, "@id", Id);

                    cmd.ExecuteNonQuery();





                }
            }
        
        }
        public int Edit(Shoe shoe)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        UPDATE Shoe
                                       SET   Name = @name, Retail = @retail , ImageUrl =@imageUrl
                                       WHERE Id = @Id ";

                    DbUtils.AddParameter(cmd, "@id", shoe.Id);

                    DbUtils.AddParameter(cmd, "@name", shoe.Name);
                    DbUtils.AddParameter(cmd, "@retail", shoe.Retail);
                    DbUtils.AddParameter(cmd, "@imageUrl", shoe.ImageUrl);
                    



                    cmd.ExecuteNonQuery();


                }
            }
            return shoe.Id;
        }
        public List<Shoe> GetAllShoes(int collectionId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT s.Id, Name, ImageUrl, Retail
                    
                    FROM Shoe s
                    JOIN ShoeCollection sc ON s.Id = sc.ShoeId
                    WHERE sc.CollectionId = @CollectionId
                     ";

                    DbUtils.AddParameter(cmd, "@CollectionId", collectionId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Shoe> shoes = new List<Shoe>();
                        while (reader.Read())
                        {
                            Shoe shoe = new Shoe
                            {

                               
                                ImageUrl = DbUtils.GetNullableString(reader, "ImageUrl"),
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Retail = DbUtils.GetDecimal(reader, "Retail"),
                               
                            };
                            shoes.Add(shoe);
                        }

                        return shoes;
                    }
                }
            }
        }
        public List<Shoe> GetAllShoes()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                         SELECT s.Id, Name, ImageUrl, Retail
                    
                    FROM Shoe s
                    
                     ";

                    

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Shoe> shoes = new List<Shoe>();
                        while (reader.Read())
                        {
                            Shoe shoe = new Shoe
                            {

                              
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                Retail = DbUtils.GetDecimal(reader, "Retail"),
                               
                            };
                            shoes.Add(shoe);
                        }

                        return shoes;
                    }
                }
            }
        }
    }
}