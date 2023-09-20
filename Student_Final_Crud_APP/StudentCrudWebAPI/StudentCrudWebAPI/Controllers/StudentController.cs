using Microsoft.AspNetCore.Mvc;
using StudentCrudWebAPI.Models;
using StudentCrudWebAPI.Repository;

namespace StudentCrudWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudent _studentService;

        public StudentController(IStudent studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<StudentModel>> Get()
        {
            List<StudentModel> StdData = _studentService.GetAll();
            return Ok(StdData);
        }


        [HttpGet("{id}")]
        public ActionResult<StudentModel> Get(int id)
        {
            StudentModel StdData = _studentService.GetById(id);
            if (StdData == null)
            {
                return NotFound();
            }
            return Ok(StdData);
        }

        [HttpPost]
        public ActionResult Post([FromBody] StudentModel StdData)
        {
            if (ModelState.IsValid)
            {
                _studentService.Add(StdData);
                return CreatedAtAction(nameof(Get), new { id = StdData.Id }, StdData);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] StudentModel StdData)
        {
            if (ModelState.IsValid)
            {
                if (_studentService.Update(id, StdData))
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_studentService.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
