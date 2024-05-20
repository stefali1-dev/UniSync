using Ergo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;

namespace UniSync.Infrastructure.Repositories
{
    public class ProfessorRepository : BaseRepository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(UniSyncContext context) : base(context)
        {
        }
    }
}
