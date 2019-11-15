using System;
using System.Threading.Tasks;

namespace Common.Auth
{
    public interface ISecurityService
    {
        string GenerateAccessToken(int userId, string email, string userRole, TimeSpan expirationDuration);
        Task<bool> VerifyToken(string token, int userId);
        string EncryptPassword(string password, string salt);
        string DecryptPassword(string passwordHash, string salt);
    }
}
