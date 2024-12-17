using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]

        public IActionResult GetAllStudents()
        {
            string[] studentsNames = new string[] { "Hadis", "Hami", "Nastaran" };

            return Ok(studentsNames);

        }

    }
}
