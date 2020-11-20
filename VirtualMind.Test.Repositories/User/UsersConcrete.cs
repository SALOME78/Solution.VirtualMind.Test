using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VirtualMind.Test.Data;
using VirtualMind.Test.Interface;
using VirtualMind.Test.Model;
using VirtualMind.Test.Model.ViewModels;

namespace VirtualMind.Test.Repositories
{
    public class UsersConcrete : IUsers
    {
        private readonly ApplicationDbContext _context;
        public UsersConcrete(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Users> GetAllUsers()
        {
            var result = (from user in _context.Users
                          select user).ToList();

            return result;
        }
        public bool AuthenticateUsers(string username, string password)
        {
            var result = (from user in _context.Users
                          where user.UserName == username && user.Password == password
                          select user).Count();

            return result > 0 ? true : false;
        }
        public LoginResponse GetUserDetailsbyCredentials(string username)
        {
            try
            {
                var result = (from user in _context.Users
                              where user.UserName == username
                              select new LoginResponse
                              {
                                  IDUser = user.ID,
                                  IDRole = 1,
                                  Status = true,
                                  UserName = user.UserName
                              }).SingleOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
