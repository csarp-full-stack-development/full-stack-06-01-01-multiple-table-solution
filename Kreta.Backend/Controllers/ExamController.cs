using Kreta.Backend.Services;
using Kreta.Shared.Models;
using Kreta.Shared.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kreta.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamService? _examService;

        public ExamController(IExamService? examService )
        {
            _examService = examService;
        }

        [HttpPost("GetParantsOfStudent")]
        public async Task<IActionResult> GetParantsOfStudent([FromBody] FullNameParameter fullNameParameter)
        {
            if (_examService is null)
                return BadRequest();
            else
            {
                List<NameBirthDay>? result = await _examService.GetParantsOfStudent(fullNameParameter).ToListAsync();
                if (result is not null)
                    return Ok(result);
                else
                    return BadRequest();
            }
        }

        [HttpPost("GetAddressOfTeacher")]
        public IActionResult GetAddressOfTeacher([FromBody] FullNameParameter fullNameParameter)
        {
            if (_examService is null)
                return BadRequest();
            else
            {
                string? result = _examService.GetCityOfTeacher(fullNameParameter);
                if (result is not null)
                    return Ok(result);
                else
                    return BadRequest();
            }
        }
    }
}
