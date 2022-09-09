using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class WishlistRL : IWishlistRL
    {
        public WishlistRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        List<WishlistModel> wishlist = new List<WishlistModel>();

        public bool AddToWishlist(int BookID, int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@BookID", BookID);
                    sqlCommand.Parameters.AddWithValue("@UserID", UserID);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
        public bool DeleteWishlist(int WishlistID, int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.DeleteFromWishlist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@WishlistID", WishlistID);
                    sqlCommand.Parameters.AddWithValue("@UserID", UserID);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

        public IEnumerable<WishlistModel> GetWishlist(int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    sqlConnection.Open();
                    String query = "SELECT WishlistID, BookID FROM Wishlist WHERE UserID = '" + UserID + "'";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        wishlist.Add(new WishlistModel()
                        {
                            WishlistID = sqlDataReader["WishlistID"].ToString(),
                            BookID = sqlDataReader["BookID"].ToString()

                        });
                    }
                    return wishlist;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }

        }
    }
}
