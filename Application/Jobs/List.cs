using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Jobs
{
    public class List
    {
        public class Query : IRequest<List<Job>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Job>>
        {
            private readonly IJobsRepository _repository;

            public Handler(IJobsRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<Job>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _repository.GetJobs();
            }
        }
    }
}