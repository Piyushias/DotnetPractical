using Azure;
using DotnetPractical.DataBase;
using DotnetPractical.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.Packaging;
using System.Linq;
using System.Xml.Schema;

namespace DotnetPractical.Controllers
{
	public class StudentController : Controller
	{
		private readonly StudentDatabase database;

		public StudentController(StudentDatabase database)
		{
			this.database = database;
		}
		
		[HttpGet]
		public IActionResult CreateStudent()
		{
			List<Subject> Subjects = database.Subjects.ToList();
			var selectList = new SelectList(Subjects, "Id", "Name");
			ViewBag.SelectList = selectList;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateStudent(Student std)
		{
			if (std.Subjects.Count > 0)
			{
				std.SubjectsId = string.Join(",", std.Subjects);
			}
			if (std.FormFilePic is not null && std.FormFilePic.ContentType.Contains("image"))
			{
				using (var stream = new MemoryStream())
				{
					await std.FormFilePic.CopyToAsync(stream);
					std.Image = stream.ToArray();
				}
			}
			//std.Subjects = new();
			//s//td.Subjects.Add(std.Subject);
			database.Students.Add(std);
			database.SaveChanges();
			return RedirectToAction("GetStudentDetails");
			
		}

		[HttpGet]
		public async Task<IActionResult> GetStudentDetails( string searchString)
		{
			var orderbyStudent = database.Students.OrderByDescending(x => x.Standard).ToList();
			return View(orderbyStudent);
		}

		[HttpPost]
		public async Task<IActionResult> SearchSTudentByName(IFormCollection collection)
		{
			string stdName = collection["Search"];
			var searchedResult = database.Students.Where(x => x.Student_Name.Contains(stdName)).ToList();
			return View("GetStudentDetails", searchedResult);
		}

		[HttpGet]
		public async Task<IActionResult> AddSubject()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddSubject(Subject subject)
		{

			var languageString = string.Join(",", subject.Languages);
			subject.Language = languageString;
			database.Subjects.Add(subject);
			database.SaveChanges();
			return View();
		}


		[HttpGet]
		public async Task<IActionResult> GetFullDatails()
		{
			List<Student> studentsCollection = new();
			var Students = database.Students.ToList();
			foreach (var student in Students)
			{
				student.Subject = new();
                var SubjectId=student.SubjectsId.Split(",").ToList();
				var Subjects = database.Subjects.Where(x => SubjectId.Contains(x.Id.ToString())).ToList();
				student.Subject.AddRange(Subjects);

                foreach (var SubId in student.Subject)
				{
					SubId.Teachers = new();
                    var teachers = database.Teachers.Where(x => x.SubjectIds.Contains(SubId.Id.ToString())).ToList();
                    SubId.Teachers.AddRange(teachers);
                }

                studentsCollection.Add(student);
            }

			return View(studentsCollection);
		}

		[HttpGet]
		public IActionResult AddTeacher()
		{
			List<Subject> Subjects = database.Subjects.ToList();
			var selectList = new SelectList(Subjects, "Id", "Name");
			ViewBag.SelectList = selectList;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AddTeacher(Teacher teacher)
		{

            if (teacher.SubjectId.Count > 0)
            {
                teacher.SubjectIds = string.Join(",", teacher.SubjectId);
            }
            if (teacher.FormFilePic is not null && teacher.FormFilePic.ContentType.Contains("image"))
            {
                using (var stream = new MemoryStream())
                {
                    await teacher.FormFilePic.CopyToAsync(stream);
                    teacher.Image = stream.ToArray();
                }
            }
            database.Teachers.Add(teacher);
			database.SaveChanges();
			return RedirectToAction(nameof(AddTeacher));
		}
	}
}
