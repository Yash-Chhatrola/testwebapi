using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace apiexample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StuController : ControllerBase
    {
        private static List<Student> Students = new List<Student>
        {
            new Student{Rno=1,Name="Yash Patel",Branch="Computer",Fees=10000 },
            new Student{Rno=2,Name="Jay Kumar",Branch="Mechanical",Fees=15000 }
        };
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            return Students;
        }
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = Students.FirstOrDefault(s => s.Rno == id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            if(Students.Any(s => s.Rno == student.Rno))
            {
                return BadRequest("Student with the same Rno already exists.");
            }
            Students.Add(student);
            return CreatedAtAction("Getstudent", new { id = student.Rno }, student);
        }
        [HttpPut("{id}")]
        public ActionResult<Student>PutStudent(int id,Student student)
        {
            var existingStudent = Students.FirstOrDefault(s => s.Rno == id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = student.Name;
            existingStudent.Branch = student.Branch;
            existingStudent.Fees = student.Fees;
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult<Student>DeleteStudent(int id)
        {
            var student = Students.FirstOrDefault(s => s.Rno == id);
            if(Students == null)
            {
                return NotFound();
            }
            Students.Remove(student);
            return NoContent();
        }
    }
}
