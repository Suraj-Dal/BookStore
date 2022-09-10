using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class FeedbackRL : IFeedbackRL
    {
        public FeedbackRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        List<GetFeedbackModel> feedback = new List<GetFeedbackModel>();

        public bool AddFeedback(FeedbackModel feedbackModel, int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoFeedbackTable", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@rating", feedbackModel.rating);
                    sqlCommand.Parameters.AddWithValue("@Comment", feedbackModel.Comment);
                    sqlCommand.Parameters.AddWithValue("@BookID", feedbackModel.BookID);
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

        public IEnumerable<GetFeedbackModel> GetFeedback(int UserID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
                try
                {
                    sqlConnection.Open();
                    String query = "SELECT FeedbackID, rating, Comment, BookID FROM FeedbackTable WHERE UserID = '" + UserID + "'";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        feedback.Add(new GetFeedbackModel()
                        {
                            FeedbackID = sqlDataReader["FeedbackID"].ToString(),
                            rating = sqlDataReader["rating"].ToString(),
                            Comment = sqlDataReader["Comment"].ToString(),
                            BookID = sqlDataReader["BookID"].ToString()

                        });
                    }
                    return feedback;
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
