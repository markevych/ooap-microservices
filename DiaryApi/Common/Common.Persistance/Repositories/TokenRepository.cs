using System;
using Common.Domain;
using Common.Domain.Models;

namespace Common.Persistence.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public RefreshToken GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
