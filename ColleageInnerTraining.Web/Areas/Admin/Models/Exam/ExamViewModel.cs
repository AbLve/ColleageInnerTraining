using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColleageInnerTraining.Web.Areas.Admin.Models
{
    public class ExamViewModel
    {
        /// <summary>
        /// 部门
        /// </summary>
        public List<DepartmentInfoListDto> departs { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public List<JobPostListDto> posts { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public List<ClassesInfoListDto> classes { get; set; }
        
        public int PaperId { get; set; }

        public int ExamId { get; set; }
        public List<ComboboxItemDto> departLists { get; set; }

    }
}