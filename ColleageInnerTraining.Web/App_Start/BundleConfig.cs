using System.Web.Optimization;
using ColleageInnerTraining.Web.Bundling;

namespace ColleageInnerTraining.Web.App.Startup
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //LIBRARIES

            AddAppCssLibs(bundles);

            //bundles.Add(
                //new ScriptBundle("~/Bundles/Admin/libs/js")
                //    .Include(
                //        ScriptPaths.Json2,
                //        ScriptPaths.JQuery,
                //        ScriptPaths.JQuery_Migrate,
                //        ScriptPaths.JQuery_UI,
                //        ScriptPaths.JQuery_Validation,
                //        ScriptPaths.Bootstrap,
                //        ScriptPaths.Bootstrap_Hover_Dropdown,
                //        ScriptPaths.JQuery_Slimscroll,
                //        ScriptPaths.JQuery_BlockUi,
                //        ScriptPaths.Js_Cookie,
                //        ScriptPaths.JQuery_Uniform,
                //        ScriptPaths.JQuery_Ajax_Form,
                //        ScriptPaths.JQuery_jTable,
                //        ScriptPaths.JQuery_Color,
                //        ScriptPaths.JQuery_Jcrop,
                //        ScriptPaths.JQuery_Timeago,
                //        ScriptPaths.SignalR,
                //        ScriptPaths.LocalForage,
                //        ScriptPaths.Morris,
                //        ScriptPaths.Morris_Raphael,
                //        ScriptPaths.JQuery_Sparkline,
                //        ScriptPaths.JsTree,
                //        ScriptPaths.Bootstrap_Switch,
                //        ScriptPaths.SpinJs,
                //        ScriptPaths.SpinJs_JQuery,
                //        ScriptPaths.PushJs,
                //        ScriptPaths.SweetAlert,
                //        ScriptPaths.Toastr,
                //        ScriptPaths.MomentJs,
                //        ScriptPaths.MomentTimezoneJs,
                //        ScriptPaths.Bootstrap_DateRangePicker,
                //        ScriptPaths.Bootstrap_Select,
                //        ScriptPaths.Underscore,
                //        ScriptPaths.Abp,
                //        ScriptPaths.Abp_JQuery,
                //        ScriptPaths.Abp_Toastr,
                //        ScriptPaths.Abp_BlockUi,
                //        ScriptPaths.Abp_SpinJs,
                //        ScriptPaths.Abp_SweetAlert,
                //        ScriptPaths.Abp_Moment,
                //        ScriptPaths.Abp_jTable,
                //        ScriptPaths.MustacheJs
                //    ).ForceOrdered()
                //);
            

            //METRONIC

            AddAppMetronicCss(bundles);

            bundles.Add(
              new ScriptBundle("~/Bundles/App/metronic/js")
                  .Include(
                      "~/metronic/assets/global/scripts/app.js",
                      "~/metronic/assets/admin/layout4/scripts/layout.js",
                      "~/metronic/assets/layouts/global/scripts/quick-sidebar.js"
                  ).ForceOrdered()
              );

        }
        
        private static void AddAppCssLibs(BundleCollection bundles) { 
            //~/Bundles/vendor/bottom (Included in the bottom for fast page load)
            bundles.Add(
                new ScriptBundle("~/Bundles/vendor/js/bottom")
                    .Include(
                        "~/Scripts/json2.min.js",

                        "~/Scripts/jquery-2.1.4.min.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js",
                        "~/Scripts/jQuery-Timepicker/jquery-ui-timepicker-addon.min.js",
                        "~/Scripts/jQuery-Timepicker/i18n/jquery-ui-timepicker-zh-CN.js",

                        "~/Scripts/bootstrap.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/sweetalert/sweet-alert.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.sweet-alert.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js"
                    )
                );

            //APPLICATION RESOURCES

            //~/Bundles/css
            bundles.Add(
                new StyleBundle("~/Bundles/css")
                );

            //~/Bundles/js
            bundles.Add(
                new ScriptBundle("~/Bundles/js")
                    .Include("~/js/main.js")
                    .Include("~/js/public.js")
                );





        }
        

        private static void AddMpaCssLibs(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/App/libs/css")
                    .Include(StylePaths.FontAwesome, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.Simple_Line_Icons, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.FamFamFamFlags, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.Bootstrap, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.JQuery_Uniform, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.Morris)
                    .Include(StylePaths.JsTree, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.SweetAlert)
                    .Include(StylePaths.Toastr)
                    .Include(StylePaths.Angular_Ui_Grid, new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include(StylePaths.Bootstrap_DateRangePicker)
                    .Include(StylePaths.Bootstrap_Select)
                    .Include(StylePaths.Bootstrap_Switch)
                    .Include(StylePaths.JQuery_Jcrop)
                    .ForceOrdered()
                );
        }

        private static void AddAppMetronicCss(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/Bundles/App/metronic/css")
                    .Include("~/metronic/assets/global/css/components-md.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/global/css/plugins-md.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/admin/layout4/css/layout.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .Include("~/metronic/assets/admin/layout4/css/themes/light.css", new CssRewriteUrlWithVirtualDirectoryTransform())
                    .ForceOrdered()
                );
        }
    }
}
