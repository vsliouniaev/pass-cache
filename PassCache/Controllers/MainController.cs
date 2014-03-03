// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainController.cs" company="N/A">
//   Public domain
// </copyright>
// <summary>
//   The single controller for this project
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PassCache.Controllers
{
    using System;
    using System.Web.Caching;
    using System.Web.Mvc;

    /// <summary>
    /// The single controller for this project
    /// </summary>
    public class MainController : Controller
    {
        /// <summary>
        /// Amount of time the messages are kept before data is discarded.
        /// </summary>
        private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Displays the add message page or adds a message to the cache.
        /// </summary>
        /// <param name="id">
        /// The message id.
        /// </param>
        /// <param name="data">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/> message adding view.
        /// </returns>
        public ActionResult Set(string id, string data)
        {
            if (id != null && data != null)
            {
                var obj = new SingleAccessObject<string>(data);
                System.Web.HttpContext.Current.Cache.Insert(id, obj, null, DateTime.UtcNow.Add(Expiration), Cache.NoSlidingExpiration);
            }

            return this.View("Set", null, new[] { GetRandomString(), GetRandomString() });
        }

        /// <summary>
        /// Retrieves a message from the cache and sends it to the client,
        /// wiping the message data
        /// </summary>
        /// <param name="id">
        /// The message id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/> view and message.
        /// </returns>
        public ActionResult Get(string id)
        {
            if (id == null)
            {
                return this.View("Gone");
            }

            var obj = (SingleAccessObject<string>)System.Web.HttpContext.Current.Cache.Get(id);

            if (obj == null)
            {
                return this.View("Gone");
            }

            string str;
            if (obj.TryGet(out str))
            {
                System.Web.HttpContext.Current.Cache.Remove(id);
                return this.View("Get", null, str);
            }
            else
            {
                return this.View("Gone");
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
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
