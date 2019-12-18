using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using Common.Persistence.Contexts;

namespace Common.Persistence.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
