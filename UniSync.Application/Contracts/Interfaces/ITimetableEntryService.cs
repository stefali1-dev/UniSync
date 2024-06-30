using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Features.TimetableEntry;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface ITimetableEntryService
    {
        public Task AddTimetableEntry(TimetableEntryDto timetableEntryDto);
        public Task<List<TimetableEntry>> GetTimetableEntriesByProfessorId(string professorId);
        public Task<List<TimetableEntry>> GetTimetableEntriesByStudentGroupName(string studentGroupName);
    }
}
