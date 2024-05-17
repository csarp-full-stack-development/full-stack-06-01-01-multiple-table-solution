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
                IQueryable<NameBirthDay>? query = _examService.GetParantsOfStudent(fullNameParameter);
                if (query is not null)
                {
                    List<NameBirthDay> result = await query.ToListAsync();
                    return Ok(result);
                }
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

        [HttpPost("SchoolClassStudentName")]
        public async Task<IActionResult> GetSchoolClassStudentName([FromBody] SchoolClassYearTypeParameter schoolClassParameter)
        {
            if (_examService is null)
                return BadRequest();
            else
            {
                IQueryable<NameBirthDay>? query = _examService.GetSchoolClassStudentNameBirthDay(schoolClassParameter.SchoolYear, schoolClassParameter.SchoolClassType);
                if (query is not null)
                {
                    List<NameBirthDay> result = await query.ToListAsync();
                    return Ok(result);
                }
                else
                    return BadRequest();
            }
        }

        [HttpPost("SchoolClassTeacherNameBirthDay")]
        public async Task<IActionResult> GetSchoolClassTeacherNameBirthDay([FromBody] SchoolClassYearTypeParameter schoolClassParameter)
        {
            if (_examService is null)
                return BadRequest();
            else
            {
                IQueryable<NameBirthDay>? query = _examService.GetSchoolClassTeacherNameBirthDay(schoolClassParameter.SchoolYear, schoolClassParameter.SchoolClassType);
                if (query is not null)
                {
                    List<NameBirthDay> result = await query.ToListAsync();
                    return Ok(result);
                }
                else
                    return BadRequest();
            }
        }
    }
}
