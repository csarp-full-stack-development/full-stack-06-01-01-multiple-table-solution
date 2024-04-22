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
        public string? GetCityOfTeacher(FullNameParameter fullNameParameter)
        {
            if (_repositoryManager is null || _repositoryManager.TeacherRepo is null || _repositoryManager.AddressRepo is null)
            {
                return null;
            }
            else
            {
                var query = from teacher in _repositoryManager.TeacherRepo.FindAll()
                            join address in _repositoryManager.AddressRepo.FindAll() on teacher.AddressId equals address.Id
                            where teacher.FirstName == fullNameParameter.FirstName && teacher.LastName == fullNameParameter.LastName
                            select address;

                return query.Select(address => address.City).FirstOrDefault();
            }
        }
    }
}
