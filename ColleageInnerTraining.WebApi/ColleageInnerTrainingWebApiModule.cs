using System.Reflection;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace ColleageInnerTraining
{
    [DependsOn(typeof(AbpWebApiModule), typeof(ColleageInnerTrainingApplicationModule))]
    public class ColleageInnerTrainingWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(ColleageInnerTrainingApplicationModule).Assembly, "app")
                .Build();
        }
    }
}
