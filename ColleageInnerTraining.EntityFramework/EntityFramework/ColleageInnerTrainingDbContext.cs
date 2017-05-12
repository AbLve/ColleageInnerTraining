using Abp.EntityFramework;
using ColleageInnerTraining;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Core;
using System.Data.Entity;

namespace ColleageInnerTraining.EntityFramework
{
    public class ColleageInnerTrainingDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        public IDbSet<Student> Students { get; set; }        

        /// <summary>
        /// 课程分类
        /// </summary>
        public IDbSet<CourseCategory> CourseCategorys { get; set; }
        /// <summary>
        /// 课程基本信息
        /// </summary>
        public IDbSet<CourseInfo> CourseInfos { get; set; }
        
        //菜单:
        public virtual IDbSet<Menu> Menus { get; set; }
        //内训师:
        public virtual IDbSet<Teachers> Teachers { get; set; }

        /// <summary>
        /// 班级信息
        /// </summary>
        public IDbSet<ClassesInfo> ClassesInfos { get; set; }
        /// <summary>
        /// 班级成员信息
        /// </summary>
        public IDbSet<ClassUser> ClassUsers { get; set; }
        /// <summary>
        /// 线下培训信息
        /// </summary>
        public IDbSet<ClassTrainingInfo> ClassTrainingInfos { get; set; }
        /// <summary>
        /// 线下培训记录
        /// </summary>
        public IDbSet<ClassTrainingRecord> ClassTrainingRecords { get; set; }
        /// <summary>
        /// 班级和部门或岗位关联表
        /// </summary>
        public IDbSet<ClassDepJob> ClassDepJobs { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public IDbSet<DepartmentInfo> DepartmentInfos { get; set; }

        /// <summary>
        /// 用户账号信息
        /// </summary>
        public IDbSet<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// 岗位信息
        /// </summary>
        public IDbSet<JobPost> JobPosts { get; set; }

        /// <summary>
        /// 轮播图
        /// </summary>
        public IDbSet<Banner> Bannera { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public IDbSet<Taget> Taget { get; set; }
        /// <summary>
        /// 公告
        /// </summary>
        public IDbSet<Notice> Notice { get; set; }

        /// <summary>
        /// 公告部门岗位关联
        /// </summary>
        public IDbSet<NoticeDepJob> NoticeDepJob { get; set; }

        /// <summary>
        /// 知识库
        /// </summary>
        public IDbSet<KonwledgeInfo> KonwledgeInfo { get; set; }
        /// <summary>
        /// 知识库标签关联
        /// </summary>
        public IDbSet<KonwledgeTag> KonwledgeTag { get; set; }
        /// <summary>
        /// 知识点部门岗位关联
        /// </summary>
        public IDbSet<KnowledgeDepJob> KonwledgeDepJob { get; set; }

        /// <summary>
        /// 课程人员关联表
        /// </summary>
        public IDbSet<CourseBoundPersonnel> CBoundPersonnel { get; set; }

        /// <summary>
        /// 课程类型关联表
        /// </summary>
        public IDbSet<CourseBoundConfigureType> CBoundConfigure { get; set; }

        /// <summary>
        /// 互动社区表
        /// </summary>
        public IDbSet<CommunityInteraction> CInterration { get; set; }
        /// <summary>
        /// 收藏
        /// </summary>
        public IDbSet<Collection> Collection { get; set; }
        /// <summary>
        /// 阅读人数
        /// </summary>
        public IDbSet<ReadTimes> ReadTime { get; set; }
        /// <summary>
        /// 考试记录
        /// </summary>
        public IDbSet<ExamRecord> ExamRecord { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public ColleageInnerTrainingDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in ColleageInnerTrainingDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ColleageInnerTrainingDbContext since ABP automatically handles it.
         */
        public ColleageInnerTrainingDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}