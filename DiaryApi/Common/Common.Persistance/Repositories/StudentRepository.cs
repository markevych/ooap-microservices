using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using Common.Persistence.Contexts;

namespace Common.Persistence.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
