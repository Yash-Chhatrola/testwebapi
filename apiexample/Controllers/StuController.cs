using apiexample.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace apiexample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StuController : ControllerBase
    {
        //private static List<Student> Students = new List<Student>
        //{
        //    new Student{Rno=1,Name="Yash Patel",Branch="Computer",Fees=10000 },
        //    new Student{Rno=2,Name="Jay Kumar",Branch="Mechanical",Fees=15000 }
        //};
        private readonly ApplicationDbContext dbContext;
        public StuController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
           var stud = dbContext.students.ToList();

            return Ok(stud);
        }
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = dbContext.students.FirstOrDefault(s => s.Rno == id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            if(dbContext.students.Any(s => s.Rno == student.Rno))
            {
                return BadRequest("Student with the same Rno already exists.");
            }
            dbContext.students.Add(student);
            dbContext.SaveChanges();
            return CreatedAtAction("GetStudent", new { id = student.Rno }, student);
        }
        [HttpPut("{id}")]
        public ActionResult<Student>PutStudent(int id,Student student)
        {
            var existingStudent = dbContext.students.FirstOrDefault(s => s.Rno == id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.Name = student.Name;
            existingStudent.Branch = student.Branch;
            existingStudent.Fees = student.Fees;

            dbContext.SaveChanges();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public ActionResult<Student>DeleteStudent(int id)
        {
            var student = dbContext.students.FirstOrDefault(s => s.Rno == id);
            if(student == null)
            {
                return NotFound();
            }
            dbContext.students.Remove(student);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
