using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

using IdentityService.Domain;
using IdentityService.Domain.Models;
using IdentityService.Persistence.DTOs;

namespace IdentityService.Persistence
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IDbConnection _dbConnection;

        public TokenRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<RefreshToken> GetToken(int id)
        {
            const string query = "SELECT t.*, u.* " +
                                 "FROM [Tokens] t " +
                                 "INNER JOIN [Users] u ON u.UserId = t.UserId " +
                                 "WHERE t.[Id] = @Id";

            var tokens = await _dbConnection.QueryAsync<RefreshTokenDto, UserDto, RefreshToken>(
                query,
                ((dto, userDto) => dto.ToRefreshToken(userDto.ToUser())),
                new { Id = id });

            return tokens.SingleOrDefault();
        }

        public Task RemoveToken(int id)
        {
            const string query = "DELETE *" +
                                 "FROM [Tokens] t " +
                                 "WHERE t.[Id] = @Id";

            return _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task CreateToken(RefreshToken token)
        {
            token.Created = DateTime.UtcNow;

            var tokenDto = token.ToRefreshTokenDto();
            await _dbConnection.ExecuteAsync(
                "INSERT INTO [Tokens] " +
                "([UserId], [Value], [Created]) " +
                "VALUES " +
                "(@UserId, @Value, @Created)",
                new
                {
                    tokenDto.UserId,
                    tokenDto.Value,
                    tokenDto.Created
                });
        }
    }
}
