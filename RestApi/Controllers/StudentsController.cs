using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Data;
using RestApi.Migrations;
using RestApi.Models;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly RestApiDbContext _context;

        public StudentsController(RestApiDbContext context)
        {
            _context = context;
        }

        // GET:  api/students?token=?

        //cxczxcx

        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] string token)
        {
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Token missing" });

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.token == token);

            if (user == null)
                return Unauthorized(new { message = "Invalid token" });

            var students = await _context.Students
                .Where(s => s.userId == user.Id)
                .Select(s => new
                {
                    st_id = s.Id,
                    st_name = s.FullName,
                    st_father_mame = s.FatherName,
                    st_phone = s.Phone,
                    st_email = s.Email,
                    st_course = s.Course,
                    st_city = s.City,
                    st_address = s.Address,
                    st_description = s.Description
                })
                .ToListAsync();
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(students));
            return Ok(students);
        }



        [HttpGet("{student_roll_no}/{token}")]
        public async Task<IActionResult> GetStudent(int student_roll_no, string token) {

            if(string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Token missing." });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.token == token);
            if (user == null)
                return Unauthorized(new { message = "Invalid token." });

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.RollNo == student_roll_no && s.userId == user.Id);

            if (student == null)
                return NotFound(new { message = $"Student with Roll No {student_roll_no} not found." });

            return Ok(new
            {
                st_id = student.Id,
                st_roll_no = student.RollNo,
                st_name = student.FullName,
                st_father_name = student.FatherName,
                st_phone = student.Phone,
                st_email = student.Email,
                st_course = student.Course,
                st_city = student.City,
                st_address = student.Address,
                st_description = student.Description
            });
        }


        // POST: api/students
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student added successfully", student });
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            student.FullName = updatedStudent.FullName;
            student.FatherName = updatedStudent.FatherName;
            student.Email = updatedStudent.Email;
            student.Phone = updatedStudent.Phone;
            student.Address = updatedStudent.Address;
            student.City = updatedStudent.City;
            student.Course = updatedStudent.Course;
            student.Description = updatedStudent.Description;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Student updated successfully", student });
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok($"Student with ID {id} deleted successfully.");
        }
    }
}
