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

        // Get all students for the authenticated user
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
        public async Task<IActionResult> AddStudent([FromQuery] string token, Student st)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new
                    {
                        status = 401,
                        message = "Token missing."
                    });

                var user = await _context.Users.FirstOrDefaultAsync(u => u.token == token);
                if (user == null)
                    return Unauthorized(new
                    {
                        status = 401,
                        message = "Invalid token."
                    });

                if (st.RollNo == 0 || st.RollNo == null)
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Roll number is required."
                    });

                // Check for duplicate roll number for the same user

                int roll_no = st.RollNo.Value;

                // 409: Duplicate roll number

                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.RollNo == roll_no && s.userId == user.Id);

                if (existingStudent != null)
                    return Conflict(new
                    {
                        status = 409,
                        message = "Roll number already exists."
                    });

                var newStudent = new Student {
                    userId = user.Id,
                    RollNo = st.RollNo,
                    FullName = st.FullName,
                    FatherName = st.FatherName,
                    Email = st.Email,
                    Phone = st.Phone,
                    Address = st.Address,
                    City = st.City,
                    Course = st.Course,
                    Description = st.Description
                };

                await _context.Students.AddAsync(newStudent);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Student added successfully.",
                    student = newStudent
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    message = "Internal server error.",
                    error = ex.Message
                });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent( int id,[FromQuery] string token,[FromBody] Student updatedStudent)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Token missing."
                    });
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.token == token);
                if (user == null)
                {
                    return Unauthorized(new
                    {
                        status = 401,
                        message = "Invalid token."
                    });
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == id && s.userId == user.Id);

                if (student == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = $"Student with ID {id} not found or you do not have permission to update."
                    });
                }
                if (updatedStudent.RollNo != student.RollNo)
                {
                    bool duplicate = await _context.Students.AnyAsync(s =>
                        s.RollNo == updatedStudent.RollNo && s.userId == user.Id);

                    if (duplicate)
                    {
                        return BadRequest(new
                        {
                            status = 400,
                            message = "Roll number already exists."
                        });
                    }
                }

                student.RollNo = updatedStudent.RollNo;
                student.FullName = updatedStudent.FullName;
                student.FatherName = updatedStudent.FatherName;
                student.Email = updatedStudent.Email;
                student.Phone = updatedStudent.Phone;
                student.Address = updatedStudent.Address;
                student.City = updatedStudent.City;
                student.Course = updatedStudent.Course;
                student.Description = updatedStudent.Description;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Student updated successfully.",
                    student
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    message = "An internal server error occurred.",
                    error = ex.Message
                });
            }
        }

        // DELETE: api/students/{id}?token=?
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id,[FromQuery] string token){
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new
                    {
                        status = 400,
                        message = "Token missing."
                    });
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.token == token);
                if (user == null)
                {
                    return Unauthorized(new
                    {
                        status = 401,
                        message = "Invalid token."
                    });
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.Id == id && s.userId == user.Id);

                if (student == null)
                {
                    return NotFound(new
                    {
                        status = 404,
                        message = "Student not found or you don't have permission to delete."
                    });
                }
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = 200,
                    message = "Student deleted successfully.",
                    studentId = id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = 500,
                    message = "An error occurred while deleting the student.",
                    error = ex.Message
                });
            }
        }

    }
}
