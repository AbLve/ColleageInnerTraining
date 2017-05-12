using System.Web.Optimization;

namespace ColleageInnerTraining.Web.Bundling
{
    public static class WapBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //COMMON BUNDLES USED BOTH IN FRONTEND AND BACKEND
            
            bundles.Add(
                 new StyleBundle("~/Bundles/Wap/css")
                 .Include("~/Content/bootstrap.min.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/wapcss/style.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/dropload.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .IncludeDirectory("~/Areas/Wap", "*.css", true)
                 .ForceOrdered()
                );

            bundles.Add(
               new ScriptBundle("~/Bundles/Wap/libs/js")
                   .Include(
                        ScriptPaths.Json2,
                        ScriptPaths.JQuery,
                        ScriptPaths.Bootstrap,
                        ScriptPaths.Bootstrap_Hover_Dropdown,
                        ScriptPaths.JQuery_Migrate,
                        ScriptPaths.JQuery_Slimscroll,
                        ScriptPaths.JQuery_BlockUi,
                        ScriptPaths.Js_Cookie
                   ).ForceOrdered()
               );

            bundles.Add(
                new ScriptBundle("~/Bundles/Wap/js")
                .Include("~/libs/laytpl/laytpl.js")
                .Include("~/js/wapjs/jquery.flexslider-min.js")
                .Include("~/js/wapjs/public.js")
                .Include("~/js/dropload-gh-pages/dropload.min.js")
                .Include("~/Areas/Wap/wap.js")
                .ForceOrdered()
                );
        }
    }
}