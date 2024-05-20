using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Domain.Entities;

namespace UniSync.Application.Persistence
{
    public interface IProfessorRepository : IAsyncRepository<Professor>
    {
    }
}
