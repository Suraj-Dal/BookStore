using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class AddressRL : IAddressRL
    {
        public AddressRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        List<GetAddressModel> address = new List<GetAddressModel>();

        public bool AddAddress(AddressModel addressModel, int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoAddressTable", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@Address", addressModel.Address);
                    sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                    sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                    sqlCommand.Parameters.AddWithValue("@TypeID", addressModel.TypeID);
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

        public bool UpdateAddress(AddressModel addressModel, int AddressID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateAddressTable", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@Address", addressModel.Address);
                    sqlCommand.Parameters.AddWithValue("@City", addressModel.City);
                    sqlCommand.Parameters.AddWithValue("@State", addressModel.State);
                    sqlCommand.Parameters.AddWithValue("@AddressID", AddressID);

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

        public IEnumerable<GetAddressModel> GetAddress(int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    sqlConnection.Open();
                    String query = "SELECT AddressID, Address, City, State, TypeID FROM AddressTable WHERE UserID = '" + UserID + "'";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        address.Add(new GetAddressModel()
                        {
                            AddressID = sqlDataReader["AddressID"].ToString(),
                            Address = sqlDataReader["Address"].ToString(),
                            City = sqlDataReader["City"].ToString(),
                            State = sqlDataReader["State"].ToString(),
                            TypeID = sqlDataReader["TypeID"].ToString()
                        });
                    }
                    return address;
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
