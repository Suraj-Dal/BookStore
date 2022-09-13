using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrderRL : IOrderRL
    {
        public OrderRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        List<GetOrderModel> order = new List<GetOrderModel>();

        public bool AddOrder(OrderModel orderModel, int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoOrderTable", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@CartID", orderModel.CartID);
                    sqlCommand.Parameters.AddWithValue("@AddressID", orderModel.AddressID);
                    sqlCommand.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString());
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

        public bool CancelOrder(int OrderID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.DeleteFromOrderTable", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@OrderID", OrderID);

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

        public IEnumerable<GetOrderModel> GetAllOrders()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    sqlConnection.Open();
                    String query = "SELECT OrderID, OrderQty, AddressID, BookID, UserID, TotalPrice, DateTime FROM OrderTable";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        order.Add(new GetOrderModel()
                        {
                            OrderID = sqlDataReader["BookID"].ToString(),
                            OrderQty = sqlDataReader["OrderQty"].ToString(),
                            AddressID = sqlDataReader["AddressID"].ToString(),
                            BookID = sqlDataReader["BookID"].ToString(),
                            UserID = sqlDataReader["UserID"].ToString(),
                            TotalPrice = sqlDataReader["TotalPrice"].ToString(),
                            DateTime = sqlDataReader["DateTime"].ToString()
                        });
                    }
                    return order;
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
