using CourseFlow.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Net;

namespace CourseFlow.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = new OleDbConnection(_connectionString))
            using (var command = new OleDbCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [User] WHERE Username = @Username AND [Password] = @Password";
                command.Parameters.AddWithValue("@Username", credential.UserName);
                command.Parameters.AddWithValue("@Password", credential.Password);
                validUser = command.ExecuteScalar() != null;
            }
            return validUser;
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
            using (var connection = new OleDbConnection(_connectionString))
            using (var command = new OleDbCommand())
            {
                connection.Open();
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
                            Password = string.Empty,
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Role = reader["Role"].ToString(),
                            ProfilePicture = reader["ProfilePicture"].ToString()
                        };
                    }
                }
            }
            return user;
        }
    }
}
