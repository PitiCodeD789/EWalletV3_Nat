using EWalletV2.DataAccess.Contexts;
using EWalletV2.Domain.Entities;
using EWalletV2.Domain.Repoitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWalletV2.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public string AddUserAndGetAccount(UserEntity userData)
        {
            try
            {
                _context.Add(userData);

                UserEntity user = _context.Users.FirstOrDefault(x => x.Email == userData.Email);
                string account = GenerateUserAccount(user.Id);
                user.Account = account;

                _context.SaveChanges();

                return account;
            }
            catch (Exception)
            {

                return null;
            }
        }

        private string GenerateUserAccount(int id)
        {
            return "01" + id.ToString("D8");
        }

        public bool ChangeBalance(string email, decimal amount)
        {
            try
            {
                UserEntity user = _context.Users.FirstOrDefault(x => x.Email == email);
                user.Balance = amount;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public UserEntity GetUserByAccountNumber(string merchantAccNo)
        {
            UserEntity user = _context.Users.FirstOrDefault(x => x.Account == merchantAccNo.ToLower());
            return user;
        }

        public UserEntity GetUserByEmail(string email)
        {
            UserEntity user = _context.Users.FirstOrDefault(x => x.Email == email.ToLower());
            return user;
        }

        public bool Update(UserEntity userData)
        {
            try
            {
                //UserEntity user = _context.Users.FirstOrDefault(x => x.Email == userData.Email.ToLower());
                //user.FirstName = userData.FirstName;
                //user.LastName = userData.LastName;
                //user.MobileNumber = userData.MobileNumber;
                //user.BirthDate = userData.BirthDate;
                //user.Gender = userData.Gender;
                //user.Pin = userData.Pin;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
