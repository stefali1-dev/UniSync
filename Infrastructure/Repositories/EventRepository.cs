using UniSync.Application.Persistence;
using UniSync.Domain.Common;
using UniSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UniSync.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(UniSyncContext context) : base(context)
        {
        }

        public Task<bool> IsEventNameAndDateUnique(string eventName, DateTime eventDate)
        {
            var matches = context.Events.Any(e => e.EventName.Equals(eventName) 
            && e.EventDate.Date.Equals(eventDate.Date));
            return Task.FromResult(matches);
        }

        public override async Task<Result<Event>> FindByIdAsync(Guid id)
        {
            var result = await context.Events.Include(e => e.Category).FirstOrDefaultAsync(e => e.EventId.Equals(id))!;
            if (result == null)
            {
                return Result<Event>.Failure($"Entity with id {id} not found");
            }
            return Result<Event>.Success(result);
        }

        public override async Task<Result<IReadOnlyList<Event>>> GetAllAsync()
        {
            var result = await context.Events.Include(e => e.Category).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<Event>>.Success(result);
        }

    }
}
