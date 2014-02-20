using System.Web.Optimization;

namespace PassCache
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/js").Include(
                    "~/Static/uheprng.js",
                    "~/Static/sjcl.js",
                    "~/Static/get.js",
                    "~/Static/set.js"));
            
            bundles.Add(new StyleBundle("~/css").Include(
                    "~/Static/bootstrap.min.css",
                    "~/Static/passcache.css"
                ));
            
            BundleTable.EnableOptimizations = true;
        }
    }
}
