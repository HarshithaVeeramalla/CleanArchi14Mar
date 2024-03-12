using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Jobs
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            public Guid Id { get; set; }
            public Job Job { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IJobsRepository _repository;

            public Handler(IJobsRepository jobsRepository)
            {
                _repository = jobsRepository;
            }

            public Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return _repository.CreateJob(request.Job);
            }
        }
    }
}