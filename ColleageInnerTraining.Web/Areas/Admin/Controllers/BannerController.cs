using Abp.Application.Services.Dto;
using Abp.Web.Security.AntiForgery;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Web.Auth;
using ColleageInnerTraining.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 轮播图管理
    /// </summary>
    public class BannerController : BaseController
    {
        private const string ex = "GZSXY.Pages.Manage.System.BannerManage.";

        #region 注入服务
        private IBannerAppService bannerservice;
        public BannerController(IBannerAppService _bannerservice)
        {
            bannerservice = _bannerservice;
        }
        #endregion

        #region 操作

        /// <summary>
        /// 轮播图index
        /// </summary>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Index")]
        [Route("/Banner/Index")]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 分布页
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="enabled"></param>
        /// <param name="type"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        [Route("/Banner/GetDataList")]
        public ActionResult GetDataList(string keyword, bool enabled = true, int type = 0, int pIndex = 1)
        {
            ViewBag.pageName = "GetDataList";
            var input = new GetBannerInput() { Title = keyword, ClientType = type, Enabled = enabled, SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize };
            var pagedata = bannerservice.GetPagedBanners(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/DataList", pagedata.Items);
        }
        /// <summary>
        /// 轮播图表单页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Create")]
        [Route("/Banner/Create")]
        public async Task<ActionResult> Create(int id = 0)
        {
            var model = new GetBannerForEditOutput();
            if (id > 0)
            {
                model = await bannerservice.GetBannerForEditAsync(new NullableIdDto<long> { Id = id });
            }      
            return View(model.BannerEditDto);
        }
        /// <summary>
        /// 保存轮播图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/Banner/Save")]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BannerEditDto dto)
        {
            try
            {
                bannerservice.CreateOrUpdateBannerAsync(new CreateOrUpdateBannerInput { BannerEditDto = dto });
                return RedirectToAction("/Index");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [PermissionAuthorizeAttribute(ex + "Delete")]
        [Route("/Banner/Delete")]
        public ActionResult Delete(int id)
        {
            try
            {
                bannerservice.DeleteBannerAsync(new EntityDto<long> { Id = id });
                return Success();
            }
            catch (Exception e)
            {
                return Fail();
            }
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <returns></returns>
        [DisableAbpAntiForgeryTokenValidation]
        [Route("/Banner/UploadFile")]
        public JsonResult UploadFile()
        {
            var file = Request.Files["BannerImg"];
            var path = "/uploads/bannerimg/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            return Upload(file, path);
        }

        #endregion
    }
}