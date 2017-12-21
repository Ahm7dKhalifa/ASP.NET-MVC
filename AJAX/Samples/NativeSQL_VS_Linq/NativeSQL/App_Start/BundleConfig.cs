using System.Web;
using System.Web.Optimization;

namespace NativeSQL
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Design/js/jquery").Include(
                        "~/Design/js/jquery-{version}.js"));

     
            bundles.Add(new ScriptBundle("~/ajax").Include(
                       "~/Design/js/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/Design/js/modernizr").Include(
                        "~/Design/js/modernizr-*"));
            
        }
    }
}
