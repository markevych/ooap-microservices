using System;
using System.Collections.Generic;
using System.Security.Claims;
using Common.Domain.Models;

namespace Common.Domain.Interfaces.Security
{
    public interface ISecurityService
    {
        string GenerateAccessToken(User user, string userRole, TimeSpan expirationDuration);
        int FetchUserId(IEnumerable<Claim> claims);
        string EncryptPassword(string password, string salt);
        string DecryptPassword(string passwordHash, string salt);
    }
}
