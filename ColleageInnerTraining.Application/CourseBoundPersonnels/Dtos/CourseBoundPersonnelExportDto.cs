using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程人员绑定表列表Dto
    /// </summary>
    [AutoMapFrom(typeof(CourseBoundPersonnel))]
    public class CourseBoundPersonnelExportDto : EntityDto<long>
    {
        /// <summary>
        /// 培训档案号
        /// </summary>
        [DisplayName("培训档案号")]
        public string TrainDocNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 一级部门
        /// </summary>
        [DisplayName("一级部门")]
        public string DepartMentName1 { get; set; }
        /// <summary>
        /// 二级部门
        /// </summary>
        [DisplayName("二级部门")]
        public string DepartMentName2 { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        [DisplayName("职务")]
        public string PostName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Gender { get; set; }
        /// <summary>
        /// 培训课程
        /// </summary>
        [DisplayName("培训课程")]
        public string CourseName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 培训费用
        /// </summary>
        [DisplayName("培训费用")]
        public float CourseAmout { get; set; }
        ///<summary>
        /// 培训方式
        /// </summary>
        [DisplayName("培训方式")]
        public string TypeName { get; set; }
        ///<summary>
        /// 参训时长
        /// </summary>
        [DisplayName("参训时长")]
        public int TimeLength { get; set; }

        ///<summary>
        /// 考核方式
        /// </summary>
        [DisplayName("考核方式")]
        public string ExamMethod { get; set; }

        ///<summary>
        /// 考核结果
        /// </summary>
        [DisplayName("考核结果")]
        public string ExamResult { get; set; }

        ///<summary>
        /// 总时长
        /// </summary>
        [DisplayName("总时长")]
        public int TotalTime { get; set; }

        ///<summary>
        /// 总费用
        /// </summary>
        [DisplayName("总费用")]
        public float TotalAmount { get; set; }

        ///<summary>
        /// 培训积分
        /// </summary>
        [DisplayName("培训积分")]
        public int TrainSore { get; set; }

        ///<summary>
        /// 总积分
        /// </summary>
        [DisplayName("总积分")]
        public int TotalTrainSore { get; set; }

        ///<summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string memo { get; set; }


    }
}
