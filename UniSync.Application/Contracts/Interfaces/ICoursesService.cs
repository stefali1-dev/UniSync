using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface ICoursesService
    {
        public Task LoadCoursesFromCsv(string csvPath);
    }
}
