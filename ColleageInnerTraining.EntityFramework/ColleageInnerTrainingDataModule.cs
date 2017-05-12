using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using ColleageInnerTraining.EntityFramework;

namespace ColleageInnerTraining
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(ColleageInnerTrainingCoreModule))]
    public class ColleageInnerTrainingDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<ColleageInnerTrainingDbContext>(null);
        }
    }
}
