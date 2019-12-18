using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using Common.Persistence.Contexts;

namespace Common.Persistence.Repositories
{
    public class TeacherSubjectRepository : GenericRepository<TeacherSubject>, ITeacherSubjectRepository
    {
        public TeacherSubjectRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
