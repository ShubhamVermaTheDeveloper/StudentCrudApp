using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly DBContext context;

        public StudentAPIController(DBContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> GetStudents()
        {
            var data = await context.SHUBHAM_STUDENT_CRUD.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> GetStudents(int id)
        {
            var student = await context.SHUBHAM_STUDENT_CRUD.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> UpdateStudentsByID(int id, SHUBHAM_STUDENT_CRUD std)
        {
      
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPost]
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> AddStudent(SHUBHAM_STUDENT_CRUD student)
        {
            await context.SHUBHAM_STUDENT_CRUD.AddAsync(student);
            await context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> DeleteStudentsByID(int id)
        {
            var std = await context.SHUBHAM_STUDENT_CRUD.FindAsync(id);
            if (std == null)
            {
                return NotFound();
            }
            context.SHUBHAM_STUDENT_CRUD.Remove(std);
           await context.SaveChangesAsync();
            return Ok();
        }
    }
}
