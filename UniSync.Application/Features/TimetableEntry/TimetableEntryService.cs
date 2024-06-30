using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;
using UniSync.Domain.Entities.Administration;

namespace UniSync.Application.Features.TimetableEntry
{
    public class TimetableEntryService : ITimetableEntryService
    {
        private readonly ITimetableEntryRepository timetableEntryRepository;

        public TimetableEntryService(ITimetableEntryRepository timetableEntryRepository)
        {
            this.timetableEntryRepository = timetableEntryRepository;
        }

        public async Task AddTimetableEntry(TimetableEntryDto timetableEntryDto)
        {
            var timetableEntry = new UniSync.Domain.Entities.Administration.TimetableEntry
            {
                TimetableEntryId = Guid.NewGuid(),
                TimeInterval = timetableEntryDto.TimeInterval,
                CourseId = Guid.Parse(timetableEntryDto.CourseId),
                CourseName = timetableEntryDto.CourseName,
                CourseType = timetableEntryDto.CourseType,
                ProfessorId = Guid.Parse(timetableEntryDto.ProfessorId),
                ProfessorName = timetableEntryDto.ProfessorName,
                Classroom = timetableEntryDto.Classroom,
                DayOfWeek = timetableEntryDto.DayOfWeek,
                StudentGroup = timetableEntryDto.StudentGroup
            };

            await timetableEntryRepository.AddAsync(timetableEntry);
        }

        public async Task<List<UniSync.Domain.Entities.Administration.TimetableEntry>> GetTimetableEntriesByProfessorId(string professorId)
        {
            return await timetableEntryRepository.GetByProfessorIdAsync(Guid.Parse(professorId));
        }

        public async Task<List<UniSync.Domain.Entities.Administration.TimetableEntry>> GetTimetableEntriesByStudentGroupName(string groupName)
        {
            return await timetableEntryRepository.GetByGroupNameAsync(groupName);

        }
    }
}
