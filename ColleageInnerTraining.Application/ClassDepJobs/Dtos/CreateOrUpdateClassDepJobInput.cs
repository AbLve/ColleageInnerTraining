using ColleageInnerTraining.Application;

namespace ColleageInnerTraining.Application.Dtos
{
    /// <summary>
    /// 班级和部门或岗位新增和编辑时用Dto
    /// </summary>

    public class CreateOrUpdateClassDepJobInput
    {
        /// <summary>
        /// 班级和部门或岗位编辑Dto
        /// </summary>
        public ClassDepJobEditDto ClassDepJobEditDto { get; set; }

    }
}
