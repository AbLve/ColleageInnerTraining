using Abp.Application.Services.Dto;
using Abp.Web.Security.AntiForgery;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ColleageInnerTraining.Common;
using ColleageInnerTraining.Web.Auth;
using System.Net;
using System.Web.Security;
using System.Text;
using ColleageInnerTraining.Common.Utils;
using System.Web.Script.Serialization;
using ColleageInnerTraining.Web.Controllers;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{    
    public class CourseController : BaseController
    {
        public static string host = System.Configuration.ConfigurationManager.AppSettings["ApiHost"].ToString();
        public static string webHost = System.Configuration.ConfigurationManager.AppSettings["WebHost"].ToString();        

        private ICourseInfoAppService _coreInfoService;
        private ICourseCategoryAppService _coreCategoryService;
        private IDepartmentInfoAppService _departmentService;
        private IJobPostAppService _jobPostService;
        private ICourseBoundPersonnelAppService _boundPerPostService;
        private ICourseBoundConfigureTypeAppService _conTypePostService;
        private ITeachersAppService _teachersService;

        

        public CourseController(ICourseInfoAppService coreInfoService, ICourseCategoryAppService categoryService,
            IDepartmentInfoAppService departmentService,IJobPostAppService jobPostService,
            ICourseBoundPersonnelAppService boundPerPostService, ICourseBoundConfigureTypeAppService conTypePostService,
            ITeachersAppService teachersService)
        {
            _coreInfoService = coreInfoService;
            _coreCategoryService = categoryService;
            _departmentService = departmentService;
            _jobPostService = jobPostService;
            _boundPerPostService = boundPerPostService;
            _conTypePostService = conTypePostService;
            _teachersService = teachersService;
        }      

        #region 课程管理
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.Index")]
        public ActionResult Index()
        {
            #region 下拉框
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

        // 课程管理分页信息
        public ActionResult GetCurriManageDataList(string txtcname = "", string txttname = "", int txttype = 0, int txtcate = 0, 
            int DepartmentId=0, int jobPostId=0, int pIndex = 1)
        {
            ViewBag.pageName = "GetCurriManageDataList";
            var input = new GetCourseInfoInput()
            {
                FilterText = txtcname,
                TeacherName = txttname,
                Type = txttype,
                categoryType = txtcate,
                DepartmentId = DepartmentId,
                JobPostId = jobPostId,
                SkipCount = (pIndex - 1) * PageSize, MaxResultCount = PageSize
            };
            var pagedata = _coreInfoService.GetPagedCourseInfos(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/CurriManageDataList", pagedata);
        }

        //新增修改页面
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.CreateAndEdite")]
        public ActionResult CreateAndEdite(int? id)
        {
            #region 下拉框数据获取
            //内训师
            var teachersDate = _teachersService.GetAllDate().Where(c => c.Status == (int)CourseStatus.Audited);
            var teachersItem = new List<SelectListItem>();
            if (teachersDate != null && teachersDate.Any())
            {
                foreach (var itemt in teachersDate)
                {
                    teachersItem.Add(new SelectListItem { Text = itemt.UserName, Value = itemt.Id.ToString() });
                }
            }
            ViewBag.TeacharData = teachersItem;

            #endregion
            
            var resultCoreInfo = _coreInfoService.GetCourseInfoForEdit(new NullableIdDto<long>() { Id = id });
            if (resultCoreInfo.CourseInfo.Id == null || resultCoreInfo.CourseInfo.Id == 0)
            {
                resultCoreInfo.CourseInfo.StartTime = System.DateTime.Now;
                resultCoreInfo.CourseInfo.EndTime = System.DateTime.Now;
            }

            return View(resultCoreInfo.CourseInfo);
        }

        /// <summary>
        /// 课程分类
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCateTreeSelectJson()
        {
            var data = _coreCategoryService.GetCourseCategoryList();
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.CategoryId + "";
                treeModel.text = item.CourseCategoryName;
                treeModel.parentId = item.ParentId + "";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
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


        //新增和修改数据
        [HttpPost]
        [ValidateInput(false)]
        [Route("/Course/Save")]
        [ValidateAntiForgeryToken]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.Save")]
        public ActionResult Save(CourseInfoEditDto input)
        {
            try
            {
                var createOrUpdate = new CreateOrUpdateCourseInfoInput();
                input.TypeName = EnumDescription.GetFieldText((CourseType)input.Type);
                createOrUpdate.CourseInfoEditDto = input;
                if (input.Content != null)
                {
                    input.Content = input.Content.Replace(Environment.NewLine, "<br>");
                }
                
                if (input.Content != "") createOrUpdate.CourseInfoEditDto.Content = HttpUtility.HtmlDecode(input.Content);
                _coreInfoService.CreateOrUpdateCourseInfo(createOrUpdate);
                return RedirectToAction("/Index");

            }
            catch (Exception ex){ }
            return RedirectToAction("/CreateAndEdite", input);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>

        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.DeleteById")]
        public ActionResult DeleteById(int id)
        {
            _coreInfoService.DeleteCourseInfo(new EntityDto<long>() { Id = id });
            return RedirectToAction("/Index");
        }
        //删除多条数据
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.DeleteByIds")]
        public ActionResult DeleteByIds(List<long> id)
        {
            _coreInfoService.BatchDeleteCourseInfo(id);
            return RedirectToAction("/Index");
        }
        [HttpPost]
        [DisableAbpAntiForgeryTokenValidation]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.UploadfyPic")]
        public ActionResult UploadfyPic()
        {
            var result = new resultDate();
            try
            {
                Response.ContentType = "text/plain";
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["importFile"];
                string uploadPath = Server.MapPath("/Uploads/Pic/");
                if (file != null)
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    //保存文件
                    file.SaveAs(uploadPath + file.FileName);
                    result.IsSuccess = "0";
                    result.filePath = "/Uploads/Pic/" + file.FileName;
                }
                else
                {
                    result.IsSuccess = "-1";
                }
            }
            catch (System.Exception)
            {

                result.IsSuccess = "-1";
            }                             
            return Json(result);
        }

        //课程和考试配置界面
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.CurExaDisposition")]
        public ActionResult CurExaDisposition(int courseId)
        {
            var resultCoreInfo = _coreInfoService.GetCourseInfoForEdit(new NullableIdDto<long>() { Id = courseId });
            return View(resultCoreInfo.CourseInfo);
        }

        //只修改课程和考试
        [DisableAbpAntiForgeryTokenValidation]
        public ActionResult UpdateCurExa(int courseId, int ExaId, int PollId)
        {
            var result = "-1";
            var resultCoreInfo = _coreInfoService.GetCourseInfoEditById(new EntityDto<long>() { Id = courseId });
            if (resultCoreInfo != null)
            {
                resultCoreInfo.ExaminationId = ExaId;
                resultCoreInfo.PollId = PollId;
                _coreInfoService.UpdateCourseInfo(resultCoreInfo);
                result = "0";
            }
            return Content(result);
        }

        #endregion

        #region 课程审核
        //课程审核
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.CheckCourse")]
        public ActionResult CheckCourse()
        {
            var courseCategoryList = _coreCategoryService.GetCourseCategoryList();
            var courseCategoryItem = new List<SelectListItem>();
            if (courseCategoryList != null && courseCategoryList.Any())
            {
                foreach (var item in courseCategoryList)
                {
                    courseCategoryItem.Add(new SelectListItem { Text = item.CourseCategoryName, Value = item.Id.ToString() });
                }
            }
            ViewBag.CourseCategoryData = courseCategoryItem;
            return View();
        }

        //课程审核分页信息
        public ActionResult GetCheckCourseDataList(string txtcname = "", string txttname = "", int txttype = 0, int txtcate = 0, int pIndex = 1)
        {
            ViewBag.pageName = "GetCheckCourseDataList";
            var input = new GetCourseInfoInput()
            {
                FilterText = txtcname,
                TeacherName = txttname,
                Type = txttype,
                //CheckStatus = (int)CourseStatus.Pending,
                categoryType = txtcate,
                SkipCount = (pIndex - 1) * PageSize,
                MaxResultCount = PageSize
            };
            var pagedata = _coreInfoService.GetPagedCourseInfos(input);
            GetPageData(pagedata.TotalCount);
            return PartialView("Shared/CheckCourseDataList", pagedata);
        }

        //课程审核
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Course.Check")]
        public ActionResult Check(int id, int status)
        {
            var result = "-1";
            var request = "-1";
            var resultCoreInfo = _coreInfoService.GetCourseInfoForEdit(new NullableIdDto<long>() { Id = id });
            if (resultCoreInfo != null)
            {
                #region 操作必学人员接口
                if (status == 1)
                {
                    //个人信息
                    var personList = _boundPerPostService.GetCourseBoundUserByCourseId((int)resultCoreInfo.CourseInfo.Id);
                    if (personList != null && personList.Any())
                    {
                        foreach (var Pitem in personList)
                        {
                            HttpRequestExamRequire((int)ConfigureType.Personal, Pitem.AccountSysNo, Pitem.AccountUserName, resultCoreInfo.CourseInfo.ExaminationId);
                        }
                    }
                    //类型访问
                    var type = _conTypePostService.GetCTypeByCourseId((int)resultCoreInfo.CourseInfo.Id);
                    if (type != null && type.Any())
                    {
                        foreach (var Citem in type)
                        {
                            HttpRequestExamRequire(Citem.type, Citem.BusinessId, Citem.BusinessName, resultCoreInfo.CourseInfo.ExaminationId);
                        }
                    }
                    request = "0";
                }
                #endregion

                if (request == "0" && status == 1)
                {
                    resultCoreInfo.CourseInfo.Status = (int)CourseStatus.Audited;
                }
                else if (status == 0)
                {
                    resultCoreInfo.CourseInfo.Status = (int)CourseStatus.Fail;
                }
                _coreInfoService.UpdateCourseInfo(resultCoreInfo.CourseInfo);
                result = "0";
            }
            return Content(result);
        }

        public ActionResult CourseDetailt( int id, int tc)
        {
            var resultCoreInfo = _coreInfoService.GetCourseInfoById(new EntityDto<long>() { Id = id });
            resultCoreInfo.IsTypeC = tc;
            return View(resultCoreInfo);
        }
        #endregion

        public class resultDate
        {
            public string IsSuccess { get; set; }
            public string filePath { get; set; }
        }


        #region  API必学人员

        public static string appkey = System.Configuration.ConfigurationManager.AppSettings["Appkey"].ToString();   
        //= "17b6c774-5223-432a-91ef-2db032fdbd4a";
        public static string appsecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"].ToString();   
        //= "46a27273-9f56-4e7b-9cb4-6b29dc90f4ad";

        //API请求
        public static string HttpRequestExamRequire(int type, int bId, string bName, int examId)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
            string sign = MD5Encrypt(MD5Encrypt(appkey + timestamp) + appsecret);
            var url = host + "/api/exam/exam/saveExamRequire?";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("appkey", appkey);
            dictionary.Add("sTimestamp", timestamp);
            dictionary.Add("sign", sign);
            dictionary.Add("type", type.ToString());
            dictionary.Add("bizId", bId.ToString());
            dictionary.Add("bizName", bName.ToString());
            dictionary.Add("examId", examId.ToString());
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var parmit = javaScriptSerializer.Serialize(dictionary);
            byte[] bytes = Encoding.UTF8.GetBytes(parmit);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);
            //声明一个HttpWebRequest请求  
            request.Timeout = 90000;
            //设置连接超时时间  
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string strResult = streamReader.ReadToEnd();
            streamReceive.Dispose();
            streamReader.Dispose();
            return strResult;
        }

        private static string MD5Encrypt(string strText)
        {
            strText = FormsAuthentication.HashPasswordForStoringInConfigFile(strText, "MD5").ToLower();
            return strText;
        }

        #endregion

        #region 二维码问题
        public ActionResult QRCode(int courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        public ActionResult CreateQrCode(int courseId)
        {
            var url = webHost + "/Wap/Member/CheckIn?CourseId=" + courseId.ToString();
            using (var memoryStream = QRCodeHelper.GetQRCode(url, 9))
            {
                Response.ContentType = "image/jpeg";
                Response.OutputStream.Write(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
                Response.End();
            }
            return null;
        }

        #endregion
    }
}