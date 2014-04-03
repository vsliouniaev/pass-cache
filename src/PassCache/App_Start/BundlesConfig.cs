// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundlesConfig.cs" company="N/A">
//   Public domain
// </copyright>
// <summary>
//   The bundle config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PassCache
{
    using System.Web.Optimization;

    /// <summary>
    /// The bundle config.
    /// </summary>
    public class BundlesConfig
    {
        /// <summary>
        /// The register bundles.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/js").Include(
                    "~/Static/uheprng.js",
                    "~/Static/sjcl.js",
                    "~/Static/zeroclipboard.js",
                    "~/Static/get.js",
                    "~/Static/set.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/Static/bootstrap.min.css",
                "~/Static/passcache.css"));
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
