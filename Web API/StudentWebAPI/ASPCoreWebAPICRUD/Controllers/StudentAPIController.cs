using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]/[action]")] //Provide attributes for define routing pattern
    [ApiController]  // define the behaviour of controller 
    public class StudentAPIController : ControllerBase  // inheit the ControllerBase class for creating API Controller
    {
        private readonly DBContext context;  // it is use to store the instace of a database context

        public StudentAPIController(DBContext context)  // constructor of class accepting parameter of type DBContext
        {
            this.context = context;  // asigning the value of context
        }


        [HttpGet]  // it indicates the method should respond to HTTP GET 
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> GetStudents()  // Asynchronous action method that means it can execute non-blocking operations
        {
            var data = await context.SHUBHAM_STUDENT_CRUD.ToListAsync();  // Asynchronous database query
            return Ok(data);  // statement returns an HTTP reponde to the client along with data
        }

        [HttpGet("{id}")]  //attribute is used to define a route parameter along with Id   
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> GetStudents(int id)    // Get the student using id
        {
            var student = await context.SHUBHAM_STUDENT_CRUD.FindAsync(id);   // find the record inside the table by using id primary key attribute
            if (student == null)  // check data is null or not 
            {
                return NotFound();  // if data was null it will return NotFound
            }
            return Ok(student); // statement returns an HTTP respond to the client along with data
        }


        [HttpPut("{id}")]  // to update the record using ID
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> UpdateStudentsByID(int id, SHUBHAM_STUDENT_CRUD std)  // Update student details by using Id 
        {
      
            if (id != std.Id)   // check id is match inside the table or not
            { 
                return BadRequest();  // if it is not match return BadRequest 
            }
            context.Entry(std).State = EntityState.Modified;   //it is used to mark an entity as modified in the context's
            await context.SaveChangesAsync();    // Asynchronsly save changes 
            return Ok(std);  // return the HTTP response 
        }


        // Add a new record 
        [HttpPost]
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> AddStudent(SHUBHAM_STUDENT_CRUD student)
        {
            await context.SHUBHAM_STUDENT_CRUD.AddAsync(student);
            await context.SaveChangesAsync();
            return Ok(student);
        }


        // Delete the existing record by using ID
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SHUBHAM_STUDENT_CRUD>>> DeleteStudentsByID(int id)
        {
            var std = await context.SHUBHAM_STUDENT_CRUD.FindAsync(id);  // find the record using id
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
