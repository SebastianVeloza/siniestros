using Application.Services.JWT;
using Domain.Entities.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly IMediator _mediator;
        private readonly JwtSettings _jwtSettings;

        public TokenController(AuthService authService, RefreshTokenService refreshTokenService, IMediator mediator, JwtSettings jwtSettings)
        {
            _authService = authService;
            _refreshTokenService = refreshTokenService;
            _mediator = mediator;
            _jwtSettings = jwtSettings;
        }


        /// <summary>
        /// Login para empleado.
        /// </summary>
        /// <param name="login"> Usuario para ingresar sesion.</param>
        /// <param name="Password">Contraseña para ingresar sesion.</param>
        [HttpPost]
        [Route("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> GetByLogin()
        {
            

            var token = _authService.GenerateToken();
            var refreshToken = await _refreshTokenService.GenerateRefreshToken(token);

            #region Cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // true si es https
                SameSite = SameSiteMode.Strict, 
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes + 5) // Expiración del token
            };

            Response.Cookies.Append("access_token", token, cookieOptions);
            Response.Cookies.Append("refresh_token", refreshToken, cookieOptions);

            #endregion

            return Ok("Token Generado correctamente");
        }

        /// <summary>
        /// Refrescar el token.
        /// </summary>
        /// <param name="request">token y refresh token.</param>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var token = Request.Cookies["access_token"];
            var refresh = Request.Cookies["refresh_token"];

            var isValid = await _refreshTokenService.ValidateRefreshToken(refresh, token);

            if (!isValid)
            {
                return Unauthorized("Refresh Token invalido");
            }
            else
            {
                Response.Cookies.Delete("access_token");
                Response.Cookies.Delete("refresh_token");

            }
            #region token
            int i = 0;
            string newToken;
            string newRefreshToken;

            newToken = _authService.GenerateToken();
            newRefreshToken = await _refreshTokenService.GenerateRefreshToken( newToken);

            #region Cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false, 
                SameSite = SameSiteMode.Strict, 
                Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpiryInMinutes + 5) 
            };

            Response.Cookies.Append("access_token", newToken, cookieOptions);
            Response.Cookies.Append("refresh_token", newRefreshToken, cookieOptions);
            #endregion
            #endregion

            return Ok();
        }

        /// <summary>
        /// elimina las cookies.
        /// </summary>
        [HttpPost("logout")]
        public IActionResult Logout()
        {

            // Eliminar las cookies
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");


            return Ok(new { Message = "Tokens eliminados" });
        }
    }
}
