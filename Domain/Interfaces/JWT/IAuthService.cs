using System.Security.Claims;

namespace Domain.Interfaces.JWT
{
    public interface IAuthService
    {
        ClaimsPrincipal ValidateToken(string token);
        public string GenerateToken();
        public string GenerateRefreshToken();
    }
}
