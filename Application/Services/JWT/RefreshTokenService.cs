using Domain.Entities;
using Domain.Entities.JWT;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Services.JWT
{
    public class RefreshTokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthService _authService;

        public RefreshTokenService(JwtSettings jwtSettings, IUnitOfWork unitOfWork, AuthService authService)
        {
            _jwtSettings = jwtSettings;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<string> GenerateRefreshToken( string token)
        {
            try
            {
                var refreshToken = new historico_refresh_token
                {
                    token = token,
                    refresh_token = _authService.GenerateRefreshToken(),
                    fecha_creacion = DateTime.Now,
                    fecha_expiracion = DateTime.Now.AddHours(24)
                };

                 _unitOfWork.historico_Refresh_TokenRepository.Add(refreshToken);
                await _unitOfWork.SaveChangesAsync();
                return refreshToken.refresh_token;
            }
            catch (Exception ex)
            {
                throw new Exception($"|Error al generar el token de actualización: {ex.Message}", ex);
            }

        }

        public async Task<bool> ValidateRefreshToken(string refreshToken, string token)
        {
            try
            {

                var existingToken = await _unitOfWork.historico_Refresh_TokenRepository.GetByTokenAsync(refreshToken);

                if (existingToken == null || existingToken.token != token || !existingToken.activo)
                {
                    return false;
                }
                #region ValidarTokenExpiro
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                    ClockSkew = TimeSpan.Zero //  elimina la tolerancia de tiempo
                };

                var expiro = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                if (expiro.Identity.IsAuthenticated == true)
                {
                    throw new SecurityTokenExpiredException("|El token No ha expirado");
                }


                #endregion

                return true;
            }
            catch (SecurityTokenExpiredException)
            {

                Console.WriteLine("El token ha expirado");
                return true;
            }

            catch (Exception ex)
            {
                return false; // El token no es válido por otras razones
            }
        }
    }
}
