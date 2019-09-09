using EWalletV2.DataAccess.Context;
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

        public UserEntity GetUserByAccountNumber(string merchantAccNo)
        {
            throw new NotImplementedException();
        }

        public UserEntity GetUserByEmail(string email)
        {
            UserEntity user = _context.User.FirstOrDefault(x => x.Email == email.ToLower());
            return user;
        }
    }
}
