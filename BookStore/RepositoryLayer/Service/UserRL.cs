using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;

        public bool Register(UserRegistrationModel userDetails)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using(sqlConnection)
                try
                {
                    var password = Encryption(userDetails.Password);
                    SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoUserTable", sqlConnection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@FullName", userDetails.FullName);
                    sqlCommand.Parameters.AddWithValue("@EmailID", userDetails.EmailID);
                    sqlCommand.Parameters.AddWithValue("@Password", password);
                    sqlCommand.Parameters.AddWithValue("@Phone", userDetails.Phone);

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

        public string UserLogin(LoginModel loginModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("dbo.UserLogin", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    var password = Encryption(loginModel.Password);
                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@EmailID", loginModel.EmailID);
                    command.Parameters.AddWithValue("@Password", password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT UserID FROM UserTable WHERE EmailID = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        var Id = cmd.ExecuteScalar();
                        var token = GenerateSecurityToken(loginModel.EmailID, Id.ToString());
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public bool ResetPassword(ResetModel resetModel, string EmailID)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using(sqlConnection)
            {
                try
                {
                    if (resetModel.Password.Equals(resetModel.Confirm))
                    {
                        SqlCommand command = new SqlCommand("dbo.ResetPassword", sqlConnection);
                        command.CommandType = CommandType.StoredProcedure;
                        var password = Encryption(resetModel.Password);
                        sqlConnection.Open();
                        command.Parameters.AddWithValue("@EmailID", EmailID);
                        command.Parameters.AddWithValue("@Password", password);
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return false;
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

        public string ForgetPassword(string Email)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
            {
                try
                {
                    sqlConnection.Open();
                    string query = "SELECT EmailID FROM UserTable WHERE EmailID = '" + Email + "'";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    var email = cmd.ExecuteScalar();
                    string query1 = "SELECT UserID FROM UserTable WHERE EmailID = '" + Email + "'";
                    SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                    var id = sqlCommand.ExecuteScalar();
                    if (email != null)
                    {
                        var token = GenerateSecurityToken(email.ToString(), id.ToString());
                        MSQModel msqModel = new MSQModel();
                        msqModel.sendData2Queue(token);
                        return token;
                    }
                    else
                        return null;
                    
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

        private string GenerateSecurityToken(string Email, string UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Email,Email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(Configuration["AppSettings:Key"],
              Configuration["AppSettings:Key"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string Encryption(string password)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string Decryption(string encryptedPass)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(encryptedPass))
            {
                return "";
            }
            var encodeBytes = Convert.FromBase64String(encryptedPass);
            var result = Encoding.UTF8.GetString(encodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}
