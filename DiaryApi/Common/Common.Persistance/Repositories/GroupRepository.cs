﻿using Common.Domain.Interfaces.Persistence;
using Common.Domain.Models;
using Common.Persistence.Contexts;

namespace Common.Persistence.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
