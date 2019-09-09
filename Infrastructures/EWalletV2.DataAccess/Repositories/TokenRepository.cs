using EWalletV2.DataAccess.Contexts;
using EWalletV2.Domain.Entity;
using EWalletV2.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWalletV2.DataAccess.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext _context;
        public TokenRepository(DataContext context)
        {
            _context = context;
        }

        public void AddToken(TokenEntity tokenEntity)
        {
            
                _context.Add(tokenEntity);
                _context.SaveChanges();
               
    
        }

        public bool DeleteTokenByEmail(string email)
        {
            var entity = _context.Tokens.Where(t => t.Email == email).FirstOrDefault();
            try
            {
                _context.Tokens.Remove(entity);
                return true;
              
            }
            catch (Exception)
            {

              return  false;
            }
            
        }

        public TokenEntity GetTokenByEmail(string email)
        {
            return _context.Tokens.FirstOrDefault(x => x.Email == email);
        }

        public void UpdateToken(TokenEntity tokenEntity)
        {
          
                int id = tokenEntity.Id;
            TokenEntity entity = _context.Tokens.AsTracking().FirstOrDefault(x => x.Id == tokenEntity.Id);          

            _context.Entry(entity).CurrentValues.SetValues(tokenEntity);
                _context.SaveChanges();
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
    }
}
