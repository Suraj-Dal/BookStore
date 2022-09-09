using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class BookRL : IBookRL
    {
        public BookRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        List<GetBookModel> books = new List<GetBookModel>();

        public bool AddBook(BookModel bookModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoBookTable", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@rating", bookModel.rating);
                    sqlCommand.Parameters.AddWithValue("@PeopleRated", bookModel.PeopleRated);
                    sqlCommand.Parameters.AddWithValue("@Price", bookModel.Price);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@Description", bookModel.Description);
                    sqlCommand.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);

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

        public bool UpdateBook(BookModel bookModel, int BookID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateBookTable", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    
                    sqlCommand.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    sqlCommand.Parameters.AddWithValue("@rating", bookModel.rating);
                    sqlCommand.Parameters.AddWithValue("@PeopleRated", bookModel.PeopleRated);
                    sqlCommand.Parameters.AddWithValue("@Price", bookModel.Price);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@Description", bookModel.Description);
                    sqlCommand.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    sqlCommand.Parameters.AddWithValue("@BookImage", bookModel.BookImage);
                    sqlCommand.Parameters.AddWithValue("@BookID", BookID);

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

        public bool DeleteBook(int BookID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.DeleteFromBookTable", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookID", BookID);

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

        public IEnumerable<GetBookModel> GetAllBooks()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    sqlConnection.Open();
                    String query = "SELECT BookID, BookName, AuthorName, rating, PeopleRated, Price, DiscountPrice, Description, Quantity, BookImage FROM BookTable";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        books.Add(new GetBookModel() {BookID = sqlDataReader["BookID"].ToString(),
                            BookName = sqlDataReader["BookName"].ToString(),
                            AuthorName = sqlDataReader["AuthorName"].ToString(),
                            rating = sqlDataReader["rating"].ToString(),
                            PeopleRated = sqlDataReader["PeopleRated"].ToString(),
                            Price = sqlDataReader["Price"].ToString(),
                            DiscountPrice = sqlDataReader["DiscountPrice"].ToString(),
                            Description = sqlDataReader["Description"].ToString(),
                            Quantity = sqlDataReader["Quantity"].ToString(),
                            BookImage = sqlDataReader["BookImage"].ToString()
                        });
                    }
                    return books;
                }
                catch (Exception)
                {
                    throw;
                }
        }

        public IEnumerable<GetBookModel> GetBookById(int BookID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.GetBook", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookID", BookID);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        books.Add(new GetBookModel()
                        {
                            BookID = sqlDataReader["BookID"].ToString(),
                            BookName = sqlDataReader["BookName"].ToString(),
                            AuthorName = sqlDataReader["AuthorName"].ToString(),
                            rating = sqlDataReader["rating"].ToString(),
                            PeopleRated = sqlDataReader["PeopleRated"].ToString(),
                            Price = sqlDataReader["Price"].ToString(),
                            DiscountPrice = sqlDataReader["DiscountPrice"].ToString(),
                            Description = sqlDataReader["Description"].ToString(),
                            Quantity = sqlDataReader["Quantity"].ToString(),
                            BookImage = sqlDataReader["BookImage"].ToString()
                        });
                    }
                    return books;
                }
                catch (Exception)
                {
                    throw;
                }
        }
    }
}
