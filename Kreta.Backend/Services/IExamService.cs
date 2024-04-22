using Kreta.Shared.Models;
using Kreta.Shared.Parameters;

namespace Kreta.Backend.Services
{
    public interface IExamService
    {
        public IQueryable<NameBirthDay>? GetParantsOfStudent(FullNameParameter fullNameParameter);
        public string? GetCityOfTeacher(FullNameParameter fullNameParameter);
    }
}
