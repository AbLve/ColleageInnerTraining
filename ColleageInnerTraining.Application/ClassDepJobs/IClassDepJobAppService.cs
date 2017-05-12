using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;

namespace ColleageInnerTraining.Application
{
    /// <summary>
    /// 班级和部门或岗位服务接口
    /// </summary>
    public interface IClassDepJobAppService : IApplicationService
    {
        #region 班级和部门或岗位管理

        /// <summary>
        /// 根据查询条件获取班级和部门或岗位分页列表
        /// </summary>
        PagedResultDto<ClassDepJobListDto> GetPagedClassDepJobs(GetClassDepJobInput input);

        /// <summary>
        /// 通过Id获取班级和部门或岗位信息进行编辑或修改 
        /// </summary>
        GetClassDepJobForEditOutput GetClassDepJobForEdit(NullableIdDto<long> input);

        /// <summary>
        /// 通过指定id获取班级和部门或岗位ListDto信息
        /// </summary>
        ClassDepJobListDto GetClassDepJobById(EntityDto<long> input);

        /// <summary>
        /// 获取全部id
        /// </summary>
        List<int> GetClassDepJobCIdsOrJids(int type);

        /// <summary>
        /// 新增或更改班级和部门或岗位
        /// </summary>
        void CreateOrUpdateClassDepJob(CreateOrUpdateClassDepJobInput input);

        /// <summary>
        /// 通过班级ID获取数据
        /// </summary>
        List<ClassDepJob> GetCDJByCIdOrTypeId(int CId, int TId);


        /// </summary>

        /// <summary>
        /// 新增班级和部门或岗位
        /// </summary>
        ClassDepJobEditDto CreateClassDepJob(ClassDepJobEditDto input);

        /// <summary>
        /// 更新班级和部门或岗位
        /// </summary>
        void UpdateClassDepJob(ClassDepJobEditDto input);

        /// <summary>
        /// 删除班级和部门或岗位
        /// </summary>
        void DeleteClassDepJob(EntityDto<long> input);
        #endregion




    }
}
