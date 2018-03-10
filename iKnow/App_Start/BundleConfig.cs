using System.Web;
using System.Web.Optimization;

namespace iKnow {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app/services/questionService.js",
                        "~/Scripts/app/services/searchService.js",
                        "~/Scripts/app/services/loadMoreService.js",
                        "~/Scripts/app/controllers/topicController.js",
                        "~/Scripts/app/controllers/questionController.js",
                        "~/Scripts/app/controllers/loadMoreController.js",
                        "~/Scripts/app/controllers/answerController.js",
                        "~/Scripts/app/controllers/photoUploadController.js",
                        "~/Scripts/app/controllers/registerController.js",
                        "~/Scripts/app/controllers/modalController.js",
                        "~/Scripts/app/controllers/headerController.js",
                        "~/Scripts/app/controllers/warningErrorController.js",
                        "~/Scripts/app/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/chosen*"));

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
