using System;
using System.Web.Caching;
using System.Web.Mvc;
using PassCache.Models;

namespace PassCache.Controllers
{
    public class MainController : Controller
    {
        private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(5);

        public ActionResult Set(string id, string data)
        {
            if (id != null && data != null)
            {
                System.Web.HttpContext.Current.Cache.Insert(id, data, null, DateTime.UtcNow.Add(Expiration),
                                                            Cache.NoSlidingExpiration);
            }

            return View("Set", null, new SetModel());
        }

        public ActionResult Get(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Set");
            }

            string str;
            lock (Lock)
            {
                str = (string) System.Web.HttpContext.Current.Cache.Get(id);
                System.Web.HttpContext.Current.Cache.Remove(id);
            }
            
            if (string.IsNullOrEmpty(str))
            {
                return RedirectToAction("Set");
            }

            return View("Get", null, str);
        }

        private static readonly object Lock = new object();
    }
}
