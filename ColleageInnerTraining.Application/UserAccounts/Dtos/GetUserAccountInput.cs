using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;
using System.Collections.Generic;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 用户账号信息查询Dto
    /// </summary>
    public class GetUserAccountInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string FilterText { get; set; }
        /// <summary>
	    /// 用户名
		/// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// 是否已绑定
        /// </summary>
        public int Isbound { get; set; }

        /// <summary>
        /// 课程Id
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// 用户Ids
        /// </summary>
        public List<int> UserIds { get; set; }

        /// <summary>
        /// 岗位Id
        /// </summary>
        public int jobId { get; set; }


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
