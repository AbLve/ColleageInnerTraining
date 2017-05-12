using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.AutoMapper;
using ColleageInnerTraining.Web.Bundling;

namespace ColleageInnerTraining.Web
{
    [DependsOn(
        typeof(AbpWebMvcModule),
        typeof(ColleageInnerTrainingDataModule), 
        typeof(ColleageInnerTrainingApplicationModule), 
        typeof(ColleageInnerTrainingWebApiModule),
        typeof(AbpAutoMapperModule)
        )]
    public class ColleageInnerTrainingWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabled = true;
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
           
           
            //配置Auditing info 存储
            //Configuration.ReplaceService(typeof(IAuditingStore),()=> { IocManager.Register<IAuditingStore, CustomeAuditingStore>(Abp.Dependency.DependencyLifeStyle.Singleton); });
        }


        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            CommonBundleConfig.RegisterBundles(BundleTable.Bundles);
            AdminBundleConfig.RegisterBundles(BundleTable.Bundles);
            WapBundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); 
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }
    }
}
