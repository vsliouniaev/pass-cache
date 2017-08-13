using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PassCache.Middleware
{
    public class EnforceHttps
    {
        private readonly RequestDelegate _next;

        public EnforceHttps(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.IsHttps)
            {
                context.Response.Redirect($"https://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}", true);
                return;
            }

            await _next(context);
        }
    }
}
