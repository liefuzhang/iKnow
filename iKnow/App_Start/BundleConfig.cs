using System.Web;
using System.Web.Optimization;

namespace iKnow {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/site.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/chosen*"));

            bundles.Add(new ScriptBundle("~/bundles/trumbowyg").Include(
                        "~/Scripts/trumbowyg.js",
                        "~/Scripts/trumbowyg.base64.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/font-awesome.css",
                        "~/Content/chosen.css",
                        "~/Content/site.css"));
        }
    }
}
