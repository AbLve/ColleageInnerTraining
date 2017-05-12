using Abp.Application.Services.Dto;
using Abp.Web.Security.AntiForgery;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 公告
    /// </summary>
    public class NoticeController : BaseController
    {
        private const string ex = "GZSXY.Pages.Manage.System.NoticeManage.";

        #region 注入服务
        private INoticeAppService noticeservice;
        public NoticeController(INoticeAppService _noticeservice)
        {
            noticeservice = _noticeservice;
        }
        #endregion

        #region 操作

        /// <summary>
        /// 公告index
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Index")]
        [Route("/Notice/Index")]
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 公告分布页
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="type"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        [Route("/Notice/GetDataList")]
        public ActionResult GetDataList(string keyword, int type = 0, int pIndex = 1)
        {
            ViewBag.pageName = "GetDataList";
            var input = new GetNoticeInput() { FilterText = keyword, SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = noticeservice.GetPagedNotices(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/DataList", pagedata.Items);
        }

        /// <summary>
        /// 公告表单页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Create")]
        [Route("/Notice/Create")]
        public ActionResult Create(int id = 0)
        {
            var model = new GetNoticeForEditOutput();
            if (id > 0)
            {
                model =  noticeservice.GetNoticeForEdit(new NullableIdDto<long> { Id = id });
            }
            return View(model.Notice);
        }

        [HttpPost]
        [Route("/Notice/Save")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NoticeEditDto dto)
        {
            try
            {
                if (dto.Content != null)
                {
                    dto.Content = HttpUtility.HtmlDecode(dto.Content.Replace(Environment.NewLine, "<br>"));
                }

                noticeservice.CreateOrUpdateNoticeAsync(new CreateOrUpdateNoticeInput { NoticeEditDto = dto });
                return RedirectToAction("/Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Delete")]
        [Route("/Notice/Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                noticeservice.DeleteNoticeAsync(new EntityDto<long> { Id = id });
                return Success();
            }
            catch (Exception e)
            {
                return Fail();
            }
        }

        /// <summary>
        /// 上传素材
        /// </summary>
        /// <returns></returns>
        [DisableAbpAntiForgeryTokenValidation]
        [Route("/Notice/UploadFile")]
        public JsonResult UploadFile()
        {
            var file = Request.Files["NoticeImg"];
            var path = "/uploads/Noticeimg/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            return Upload(file, path);
        }

        #endregion
    }
}