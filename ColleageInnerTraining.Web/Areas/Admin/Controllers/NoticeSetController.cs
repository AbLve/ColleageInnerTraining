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
    /// 公告设置
    /// </summary>
    public class NoticeSetController : BaseController
    {
        private const string ex = "GZSXY.Pages.Manage.System.SetManage.";

        #region 注入服务
        private INoticeDepJobAppService ndjService;
        private INoticeAppService nService;

        public NoticeSetController(INoticeAppService _nService, INoticeDepJobAppService _ndjService,
                                   IJobPostAppService _jobService, IDepartmentInfoAppService _departmentService,
                                   IUserAccountAppService _userService, ISqlExecuter _sqlExecuter) :
                                   base(_departmentService, _jobService, _sqlExecuter, _userService)
        {
            ndjService = _ndjService;
            nService = _nService;
        }
        #endregion

        #region 操作
        [PermissionAuthorizeAttribute(ex + "Index")]
        [Route("/NoticeSet/Index")]
        public ActionResult Index()
        {
            BindDSel();
            BindJsel();
            return View();
        }

        [Route("/NoticeSet/GetDataList")]
        public ActionResult GetDataList(int pIndex = 1)
        {
            ViewBag.pageName = "GetDataList";
            var input = new GetNoticeDepJobInput();
            var pagedata = ndjService.GetPagedNoticeDepJobs(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/DataList", pagedata.Items);
        }

        //设置
        [HttpGet]
        [PermissionAuthorizeAttribute(ex + "Set")]
        [Route("/NoticeSet/Set")]
        public ActionResult Set(int nId, int type, int bId)
        {
            var nDate = nService.GetNoticeById(new EntityDto<long>() { Id = nId });//公告数据
            if (nDate != null && type >= 0 && bId > 0)
            {
                var boundData = ndjService.GetCDJByNIdOrTypeId(Convert.ToInt32(nDate.Id), bId);//绑定的数据

                if (boundData != null)
                {
                    return Warn("已经设置到了公告");
                }

                var create = new NoticeDepJobEditDto();//实例化对象Bound对象            
                create.NoticeId = Convert.ToInt32(nDate.Id);
                create.NoticetTitle = nDate.Title;
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

                ndjService.CreateNoticeDepJob(create);
            }
            return Success();
        }

        //移除
        [HttpGet]
        [PermissionAuthorizeAttribute(ex + "Remove")]
        [Route("/NoticeSet/Remove")]
        public ActionResult Remove(int Id)
        {
            var boundData = ndjService.GetNoticeDepJobById(new EntityDto<long> { Id = Id });//绑定的数据
            if (boundData != null)
            {
                ndjService.DeleteNoticeDepJob(new EntityDto<long>() { Id = Convert.ToInt32(boundData.Id) });
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