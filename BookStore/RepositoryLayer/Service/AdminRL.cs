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
    public class AdminRL : IAdminRL
    {
        public AdminRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        SqlConnection sqlConnection;

        //public bool Register(AdminModel adminDetails)
        //{
        //    sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
        //    using (sqlConnection)
        //        try
        //        {
        //            var password = Encryption(adminDetails.Password);
        //            SqlCommand sqlCommand = new SqlCommand("dbo.InsertIntoAdminTable", sqlConnection);

        //            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

        //            sqlConnection.Open();

        //            sqlCommand.Parameters.AddWithValue("@FullName", adminDetails.AdminName);
        //            sqlCommand.Parameters.AddWithValue("@EmailID", adminDetails.EmailID);
        //            sqlCommand.Parameters.AddWithValue("@Password", password);
        //            sqlCommand.Parameters.AddWithValue("@Phone", adminDetails.AdminPhone);

        //            int result = sqlCommand.ExecuteNonQuery();
        //            if (result > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            sqlConnection.Close();
        //        }
        //}

        public string AdminLogin(LoginModel loginModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("dbo.AdminLogin", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();

                    command.Parameters.AddWithValue("@EmailID", loginModel.EmailID);
                    command.Parameters.AddWithValue("@Password", loginModel.Password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT AdminID FROM AdminTable WHERE EmailID = '" + result + "'";
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

        private string GenerateSecurityToken(string Email, string UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Role,"Admin"),
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
