using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PassCache.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using PassCache;

namespace PassCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        private static readonly MemoryCacheEntryOptions MemoryCacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Set(string id, string data)
        {
            if (id != null && data != null)
            {
                var obj = new SingleAccessObject<string>(data);
                _memoryCache.Set(id, obj, MemoryCacheOptions);
            }

            return View("Set", new[] { GetRandomString(), GetRandomString() });
        }

        public IActionResult Get(string id)
        {
            if (id == null)
            {
                return View("Gone");
            }

            SingleAccessObject<string> obj;
            _memoryCache.TryGetValue(id, out obj);

            if (obj == null)
            {
                return View("Gone");
            }

            string str;
            if (obj.TryGet(out str))
            {
                _memoryCache.Remove(id);
                return View("Get", str);
            }
            else
            {
                return View("Gone");
            }
        }

        /// <summary>
        /// Gets some entropy using the built-in cryptography provider.
        /// This is sent to the client as "good" entropy, although it cannot be trusted.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/> entropy string.
        /// </returns>
        private static string GetRandomString()
        {
            var bytes = new byte[256];
            Rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
