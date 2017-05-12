using System.Web.Optimization;

namespace ColleageInnerTraining.Web.Bundling
{
    public static class AdminBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //COMMON BUNDLES USED BOTH IN FRONTEND AND BACKEND
            //AddMpaCssLibs(bundles);
           // AddAppMetrinicCss(bundles);

            bundles.Add(
                 new StyleBundle("~/Bundles/Admin/css")
                 .Include("~/Content/bootstrap.min.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/zTreeStyle/zTreeStyle.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/uploadfy/uploadify.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/jqgrid/ui.jqgrid-bootstrap.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/jqgrid/ui.jqgrid.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/jqgrid/ui.jqgrid-bootstrap-ui.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/framework-font.css", new CssRewriteUrlWithVirtualDirectoryTransform())

                 .Include("~/css/jquery.datetimepicker.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 //.Include("~/js/bootstrap-daterangepicker-master/daterangepicker-bs3.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 //.Include("~/js/bootstrap-daterangepicker-master/font-awesome.min.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                 .Include("~/css/style.css", new CssRewriteUrlWithVirtualDirectoryTransform())

                 .IncludeDirectory("~/Areas/Admin", "*.css", true)
                    .ForceOrdered()
                );

            bundles.Add(
               new ScriptBundle("~/Bundles/Admin/libs/js")
                   .Include(
                        ScriptPaths.Json2,
                        ScriptPaths.JQuery,
                        //ScriptPaths.Bootstrap,
                        //ScriptPaths.Bootstrap_Hover_Dropdown,
                        ScriptPaths.JQuery_Migrate,
                        ScriptPaths.JQuery_Slimscroll,
                        ScriptPaths.JQuery_BlockUi,
                        ScriptPaths.Js_Cookie
                   ).ForceOrdered()
               );

            bundles.Add(
                new ScriptBundle("~/Bundles/Admin/js")
                .Include("~/js/public.js")
                .Include("~/libs/zTree/jquery.ztree.core.min.js")
                .Include("~/libs/zTree/jquery.ztree.all.js")
                .Include("~/libs/uploadfy/jquery.uploadify.min.js")
                .Include("~/libs/laytpl/laytpl.js")
                .Include("~/js/zTree/jquery.validate.min.js")

                .Include("~/js/Validate/jquery.validate.min.js")
                .Include("~/js/Validate/jquery.form.js")
                .Include("~/js/Validate/jquery.metadata.js")
                .Include("~/js/framework-ui.js")
                .Include("~/js/layer/dialog.js")

                .Include("~/js/ueditor/ueditor.config.js")
                .Include("~/js/ueditor/editor_api.js")
                .Include("~/js/ueditor/zh-cn.js")
                .Include("~/Areas/Admin/Admin.js")

                 //.Include("~/js/bootstrap-daterangepicker-master/bootstrap.min.js")
                 //.Include("~/js/bootstrap-daterangepicker-master/moment.min.js")
                 .Include("~/js/bootstrap-daterangepicker-master/daterangepicker.js")
                 .Include("~/js/datetimepicker/jquery.datetimepicker.full.min.js")
                 .ForceOrdered()
                );
        }

        //private static void AddMpaCssLibs(BundleCollection bundles)
        //{
        //    bundles.Add(
        //        new StyleBundle("~/Bundles/Mpa/libs/css")
        //            .Include(StylePaths.JQuery_UI, new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include(StylePaths.JQuery_jTable_Theme, new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include(StylePaths.FontAwesome, new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include(StylePaths.Simple_Line_Icons, new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include(StylePaths.FamFamFamFlags, new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include(StylePaths.JQuery_Uniform, new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include(StylePaths.JsTree, new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include(StylePaths.Morris)
        //            .Include(StylePaths.SweetAlert)
        //            .Include(StylePaths.Toastr)
        //            .Include(StylePaths.JQuery_Jcrop)
        //            .ForceOrdered()
        //        );
        //}

        //private static void AddAppMetrinicCss(BundleCollection bundles)
        //{
        //    bundles.Add(
        //        new StyleBundle("~/Bundles/Mpa/metronic/css")
        //            .Include("~/metronic/assets/global/css/components-md.css", new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include("~/metronic/assets/global/css/plugins-md.css", new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include("~/metronic/assets/admin/layout4/css/layout.css", new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .Include("~/metronic/assets/admin/layout4/css/themes/light.css", new CssRewriteUrlWithVirtualDirectoryTransform())
        //            .ForceOrdered()
        //        );

        //}
    }
}