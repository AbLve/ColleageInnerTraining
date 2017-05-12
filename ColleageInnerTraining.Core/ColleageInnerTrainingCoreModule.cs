using System.Reflection;
using Abp.Modules;

namespace ColleageInnerTraining
{
    public class ColleageInnerTrainingCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
