using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.Entity;

namespace EWalletV2.Domain.Repositories
{
    public interface ITokenRepository
    {
        TokenEntity GetTokenByEmail(string email);
        void AddToken(TokenEntity tokenEntity);
        void UpdateToken(TokenEntity tokenEntity);
        bool DeleteTokenByEmail(string email);
    }
}
