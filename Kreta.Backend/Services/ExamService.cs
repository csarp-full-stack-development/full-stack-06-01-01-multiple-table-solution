using Kreta.Backend.Repos.Managers;
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
        public List<ParentNameBirthDay>? GetParantsOfStudent(FullNameParameter fullNameParameter)
        {
            Student? student= _repositoryManager.StudentRepo
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
                Parent? mather=_repositoryManager.ParentRepo
                    .FindAll()
                    .FirstOrDefault(parent => parent.Id==matherId);
                Parent? father = _repositoryManager.ParentRepo
                    .FindAll()
                    .FirstOrDefault(parent => parent.Id == fatherId);
                List<ParentNameBirthDay> result = new List<ParentNameBirthDay>
                {
                    new ParentNameBirthDay
                    {
                        FirstName = mather.FirstName,
                        LastName = mather.LastName,
                        Birthday = mather.BirthDay
                    },
                    new ParentNameBirthDay
                    {
                        FirstName = father.FirstName,
                        LastName = father.LastName,
                        Birthday = father.BirthDay
                    }
                };
                return result;
            }
                
        }
    }
}
