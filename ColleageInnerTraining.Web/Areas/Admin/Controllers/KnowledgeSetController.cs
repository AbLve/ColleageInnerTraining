using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 知识点设置
    /// </summary>
    public class KnowledgeSetController : BaseController
    {
        private const string ex = "GZSXY.Pages.Manage.Knowledge.SetManage.";

        #region 注入服务
        private IKnowledgeDepJobAppService kdjService;
        private IKonwledgeInfoAppService kService;

        public KnowledgeSetController(IKonwledgeInfoAppService _kService, IKnowledgeDepJobAppService _kdjService,
                                      IJobPostAppService _jobService, IDepartmentInfoAppService _departmentService,
                                      IUserAccountAppService _userService, ISqlExecuter _sqlExecuter) :
                                      base(_departmentService, _jobService, _sqlExecuter, _userService)
        {
            departmentService = _departmentService;
            kdjService = _kdjService;
            kService = _kService;
        }
        #endregion

        #region 操作
        [PermissionAuthorizeAttribute(ex + "Index")]
        [Route("/KnowledgeSet/Index")]
        public ActionResult Index(int kId)
        {
            BindDSel();
            BindJsel();
            return View();
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        [Route("/KnowledgeSet/GetDataList")]
        public ActionResult GetDataList(int pIndex = 1)
        {
            ViewBag.pageName = "GetDataList";
            var input = new GetKnowledgeDepJobInput();
            var pagedata = kdjService.GetPagedKnowledgeDepJobs(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/DataList", pagedata.Items);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="kId"></param>
        /// <param name="type"></param>
        /// <param name="bId"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Set")]
        [Route("/KnowledgeSet/Set")]
        public ActionResult Set(int kId, int type, int bId)
        {
            var kDate = kService.GetKonwledgeInfoById(new EntityDto<long>() { Id = kId });//知识点数据
            if (kDate != null && type >= 0 && bId > 0)
            {
                var boundData = kdjService.GetCDJByKIdOrTypeId(Convert.ToInt32(kDate.Id), bId);//绑定的数据

                if (boundData != null)
                {
                    return Warn("已经设置到了知识点");
                }

                var create = new KnowledgeDepJobEditDto();//实例化对象Bound对象            
                create.KnoledgeId = Convert.ToInt32(kDate.Id);
                create.KnoledgeTitle = kDate.Title;
                create.type = type;

                switch (type)
                {
                    case (int)ConfigureType.Department:
                        var department = departmentService.GetDepartmentInfoById(new EntityDto<long>() { Id = bId });
                        if (department != null)
                        {
                            create.BusinessId = bId;
                            create.BusinessName = department.DisplayName;
                        }
                        break;
                    case (int)ConfigureType.Post:
                        var post = jobPostService.GetJobPostById(new EntityDto<long>() { Id = bId });
                        if (post != null)
                        {
                            create.BusinessId = (int)post.Id;
                            create.BusinessName = post.Name;
                        }
                        break;
                    default:
                        break;
                }

                kdjService.CreateKnowledgeDepJob(create);
            }
            return Success();
        }


        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Remove")]
        [Route("/KnowledgeSet/Remove")]
        public ActionResult Remove(int Id)
        {
            var boundData = kdjService.GetKnowledgeDepJobById(new EntityDto<long> { Id = Id });//绑定的数据
            if (boundData != null)
            {
                kdjService.DeleteKnowledgeDepJob(new EntityDto<long>() { Id = Convert.ToInt32(boundData.Id) });
            }
            return Success();
        }

        /// <summary>
        /// 部门下拉框
        /// </summary>
        protected void BindDSel()
        {
            var departmentList = departmentService.DepartmentList();
            var departmentItem = new List<SelectListItem>();
            if (departmentList != null && departmentList.Any())
            {
                foreach (var item in departmentList)
                {
                    departmentItem.Add(new SelectListItem { Text = item.DisplayName, Value = item.Id.ToString() });
                }
            }
            ViewBag.DepartmentData = departmentItem;
        }
        /// <summary>
        /// 获取岗位下拉数据
        /// </summary>
        protected void BindJsel()
        {
            var jobPostList = jobPostService.GetJobPostDataList();
            var jobPostItem = new List<SelectListItem>();
            if (jobPostList != null && jobPostList.Any())
            {
                foreach (var item in jobPostList)
                {
                    jobPostItem.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }
            ViewBag.JobPostData = jobPostItem;
        }
        #endregion
    }
}