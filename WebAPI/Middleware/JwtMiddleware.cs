using Application.Services.JWT;
using Domain.Interfaces.JWT;

namespace WebAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
        {
            var authService = serviceProvider.GetRequiredService<IAuthService>();

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                context.Request.Cookies.TryGetValue("access_token", out token);
            }

            if (token != null)
            {
                var userPrincipal = authService.ValidateToken(token);
                if (userPrincipal != null)
                {
                    context.User = userPrincipal;
                }
            }

            await _next(context);
        }
    }
}
