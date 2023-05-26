using System.Collections.Generic;
using System.Net;
using CourseFlow.Models;

namespace CourseFlow.Repositories
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);

        void Add(UserModel userModel);

        void Edit(UserModel userModel);

        void Remove(int id);

        UserModel GetById(int id);

        UserModel GetByUsername(string username);

        string GetSaltByUsername(string username);

        IEnumerable<UserModel> GetByAll();

        // ...
    }
}
