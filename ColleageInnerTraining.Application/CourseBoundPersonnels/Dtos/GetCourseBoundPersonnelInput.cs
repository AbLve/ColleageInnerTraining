using Abp.Runtime.Validation;
using ColleageInnerTraining.Dto;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 课程人员绑定表查询Dto
    /// </summary>
    public class GetCourseBoundPersonnelInput : PagedAndSortedInputDto, IShouldNormalize
    {
        //DOTO:在这里增加查询参数

		/// <summary>
	    /// 模糊查询参数
		/// </summary>
		public string FilterText { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public int DepartmentId { get; set; }

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
