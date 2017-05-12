using Abp.UI;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Controllers
{
    /// <summary>
    /// 自定义基类控制器
    /// </summary>
    public class BaseController : Controller
    {
        #region 公用服务注入
        protected ISqlExecuter sqlExecuter;
        protected IDepartmentInfoAppService departmentService;
        protected IJobPostAppService jobPostService;
        protected IUserAccountAppService userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_departmentService"></param>
        /// <param name="_jobPostService"></param>
        /// <param name="_sqlExecuter"></param>
        /// <param name="_userService"></param>
        public BaseController(IDepartmentInfoAppService _departmentService, IJobPostAppService _jobPostService, ISqlExecuter _sqlExecuter, IUserAccountAppService _userService)
        {
            departmentService = _departmentService;
            jobPostService = _jobPostService;
            sqlExecuter = _sqlExecuter;
            userService = _userService;
        }

        public BaseController() { }
        #endregion

        #region 共用部分
        /// <summary>
        /// 每页行数
        /// </summary>
        protected int PageSize => Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"].ToString());

        /// <summary>
        /// 页码最多显示范围
        /// </summary>
        protected int PageRange => Convert.ToInt16(ConfigurationManager.AppSettings["PageRange"].ToString());
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

        /// <summary>
        /// 获取查询输入框条件
        /// </summary>
        /// <returns></returns>
        protected string Getkeyword()
        {
            return Request.QueryString["keyword"];
        }

        /// <summary>
        /// 服务端验证
        /// </summary>
        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("表单数据有误！");
            }
        }
        protected void BindDSel()
        {

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
            ViewBag.DepartmentSel = departmentItem;
        }

        /// <summary>
        /// 获取岗位下拉数据
        /// </summary>
        protected void BindJSel()
        {

            //岗位下拉框
            var jobPostList = jobPostService.GetJobPostDataList();
            var jobPostItem = new List<SelectListItem>();
            if (jobPostList != null && jobPostList.Any())
            {
                foreach (var item in jobPostList)
                {
                    jobPostItem.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }
            ViewBag.JobPostSel = jobPostItem;
        }

        /// <summary>
        /// 成功提示
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult Success(string Msg = "操作成功", object data = null)
        {
            return Json(new { code = 200, msg = Msg, type = "success", data = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 失败提示
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        protected JsonResult Fail(string Msg = "操作失败")
        {
            return Json(new { code = 500, msg = Msg, type = "error" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 警告提示
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult Warn(string Msg = "系统提示", object data = null)
        {
            return Json(new { code = 500, msg = Msg, type = "warning", data = data }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 共用上传文件
        /// </summary>
        /// <param name="file">文件控件</param>
        /// <param name="path">上传位置</param>
        /// <returns></returns>
        protected JsonResult Upload(HttpPostedFileBase file, string path)
        {
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName); //取到文件的名称
                if (System.IO.Directory.Exists(Server.MapPath(path)) == false)//如果不存在就创建file文件夹
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(path));
                }
                string filePath = Path.Combine(HttpContext.Server.MapPath(path), Path.GetFileName(fileName));//合并得到完整的文件路径
                file.SaveAs(filePath); //保存上传文件到服务器

                return Success("上传成功", path + fileName);

            }
            return Fail("上传失败");
        }
        /// <summary>
        /// 表单提交后提示及跳转
        /// </summary>
        /// <param name="url"></param>
        /// <param name="msg"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        protected ActionResult Alert(string url, string msg, ReturnType type)
        {
            url = string.Format("<script>$.modalMsg('{0}','{1}');location.href='{2}';</script>", msg, type.ToString(), url);
            return Content(url);
        }
        protected ActionResult Alert(string url)
        {
            // url = "<script>alert(1111);location.href='"+url+"';</script>";
            //return Content(url);
            return Content("<script>" + url + "</script>");
        }

        /// <summary>
        /// 部门树状下拉
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDTreeSelectJson()
        {
            var data = departmentService.GetAllDepartmentInfos();
            var treeList = new List<TreeSelectModel>();
            foreach (DepartmentInfoListDto item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.DepartmentId + "";
                treeModel.text = item.DisplayName;
                treeModel.parentId = item.ParentId + "";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
      
        protected string GetRandom()
        {
            var r = new Random();
            var sb = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                sb.Append(r.Next(0, 9));
            }
            return sb.ToString() + "/";
        }
        #endregion
    }

    #region 扩展
    /// <summary>
    /// 下拉框树状对象
    /// </summary>
    public class TreeSelectModel
    {
        public string id { get; set; }
        public string text { get; set; }
        public string parentId { get; set; }
        public object data { get; set; }
    }
    /// <summary>
    /// 接收前台树状对象（部门添加和课程类型添加）
    /// </summary>
    public class TreeView
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parentId { get; set; }
        public string path { get; set; }
        public string pathName { get; set; }
        public int level { get; set; }
        public bool enabled { get; set; }
        public bool isNew { get; set; }

    }
    /// <summary>
    /// 树状对象转dto
    /// </summary>
    public static class ExtentsTreeToDto
    {
        public static List<DepartmentInfoEditDto> TreeViewToDDto(this List<TreeView> tvList)
        {
            var listdto = new List<DepartmentInfoEditDto>();
            foreach (var item in tvList)
            {
                var dto = new DepartmentInfoEditDto()
                {
                    DepartmentId = item.id,
                    DisplayName = item.name,
                    ParentId = item.parentId,
                    Enabled = item.enabled,
                    Level = item.level,
                    Path = item.path,
                    PathName = item.pathName,
                    IsNew = item.isNew
                };
                listdto.Add(dto);
            }
            return listdto;
        }

        public static List<CourseCategoryEditDto> TreeViewToCDto(this List<TreeView> tvList)
        {
            var listdto = new List<CourseCategoryEditDto>();
            foreach (var item in tvList)
            {
                var dto = new CourseCategoryEditDto();
                dto.CategoryId = item.id;
                dto.ParentId = item.parentId;
                dto.CourseCategoryName = item.name;
                dto.Level = item.level;
                dto.IsNew = item.isNew;
                dto.Path = item.path;
                dto.PathName = item.pathName;
                listdto.Add(dto);
            }
            return listdto;
        }
    }
    /// <summary>
    /// dto对象转树状对象
    /// </summary>
    public static class ExtentsDtoToTree
    {
        public static TreeView DDtoToTreeView(this DepartmentInfoListDto dto)
        {
            var tv = new TreeView()
            {
                id = dto.DepartmentId,
                name = dto.DisplayName,
                parentId = dto.ParentId,
                enabled = dto.Enabled,
                level = dto.Level,
                path = dto.Path,
                pathName = dto.PathName,
                isNew = dto.IsNew
            };
            return tv;
        }

        public static TreeView CToTreeView(this CourseCategoryListDto dto)
        {
            var tv = new TreeView()
            {
                id = (int)dto.CategoryId,
                parentId = dto.ParentId,
                name = dto.CourseCategoryName
            };
            return tv;
        }

    }

    /// <summary>
    /// 绑定下拉框的json对象
    /// </summary>
    public static class TreeSelect
    {
        public static string TreeSelectJson(this List<TreeSelectModel> data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(TreeSelectJson(data, "0", ""));
            sb.Append("]");
            return sb.ToString();
        }
        private static string TreeSelectJson(List<TreeSelectModel> data, string parentId, string blank)
        {
            StringBuilder sb = new StringBuilder();
            var ChildNodeList = data.FindAll(t => t.parentId == parentId);
            var tabline = "";
            if (parentId != "0")
            {
                tabline = "　　";
            }
            if (ChildNodeList.Count > 0)
            {
                tabline = tabline + blank;
            }
            foreach (TreeSelectModel entity in ChildNodeList)
            {
                entity.text = tabline + entity.text;
                string strJson = entity.ToJson();
                sb.Append(strJson);
                sb.Append(TreeSelectJson(data, entity.id, tabline));
            }
            return sb.ToString().Replace("}{", "},{");
        }

        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
    }

    /// <summary>
    /// 返回类型
    /// </summary>
    public enum ReturnType
    {
        success = 1,
        error = 2,
        waining = 3
    }

    #endregion
}