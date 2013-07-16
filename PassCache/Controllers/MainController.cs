using System;
using System.Web.Caching;
using System.Web.Mvc;

namespace PassCache.Controllers
{
    public class MainController : Controller
    {
        private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(5);

        public ActionResult Set(string id, string data)
        {
            var guid = Guid.NewGuid();
            if (id == null || data == null)
                return View("Set", null, guid);
            System.Web.HttpContext.Current.Cache.Insert(id, data, null, DateTime.UtcNow.Add(Expiration), Cache.NoSlidingExpiration);
            return View("Set", null, guid);
        }

        public ActionResult Get(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Set");
            }
            var str = (string) System.Web.HttpContext.Current.Cache.Get(id);
            System.Web.HttpContext.Current.Cache.Remove(id);
            if (string.IsNullOrEmpty(str))
            {
                return RedirectToAction("Set");
            }

            return View("Get", null, str);
        }
    }
}
