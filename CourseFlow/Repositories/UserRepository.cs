using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;

namespace CourseFlow.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private string SecureStringToString(SecureString securePassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand("INSERT INTO [User] (Username, [Password], FirstName, LastName, Email, Role, ProfilePicture) VALUES (@Username, @Password, @FirstName, @LastName, @Email, @Role, @ProfilePictureFilePath)", connection))
                {
                    command.Parameters.AddWithValue("@Username", userModel.Username);
                    string password = SecureStringToString(userModel.Password);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@FirstName", userModel.FirstName);
                    command.Parameters.AddWithValue("@LastName", userModel.LastName);
                    command.Parameters.AddWithValue("@Email", userModel.Email);
                    command.Parameters.AddWithValue("@Role", userModel.Role);
                    command.Parameters.AddWithValue("@ProfilePictureFilePath", userModel.ProfilePicture);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new OleDbCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [User] WHERE Username = @Username AND [Password] = @Password";
                    command.Parameters.AddWithValue("@Username", credential.UserName);
                    command.Parameters.AddWithValue("@Password", credential.Password);
                    validUser = command.ExecuteScalar() != null;
                }
                return validUser;
            }
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string UserName)
        {
            UserModel user = null;
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new OleDbCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM [User] WHERE Username = @Username";
                        command.Parameters.AddWithValue("@Username", UserName);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new UserModel()
                                {
                                    Username = reader["Username"].ToString(),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    ProfilePicture = reader["ProfilePicture"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error or throw exception as per your requirement
                throw new Exception("An error occurred while retrieving the user by username.", ex);
            }
            return user;
        }
    }
}
