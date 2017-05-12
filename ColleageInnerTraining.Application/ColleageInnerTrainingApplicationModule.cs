using System.Reflection;
using Abp.Modules;

namespace ColleageInnerTraining
{
    [DependsOn(typeof(ColleageInnerTrainingCoreModule))]
    public class ColleageInnerTrainingApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
