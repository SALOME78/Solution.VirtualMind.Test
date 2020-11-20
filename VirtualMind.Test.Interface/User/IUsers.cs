using System;
using System.Collections.Generic;
using System.Text;
using VirtualMind.Test.Model;
using VirtualMind.Test.Model.ViewModels;

namespace VirtualMind.Test.Interface
{
    public interface IUsers
    {
        List<Users> GetAllUsers();
        bool AuthenticateUsers(string username, string password);
        LoginResponse GetUserDetailsbyCredentials(string username);
    }
}
