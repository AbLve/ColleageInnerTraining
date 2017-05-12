namespace ColleageInnerTraining.Common
{
    public class Enums
    {

    }

    //课程类型
    public enum CourseType
    {
        [EnumDescription("系列课程")]
        SeriesCourse = 1,
        [EnumDescription("直播课程")]
        DirectSeeding = 2,
        [EnumDescription("点播课程")]
        OnDemand = 3,
        [EnumDescription("线下培训")]
        Line = 4
    }

    //内训师
    public enum InternalTrainer
    {
        [EnumDescription("李红")]
        LH = 1,
        [EnumDescription("王磊")]
        WL = 2,
        [EnumDescription("天天")]
        TT = 3,
        [EnumDescription("小马哥")]
        XMG = 4
    }
    //展示位置
    public enum Display
    {
        [EnumDescription("电脑端")]
        PC = 1,
        [EnumDescription("手机端")]
        MPhone = 2
    }

    //课程状态
    public enum CourseStatus
    {
        [EnumDescription("待审核")]
        Pending = 1,
        [EnumDescription("审核通过")]
        Audited = 2,
        [EnumDescription("审核不通过")]
        Fail = 3,
        [EnumDescription("已完成")]
        Completed = 4
    }

    //是否
    public enum Whether
    {
        [EnumDescription("是")]
        Yes = 1,
        [EnumDescription("否")]
        No = 2
    }

    //职能分类
    public enum ConfigureType
    {
        [EnumDescription("部门")]
        Department = 0,
        [EnumDescription("岗位")]
        Post = 1,
        [EnumDescription("班级")]
        Class = 2,
        [EnumDescription("个人")]
        Personal = 3
    }

    //职能分类去掉个人
    public enum ConfigureTypeC
    {
        [EnumDescription("部门")]
        Department = 0,
        [EnumDescription("岗位")]
        Post = 1,
        [EnumDescription("班级")]
        Class = 2
    }

    //班级项目类型
    public enum ClassProType
    {
        [EnumDescription("课程")]
        KC = 1,
        [EnumDescription("考试")]
        KS = 2,
        [EnumDescription("问卷")]
        WJ = 3,
        [EnumDescription("线下培训")]
        XXPX = 4
    }

    //内训师角色
     public enum TeacherRolu
    {

        [EnumDescription("外聘")]
        EngagedExternal = 1,
        [EnumDescription("内聘")]
        InternalModel = 2
    }

    //内训师状态
    public enum TeacherStatus
    {
        [EnumDescription("待审核")]
        Pending = 1,
        [EnumDescription("审核通过")]
        Audited = 2,
        [EnumDescription("审核不通过")]
        Fail = 3
    }
    
    
}
