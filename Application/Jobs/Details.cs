using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Jobs
{
    public class Details
    {
        public class Query : IRequest<Job>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Job>
        {
            private readonly IJobsRepository _repository;

            public Handler(IJobsRepository jobsRepository)
            {
                _repository = jobsRepository;
            }

            public Task<Job> Handle(Query request, CancellationToken cancellationToken)
            {
                return _repository.GetJob(request.Id);
            }
        }
    }
}