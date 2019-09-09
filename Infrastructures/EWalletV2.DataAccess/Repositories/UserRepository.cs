using EWalletV2.Api.ViewModels;
using EWalletV2.DataAccess.Contexts;
using EWalletV2.Domain.Entities;
using EWalletV2.Domain.Repositories;
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
                _context.SaveChanges();

                UserEntity user = _context.Users.FirstOrDefault(x => x.Email == userData.Email);
                string account = GenerateUserAccount(user.Id);
                user.Account = account;
                user.UpdateDateTime = DateTime.UtcNow;

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
            return (int)EW_Enumerations.EW_UserTypeEnum.Customer > 9 ? "" : "0"  + id.ToString("D8");
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
