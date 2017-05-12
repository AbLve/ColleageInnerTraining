using Abp.Application.Services.Dto;
using Abp.Web.Security.AntiForgery;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using ColleageInnerTraining.Web.Controllers;
using MySql.Data.MySqlClient;
using ColleageInnerTraining.Web.Auth;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 网站管理
    /// </summary>
    public class SystemController : BaseController
    {

        private const string ex = "GZSXY.Pages.Manage.System.";
        public SystemController(IDepartmentInfoAppService service, ISqlExecuter sqlExecuter, IUserAccountAppService _userService, IJobPostAppService jobpostservice) :
                           base(service, jobpostservice, sqlExecuter, _userService)
        {

        }
        #region 部门信息部分

        /// <summary>
        /// 部门index
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "DepartMent.Index")]
        public ActionResult DepartMentIndex()
        {
            return View("DepartMent/Index");
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetdepartList()
        {
            var ss = departmentService.GetPagedDepartmentInfos(new GetDepartmentInfoInput { SkipCount = 0, MaxResultCount = 1000 }).Items;
            var relist = ss.ToList();
            var datalist = new List<TreeView>();
            relist.ForEach(t => datalist.Add(t.DDtoToTreeView()));
            return Json(new { data = datalist }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存部门
        /// </summary>
        /// <param name="strlist"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        [Route("/System/SaveOrUpdate")]
        public ActionResult SaveOrUpdate(string strlist)
        {
            
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var treelist = jss.Deserialize<List<TreeView>>(strlist);
            var dtolist = treelist.TreeViewToDDto();
            foreach (var item in dtolist)
            {
                if (item.IsNew)//新增的
                {
                    var list = departmentService.GetAllDepartmentInfos();
               
                    if (list.Count > 0)
                    {
                        var pdto = list.FirstOrDefault(t => t.DepartmentId == item.ParentId);
                        item.Path = pdto.Path + item.DepartmentId + "/";
                    }
                    else
                    {
                        item.Path = item.DepartmentId + "/";
                    }
                    item.IsNew = false;
                    departmentService.CreateDepartmentInfoAsync(item);
                }
                else//修改的
                {
                    string sqlstr = "update px_common_department set displayname=@displayname ,update_time=@update_time, updater=@updater where department_id=@department_id";
                    var i = sqlExecuter.Execute(sqlstr,
                                                    new MySqlParameter("@displayname", item.DisplayName),
                                                    new MySqlParameter("@update_time", DateTime.Now),
                                                    new MySqlParameter("@updater", 1),
                                                    new MySqlParameter("@department_id", item.DepartmentId));
                }
            }
            return Content("操作成功！");

        }

        /// <summary>
        /// 获取最后id+1作为新id
        /// </summary>
        /// <returns></returns>
        public ActionResult getLastId()
        {
            var dto = departmentService.GetAllDepartmentInfos().OrderByDescending(t => t.DepartmentId).FirstOrDefault();
            if (dto == null)
            {
                return Content("100");
            }
            return Content(dto.DepartmentId + "");
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "DepartMent.Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                var model = departmentService.GetAllDepartmentInfos().Where(t => t.DepartmentId == id).FirstOrDefault();
                if (model == null)
                {
                    return Json(new { code = 200, msg = "删除成功！" }, JsonRequestBehavior.AllowGet);
                }
                string sql = "update px_common_department set removed==true where department_id=" + id;
                var i = sqlExecuter.Execute(sql);
                return Success();
            }
            catch (Exception e)
            {
                return Fail();
            }
        }

        #endregion


        #region 岗位信息
        [PermissionAuthorizeAttribute(ex + "JobPost.Index")]

        public ActionResult JobPostIndex()
        {
            return View("JobPost/Index");
        }

        /// <summary>
        /// 岗位分布页
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        public ActionResult GetJobPostDataList(string keyword, int pIndex = 1)
        {
            ViewBag.pageName = "GetJobPostDataList";
            var input = new GetJobPostInput() { FilterText = keyword, SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = jobPostService.GetPagedJobPosts(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/JobPostDataList", pagedata.Items);
        }


        /// <summary>
        /// 岗位表单页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "JobPost.Create")]
        public async Task<ActionResult> JobPostCreate(int id = 0)
        {
            var model = new GetJobPostForEditOutput();
            if (id > 0)
            {
                model = await jobPostService.GetJobPostForEditAsync(new NullableIdDto<long> { Id = id });
            }
            return View("JobPost/Create", model.JobPost);
        }

        /// <summary>
        /// 岗位保存
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/System/Save")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(JobPostEditDto dto)
        {
            try
            {

                jobPostService.CreateOrUpdateJobPostAsync(new CreateOrUpdateJobPostInput { JobPostEditDto = dto });
                return RedirectToAction("JobPostIndex");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 岗位删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "JobPost.Delete")]
        public ActionResult JobPostDelete(int id)
        {
            try
            {
                jobPostService.DeleteJobPostAsync(new EntityDto<long> { Id = id });
                return Success();
            }
            catch (Exception e)
            {
                return Fail();
            }
        }

        #endregion
    }
}
