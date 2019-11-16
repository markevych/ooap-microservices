using System;
using Common.Domain.Models;

namespace Common.Domain
{
    public interface ITokenRepository
    {
        RefreshToken GetById(Guid id);
    }
}
