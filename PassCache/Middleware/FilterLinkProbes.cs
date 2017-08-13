using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;

namespace PassCache.Middleware
{
    public class FilterLinkProbes
    {
        private string[] _blockedAgents;// = { "skype", "whatsapp" };
        private readonly RequestDelegate _next;

        public FilterLinkProbes(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _blockedAgents = configuration.Get<PassCacheConfiguration>().FilterAgents;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("User-Agent", out StringValues userAgents))
            {
                if (userAgents.Select(a => a.ToLowerInvariant()).Any(userAgent => _blockedAgents.Any(blockedAgent => blockedAgent.Contains(userAgent))))
                {
                    context.Response.Redirect("/", true);
                    return;
                }
            }

            await _next(context);
        }
    }
}
