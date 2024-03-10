using UniSync.Application.Persistence;
using MediatR;

namespace UniSync.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, DeleteEventCommandResponse>
    {
        private readonly IEventRepository repository;

        public DeleteEventCommandHandler(IEventRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteEventCommandResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.EventId);
            if (!result.IsSuccess) 
            {
                return new DeleteEventCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteEventCommandResponse
            {
                Success = true
            };
        }
    }
}
