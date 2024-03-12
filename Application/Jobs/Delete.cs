using Application.Abstractions;
using MediatR;

namespace Application.Jobs
{
    public class Delete
    {
        public class Command : IRequest<int>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IJobsRepository _jobsRepository;

            public Handler(IJobsRepository jobsRepository)
            {
                _jobsRepository = jobsRepository;
            }

            public Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return _jobsRepository.DeleteJob(request.Id);
            }
        }
    }
}