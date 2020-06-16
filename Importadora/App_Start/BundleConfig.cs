using System.Web;
using System.Web.Optimization;

namespace Importadora
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/globalizacion").Include(
                        "~/Scripts/globalize.js",
                        "~/Scripts/cultures/globalize.culture.es-UY.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryuijs").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-superhero.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryuicss").Include(
                      "~/Content/jquery-ui.*"));
        }
    }
}
