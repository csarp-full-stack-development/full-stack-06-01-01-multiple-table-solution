using Kreta.Backend.Repos.Managers;
using Kreta.Shared.Models;
using Kreta.Shared.Models.SchoolCitizens;
using Kreta.Shared.Parameters;

namespace Kreta.Backend.Services
{
    public class ExamService : IExamService
    {
        private readonly IRepositoryManager? _repositoryManager;

        public ExamService(IRepositoryManager? repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        // 1. feladat
        // Egy névvel adott diáknak(pl.Jegy János, Vas Valér) kik a szülei? Adja meg a szülők nevét és születési idejét!
        public IQueryable<NameBirthDay>? GetParantsOfStudent(FullNameParameter fullNameParameter)
        {
            if (_repositoryManager is null || _repositoryManager.StudentRepo is null || _repositoryManager.ParentRepo is null)
            {
                return null;
            }
            else
            { 
                Student? student = _repositoryManager.StudentRepo
                    .FindAll()
                    .Where(student => student.FirstName == fullNameParameter.FirstName &&
                                      student.LastName == fullNameParameter.LastName)
                    .FirstOrDefault();
                if (student == null)
                {
                    return null;
                }
                else
                {
                    Guid? matherId = student.MotherId;
                    Guid? fatherId = student.FatherId;
                    IQueryable<NameBirthDay> result = _repositoryManager.ParentRepo
                            .FindAll()
                            .Where(parent => parent.Id == matherId || parent.Id == fatherId)
                            .Select(parent => new NameBirthDay { FirstName = parent.FirstName, LastName = parent.FirstName, Birthday = parent.BirthDay });
                    return result;
                }
            }

        }

        // 2. feladat
        // Egy névvel adott tanár (pl. Földrajz Feri) hol lakik? Adja meg a város nevét!
        public string? GetCityOfTeacher(FullNameParameter fullNameParameter)
        {
            if (_repositoryManager is null || _repositoryManager.TeacherRepo is null || _repositoryManager.AddressRepo is null)
            {
                return null;
            }
            else
            {
                var query = from teacher in _repositoryManager.TeacherRepo.FindAll()
                            join address in _repositoryManager.AddressRepo.FindAll() 
                            on teacher.AddressId equals address.Id
                            where teacher.FirstName == fullNameParameter.FirstName && teacher.LastName == fullNameParameter.LastName
                            select address;

                return query.Select(address => address.City).FirstOrDefault();
            }
        }

        // 3. feladat
        // Adja meg egy osztály (pl. 9.a vagy 10.b) diákjainak nevét, születési idejét!
        public IQueryable<NameBirthDay>? GetSchoolClassStudentNameBirthDay(int schoolYear, SchoolClassType schoolClassType)
        {
            if (_repositoryManager is null || _repositoryManager.SchoolClassRepo is null || _repositoryManager.StudentRepo is null)
            {
                return null;
            }
            else
            {
                var quary = from schoolClass in _repositoryManager.SchoolClassRepo.FindAll()
                            join student in _repositoryManager.StudentRepo.FindAll()
                            on schoolClass.Id equals student.SchoolClassID
                            where schoolClass.SchoolYear == schoolYear && schoolClass.SchoolClassType == schoolClassType
                            select new NameBirthDay { FirstName = student.FirstName, LastName = student.LastName, Birthday = student.BirthDay };
                return quary;
            }
        }
        // 4. feladat
        // Adja meg, hogy egy osztályban  (pl. 9.a vagy 10.b) kik tanítanak!
        public IQueryable<NameBirthDay>? GetSchoolClassTeacherNameBirthDay(int schoolYear, SchoolClassType schoolClassType)
        {
            if (_repositoryManager is null 
                || _repositoryManager.TeacherRepo is null 
                || _repositoryManager.SchoolClassRepo is null
                || _repositoryManager.TeacherTeachInSchoolClassRepo is null
                )
            {
                return null;
            }
            else
            {
                var query = from tts in _repositoryManager.TeacherTeachInSchoolClassRepo.FindAll()
                            join teacher in _repositoryManager.TeacherRepo.FindAll() on tts.TeacherId equals teacher.Id
                            join schoolClass in _repositoryManager.SchoolClassRepo.FindAll() on tts.SchoolClassId equals schoolClass.Id
                            where schoolClass.SchoolYear == schoolYear && schoolClass.SchoolClassType == schoolClassType
                            select new NameBirthDay { FirstName = teacher.FirstName, LastName = teacher.LastName, Birthday = teacher.BirthDay };
                return query;
            }
        }

    }
}
