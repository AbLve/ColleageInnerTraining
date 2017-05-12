using System;
using System.Collections.Generic;
using ColleageInnerTraining.Web.Models.Strudents;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.Domain.Uow;
using System.Threading.Tasks;
using ColleageInnerTraining.StudentService;

namespace ColleageInnerTraining.Web.Controllers
{
    public class StudentController : ColleageInnerTrainingControllerBase
    {


        private IStudentAppService studentService;


        public StudentController(IStudentAppService service)
        { studentService = service; }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Add()
        {
            String id = Request.Params["Id"];
            StudentViewModel viewModel;
            if (id != null && id != "")
            {
                Student student = studentService.GetById(long.Parse(id));
                viewModel = new StudentViewModel { Id = student.Id, Name = student.Name,BirthDate = student.BirthDate,Gender = student.Gender };
                return View(viewModel);
            }
            else {
                viewModel = new StudentViewModel { Id = 0,Name="", BirthDate = DateTime.Parse(System.DateTime.Now.ToString("yyyy-MM-dd")),Gender = true };
                return View(viewModel);
            }
            
        }


        [HttpPost]
        [UnitOfWork]
        public ActionResult Save(StudentViewModel viewModel)
        {
            
            
            CheckModelState();
            
            var student = new Student
            {
                Id= viewModel.Id,
                Name = viewModel.Name,
                BirthDate = viewModel.BirthDate,
                Gender = true

            };
            if (student.Id == 0) { 
            studentService.Save(student);
            }
            else
                studentService.Update(student);
            return RedirectToAction("Index", "Home");
        }

    }
}