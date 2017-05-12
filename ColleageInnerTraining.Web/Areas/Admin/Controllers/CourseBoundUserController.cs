using Abp.Application.Services.Dto;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Web.Controllers;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class CourseBoundUserController : Controller
    {
        #region 注入服务
        private ICourseBoundPersonnelAppService courseBoundService;
        private IUserAccountAppService userAccountService;
        private ICourseInfoAppService courseInfoService;
        private IDepartmentInfoAppService departmentService;
        private ICourseBoundConfigureTypeAppService configureTypeService;
        private static int CourseID = 0;

        public CourseBoundUserController(ICourseBoundPersonnelAppService _service, 
            IUserAccountAppService _userAccountService, ICourseInfoAppService _courseInfoService,
            IDepartmentInfoAppService _departmentService,
            ICourseBoundConfigureTypeAppService _configureTypeService)
        {
            courseBoundService = _service;
            userAccountService = _userAccountService;
            courseInfoService = _courseInfoService;
            departmentService = _departmentService;
            configureTypeService = _configureTypeService;
        }
        #endregion

        #region 共用部分
        /// <summary>
        /// 每页行数
        /// </summary>
        protected int PageSize => 7;

        /// <summary>
        /// 页码最多显示范围
        /// </summary>
        protected int PageRange => 10;
        /// <summary>
        /// 获取分页对象，并保存
        /// </summary>
        /// <param name="datacount"></param>
        /// <returns></returns>
        protected PageQuery GetPageData(int datacount)
        {
            var page = new PageQuery();
            page.pageSize = string.IsNullOrEmpty(Request.QueryString["pSize"]) ? PageSize : int.Parse(Request.QueryString["pSize"]);
            page.pageRange = PageRange;
            page.pageCurentIndex = string.IsNullOrEmpty(Request.QueryString["pIndex"]) ? 1 : int.Parse(Request.QueryString["pIndex"]);
            page.records = datacount;
            page.keyword = Getkeyword();
            ViewBag.PageData = page;
            return page;
        }
        protected string Getkeyword()
        {
            return Request.QueryString["keyword"];
        }
        #endregion

        // GET: Admin/CourseBoundUser
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.CourseBoundUser.Index")]
        public ActionResult Index(int id)
        {
            CourseID = id;
            ViewBag.CourseId = id;
            //部门下拉框
            var departmentList = departmentService.DepartmentList();
            var departmentItem = new List<SelectListItem>();
            if (departmentList != null && departmentList.Any())
            {
                foreach (var item in departmentList)
                {
                    departmentItem.Add(new SelectListItem { Text = item.DisplayName, Value = item.DepartmentId.ToString() });
                }
            }
            ViewBag.DepartmentData = departmentItem;
            return View();
        }

        public ActionResult GetTreeDepartSelectJson()
        {
            var data = departmentService.DepartmentList();
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.DepartmentId + "";
                treeModel.text = item.DisplayName;
                treeModel.parentId = item.ParentId + "";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        public ActionResult GetBoundUserDataList(string userName = "",int demartment = 0,int ibound = 1, int courseID = 0, int pIndex = 1)
        {
            var a = CourseID;
            var userIds = new List<int>();
            int courseIDC = CourseID == 0  ? courseID : CourseID;
            if (courseIDC > 0)
            {
                userIds = courseBoundService.GetCourseBoundUserId(courseIDC);
            }
            
            ViewBag.pageName = "GetBoundUserDataList";
            var input = new GetUserAccountInput()
            {
                Username = userName,
                DepartmentId = demartment,
                Isbound = ibound,
                UserIds = userIds,
                SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize
            };
            var pagedata = userAccountService.GetPagedUserAccountsUD(input);
            GetPageData(pagedata.Count());
            return PartialView("Shared/BoundUserDataList", pagedata.Skip((pIndex - 1) * PageSize).Take(PageSize));
        }

        //设置绑定
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.CourseBoundUser.SetBoundCourseByUser")]
        public ActionResult SetBoundCourseByUser(int courseId, string userIds)
        {
            var result = new resultData();
            result.status = "-1";
            var userListId = userIds.Split(',').ToList();//用户的Id
            var list = new List<string>();
            var listLraen = new List<string>();
            var courseDate = courseInfoService.GetCourseInfoById(new EntityDto<long>() { Id = courseId });//课程数据
            if (courseDate != null && userListId != null && userListId.Any())
            {
                if (courseDate.Status != (int)CourseStatus.Pending)
                {
                    return Content(Serialize(new resultData() { status = "-1", msg = "当前课程状态为：待审核，才能添加人员" }));
                }
                foreach (var id in userListId)
                {                   
                    var userData = userAccountService.GetUserAccountById(new EntityDto<long>() { Id = Convert.ToInt32(id) });//用户数据
                    var boundData = courseBoundService.GetCourseBoundByUserIdOrCourseId(userData.SysNO, Convert.ToInt32(courseDate.Id));//绑定数据
                    if (boundData == null)//如果没就增加
                    {
                        //增加人员
                        var create = new CourseBoundPersonnelEditDto();
                        create.AccountSysNo = userData.SysNO;
                        create.AccountUserName = userData.DisplayName;
                        create.CourseId = Convert.ToInt32(courseDate.Id);
                        create.CourseName = courseDate.CourseName;
                        courseBoundService.CreateCourseBoundPersonnel(create);                        
                    }
                    else
                    {
                        list.Add(userData.DisplayName);
                    }

                    var configu = configureTypeService.GetCTypeByCIdOrType((int)courseDate.Id, (int)ConfigureType.Personal, userData.SysNO);
                    //增加关系
                    if (configu == null)
                    {
                        var createConfigure = new CourseBoundConfigureTypeEditDto();
                        createConfigure.CourseId = (int)courseDate.Id;
                        createConfigure.CourseName = courseDate.CourseName;
                        createConfigure.type = (int)ConfigureType.Personal;
                        createConfigure.BusinessId = userData.SysNO;
                        createConfigure.BusinessName = userData.DisplayName;
                        configureTypeService.CreateCourseBoundConfigureType(createConfigure);
                    }                                      
                }
                result.status = "0";
                result.msg = string.Join(",", list);
            }
            return Content(Serialize(result));
        }

        //取消绑定
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.CourseBoundUser.CancellBount")]
        public ActionResult CancellBount(int courseId, int userIds)
        {
            var result = new resultData();
            var courseDate = courseInfoService.GetCourseInfoById(new EntityDto<long>() { Id = courseId });//课程数据
            var userData = userAccountService.GetUserAccountById(new EntityDto<long>() { Id = userIds });//用户数据
            if (courseDate != null && userData != null)
            {
                var boundData = courseBoundService.GetCourseBoundByUserIdOrCourseId(userData.SysNO, Convert.ToInt32(courseDate.Id));//绑定数据
                if (boundData != null)
                {
                    courseBoundService.DeleteCourseBoundPersonnel(new EntityDto<long>(){ Id = Convert.ToInt32(boundData.Id) });
                    result.msg = "";
                }
                else
                {
                    result.msg = userData.DisplayName;
                }
                result.status = "0";
            }            
            return Content(Serialize(result));
        }

        public class resultData {
            public string status { get; set; }

            public string msg { get; set; }
        }
        public static string Serialize(object o)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            json.Serialize(o, sb);
            return sb.ToString();
        }
    }
}