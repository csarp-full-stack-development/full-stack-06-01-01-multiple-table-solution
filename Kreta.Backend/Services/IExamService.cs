using Kreta.Shared.Models;
using Kreta.Shared.Parameters;

namespace Kreta.Backend.Services
{
    public interface IExamService
    {
        public IQueryable<NameBirthDay>? GetParantsOfStudent(FullNameParameter fullNameParameter);
        public string? GetCityOfTeacher(FullNameParameter fullNameParameter);
        public IQueryable<NameBirthDay>? GetSchoolClassStudentNameBirthDay(int schoolYear, SchoolClassType schoolClassType);
        public IQueryable<NameBirthDay>? GetSchoolClassTeacherNameBirthDay(int schoolYear, SchoolClassType schoolClassType);
    }
}
