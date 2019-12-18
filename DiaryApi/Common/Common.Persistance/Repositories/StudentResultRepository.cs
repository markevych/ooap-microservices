using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using Common.Persistence.Contexts;

namespace Common.Persistence.Repositories
{
    public class StudentResultRepository : GenericRepository<StudentResult>, IStudentResultRepository
    {
        public StudentResultRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
