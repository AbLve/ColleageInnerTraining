using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程基本信息查询Dto
    /// </summary>
    public class GetCourseInfoInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string FilterText { get; set; }

        /// <summary>
        /// 内训师
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 内训师Id
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 课程类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 课程分类
        /// </summary>
        public int categoryType { get; set; }

        /// <summary>
        /// 课程审核状态
        /// </summary>
        public int CheckStatus { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 岗位Id
        /// </summary>
        public int JobPostId { get; set; }

        /// <summary>
        /// 分类路经
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 展示端
        /// </summary>
        public string DisplayPosition { get; set; }


        /// <summary>
        /// 用于排序的默认值
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
			
		
                Sorting = "Id";
            }
        }
    }
}
