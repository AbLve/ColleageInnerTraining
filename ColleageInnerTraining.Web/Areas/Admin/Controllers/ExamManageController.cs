using Abp.Application.Services.Dto;
using Abp.Web.Mvc.Controllers;
using ColleageInnerTraining.Application;
using ColleageInnerTraining.Application.Dtos;
using ColleageInnerTraining.Core;
using ColleageInnerTraining.Web.Areas.Admin.Models;
using ColleageInnerTraining.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ColleageInnerTraining.Web.Areas.Admin.Controllers
{
    public class ExamManageController : AbpController
    {
        private readonly IDepartmentInfoAppService _departmentInfoAppService;
        private readonly IJobPostAppService _jobPostAppService;
        private readonly IClassesInfoAppService _classesInfoAppService;
        private IUserAccountAppService _userAccountService;
        public ExamManageController(IDepartmentInfoAppService departmentInfoAppService, IJobPostAppService jobPostAppService,
            IClassesInfoAppService classesInfoAppService, IUserAccountAppService userAccountService)
        {
            _departmentInfoAppService = departmentInfoAppService;
            _jobPostAppService = jobPostAppService;
            _classesInfoAppService = classesInfoAppService;
            _userAccountService = userAccountService;
        }
        /// <summary>
        /// 考试管理
        /// </summary>
        /// <returns></returns>
        // GET: Admin/ExamList
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamManage.ExamList")]
        [Route("Admin/ExamManage/ExamList")]
        public ActionResult Index()
        {
            var examViewModel = new ExamViewModel();
            var departs = _departmentInfoAppService.GetAllDepartmentInfos().OrderBy(p => p.Path).Select(p => new ComboboxItemDto(p.DepartmentId.ToString() + "," + p.DisplayName, new string('　', p.Level * 2) + " " + p.DisplayName))
                                                   .ToList();
            examViewModel.departLists = departs;
            return View(examViewModel);
        }

        /// <summary>
        /// 新建考试
        /// </summary>
        /// <returns></returns>
        // GET: Admin/ExamList
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamManage.CreateExam")]
        [Route("Admin/ExamManage/CreateExam")]
        public ActionResult CreateExam()
        {
            var examViewModel = new ExamViewModel();
            return View(examViewModel);
        }

        /// <summary>
        /// 编辑考试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamManage.EditExam")]
        [Route("Admin/ExamManage/EditExam")]
        public ActionResult EditExam()
        {
            string examId = Request.QueryString["examId"];

            var examViewModel = new ExamViewModel();
            examViewModel.ExamId = int.Parse(examId);
            return View("~/Areas/Admin/Views/ExamManage/EditExam.cshtml", examViewModel);
        }
        /// <summary>
        /// 试卷管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamManage.PaperManage")]
        [Route("Admin/ExamManage/PaperManage")]
        public ActionResult PaperManage()
        {
            string examId = Request.QueryString["examId"];
            var examViewModel = new ExamViewModel();
            examViewModel.ExamId = int.Parse(examId);
            return View("~/Areas/Admin/Views/ExamManage/PaperManage.cshtml", examViewModel);
        }
        /// <summary>
        /// 必考关系管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamManage.SetRelation")]
        [Route("Admin/ExamManage/SetRelation")]
        public ActionResult SetRelation()
        {
            string examId = Request.QueryString["examId"];
            var examViewModel = new ExamViewModel();
            examViewModel.ExamId = int.Parse(examId);
            var departs = _departmentInfoAppService.GetAllDepartmentInfos().OrderBy(p => p.Path).Select(p => new ComboboxItemDto(p.DepartmentId.ToString() + "," + p.DisplayName, new string('　', p.Level * 2) + " " + p.DisplayName))
                                                   .ToList();
            examViewModel.departLists = departs;
            examViewModel.posts = _jobPostAppService.GetAllJobPosts();
            examViewModel.classes = _classesInfoAppService.GetAllClassesInfos();
            return View("~/Areas/Admin/Views/ExamManage/SetRelation.cshtml", examViewModel);
        }
        /// <summary>
        /// 配置人员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamManage.SetUsers")]
        [Route("Admin/ExamManage/SetUsers")]
        public ActionResult SetUsers()
        {
            string examId = Request.QueryString["examId"];
            var examViewModel = new ExamViewModel();
            examViewModel.ExamId = int.Parse(examId);
            examViewModel.departs = _departmentInfoAppService.GetAllDepartmentInfos();
            examViewModel.posts = _jobPostAppService.GetAllJobPosts();
            examViewModel.classes = _classesInfoAppService.GetAllClassesInfos();
            return View("~/Areas/Admin/Views/ExamManage/SetUsers.cshtml", examViewModel);
        }


        /// <summary>
        /// 为考试添加人员
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="departmentId"></param>
        /// <param name="jobPostId"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        [HttpGet]
        [PermissionAuthorizeAttribute("GZSXY.Pages.Manage.Exam.ExamManage.AddUsers")]
        [Route("Admin/ExamManage/AddUsers")]
        public ActionResult AddUsers(string keyword = "", int departmentId = 0, int jobPostId = 0, int pIndex = 1)
        {
            string examId = Request.QueryString["examId"];
            var examViewModel = new ExamViewModel();
            examViewModel.ExamId = int.Parse(examId);
            return View("~/Areas/Admin/Views/ExamManage/addUsers.cshtml", examViewModel);
        }




        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SendMail()
        //{

            
        //    //邮件传输协议类
        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //    client.Host = "116.52.7.131";//邮件服务器
        //    client.Port = 25;//smtp主机上的端口号,默认是25.
        //    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;//邮件发送方式:通过网络发送到SMTP服务器
        //    client.Credentials = new System.Net.NetworkCredential("college", "abcd@1234");//凭证,发件人登录邮箱的用户名和密码

        //    //电子邮件信息类
        //    System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress("college@gznb.com");//发件人Email,在邮箱是这样显示的,[发件人:小明<panthervic@163.com>;]
        //    System.Net.Mail.MailAddress toAddress = new System.Net.Mail.MailAddress("co01@gznb.com", "");//收件人Email,在邮箱是这样显示的, [收件人:小红<43327681@163.com>;]
        //    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(fromAddress, toAddress);//创建一个电子邮件类
        //    mailMessage.Subject = "测试邮件";
        //    //string filePath = Server.MapPath("/index.html");//邮件的内容可以是一个html文本.
        //    //System.IO.StreamReader read = new System.IO.StreamReader(filePath, System.Text.Encoding.GetEncoding("GB2312"));
        //    string mailBody = "测试邮件";
        //    mailMessage.Body = mailBody;//可为html格式文本
        //    //mailMessage.Body = "邮件的内容";//可为html格式文本
        //    mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;//邮件主题编码
        //    mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");//邮件内容编码
        //    mailMessage.IsBodyHtml = true;//邮件内容是否为html格式
        //    mailMessage.Priority = System.Net.Mail.MailPriority.High;//邮件的优先级,有三个值:高(在邮件主题前有一个红色感叹号,表示紧急),低(在邮件主题前有一个蓝色向下箭头,表示缓慢),正常(无显示).
        //    try
        //    {
        //        client.Send(mailMessage);//发送邮件
        //        //client.SendAsync(mailMessage, "ojb");异步方法发送邮件,不会阻塞线程.
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return View();
        //}
    }
}