using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Controllers;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.AuthServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Controllers;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class AccountUserController : BaseController
    {
        private const string ex = "";
        private IUserAccountAppService _userAccountService;
        private IDepartmentInfoAppService _departmentService;
        private IJobPostAppService _jobPostService;
        public AccountUserController(IUserAccountAppService userAccountService, IDepartmentInfoAppService departmentService,
            IJobPostAppService jobPostService)
        {
            _userAccountService = userAccountService;
            _departmentService = departmentService;
            _jobPostService = jobPostService;
        }        

        // GET: Admin/AccountUser
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.AccountUser.Index")]
        public ActionResult Index()
        {
            #region 下拉框数据
            //岗位下拉框
            var jobPostList = _jobPostService.GetJobPostDataList();
            var jobPostItem = new List<SelectListItem>();
            if (jobPostList != null && jobPostList.Any())
            {
                foreach (var item1 in jobPostList)
                {
                    jobPostItem.Add(new SelectListItem { Text = item1.Name, Value = item1.Id.ToString() });
                }
            }
            ViewBag.JobPostData = jobPostItem;
            #endregion

            return View();
        }

        public ActionResult GetTreeDepartSelectJson()
        {
            var data = _departmentService.DepartmentList();
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

        // 课程管理分页信息
        public ActionResult GetAccountUserDataList(string name="",int departmentId=0, int jobPostId=0, int pIndex = 1)
        {
            ViewBag.pageName = "GetAccountUserDataList";
            var input = new GetUserAccountInput()
            {
                Username = name,
                DepartmentId = departmentId,
                jobId = jobPostId,
                SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize
            };
            var pagedata = _userAccountService.GetPagedUserAccountsUD(input);
            GetPageData(pagedata.Count());
            return PartialView("Shared/AccountUserDataList", pagedata.Skip((pIndex - 1) * PageSize).Take(PageSize));
        }

        //用户信息同步操作
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.AccountUser.UserYynchronization")]
        public ActionResult UserYynchronization()
        {
            var resultstr = "-1";
            try
            {
                //获取服务端所有用户
                AuthServiceSoapClient authServiceSoapClient = new AuthServiceSoapClient();
                ColleageInnerTraining.Web.AuthServiceProxy.SystemUser[] AuthSystemUserList = authServiceSoapClient.GetSystemUserByApplicationID(SystemDataConst.ApplicationID);
                //获取用户端所有用户
                var systemUserList = _userAccountService.GetAccountData();
                if (AuthSystemUserList != null && AuthSystemUserList.Any())
                {
                    foreach (var item in AuthSystemUserList)
                    {
                        UserAccountEditDto userAccount = new UserAccountEditDto();
                        var currentUser = systemUserList.FirstOrDefault(c => c.SysNO == item.SysNO);
                        if (currentUser != null)
                        {   //修改
                            userAccount.LoginName = item.LoginName;
                            userAccount.DisplayName = item.DisplayName;
                            userAccount.PhoneNumber = item.PhoneNumber;
                            userAccount.Email = item.Email;
                            userAccount.SysNO = currentUser.SysNO;
                            userAccount.DepartmentID = currentUser.DepartmentID;
                            userAccount.DepartmentName = currentUser.DepartmentName;
                            userAccount.Password = currentUser.Password;
                            userAccount.Status = Convert.ToBoolean(item.Status);
                            userAccount.province = currentUser.province;
                            userAccount.ProvinceID = currentUser.ProvinceID;
                            userAccount.City = currentUser.City;
                            userAccount.CityID = currentUser.CityID;
                            userAccount.Area = currentUser.Area;
                            userAccount.AreaID = currentUser.AreaID;
                            userAccount.DetailedAddress = currentUser.DetailedAddress;
                            userAccount.PostID = currentUser.PostID;
                            userAccount.PostName = currentUser.PostName;
                            _userAccountService.UpdateUserAccountByNo(userAccount);
                        }
                        else
                        {   //增加
                            userAccount.SysNO = item.SysNO;
                            userAccount.LoginName = item.LoginName;
                            userAccount.DisplayName = item.DisplayName;
                            userAccount.DepartmentName = item.DepartmentName;
                            userAccount.PhoneNumber = item.PhoneNumber;
                            userAccount.Email = item.Email;
                            userAccount.Password = item.Password;
                            userAccount.Status = Convert.ToBoolean(item.Status);
                            _userAccountService.CreateUserAccount(userAccount);
                        }
                    }
                    resultstr = "0";
                }
            }
            catch (Exception ex)
            {
                resultstr = "-1";
            }
            return Json(resultstr, JsonRequestBehavior.AllowGet);
        }

        //修改会员管理
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.AccountUser.UpdateAccountUser")]
        public ActionResult UpdateAccountUser(int? Id)
        {
            #region 下拉框
            //部门下拉框
            var departmentList = _departmentService.DepartmentList();
            var departmentItem = new List<SelectListItem>();
            if (departmentList != null && departmentList.Any())
            {
                foreach (var item in departmentList)
                {
                    departmentItem.Add(new SelectListItem { Text = item.DisplayName, Value = item.DepartmentId.ToString() });
                }
            }
            ViewBag.DepartmentData = departmentItem;

            //岗位下拉框
            var jobPostList = _jobPostService.GetJobPostDataList();
            var jobPostItem = new List<SelectListItem>();
            if (jobPostList != null && jobPostList.Any())
            {
                foreach (var item1 in jobPostList)
                {
                    jobPostItem.Add(new SelectListItem { Text = item1.Name, Value = item1.Id.ToString() });
                }
            }
            ViewBag.JobPostData = jobPostItem;

            #endregion
            var userAccount = _userAccountService.GetUserAccountForEdit(new NullableIdDto<long> { Id = Id });
            return View(userAccount.UserAccount);
        }

        //修改数据
        [HttpPost]
        [Route("/AccountUser/UpdataUser")]
        [ValidateAntiForgeryToken]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.AccountUser.UpdataUser")]
        public ActionResult UpdataUser(UserAccountEditDto input)
        {
            try
            {
                _userAccountService.UpdateUserAccount(input);
                return RedirectToAction("/Index");
            }
            catch (Exception){ }
            return RedirectToAction("/UpdateAccountUser", input);
        }

        [HttpGet]
        [Route("Admin/AccountUser/GetPagedUserAccounts")]
        public JsonResult GetPagedUserAccounts(GetUserAccountInput input)
        {
            try
            {
              var list =  _userAccountService.GetPagedUserAccounts(input);
             
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception) {
                return new JsonResult();
            }

        }

    }
}