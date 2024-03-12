using Application.Jobs;
using Application.Abstractions;
using Moq;

namespace Tests.Jobs;

public class ListTests
{
    private List.Query _query;
    private CancellationToken _token;
    private Mock<IJobsRepository> _repository;
    private List.Handler _handler;

    public ListTests()
    {
        _query = new List.Query();
        _token = new CancellationToken();
        _repository = new Mock<IJobsRepository>();
        _handler = new List.Handler(_repository.Object);
    }

    [Fact]
    public async void Handle_ShouldVerifyGetJobs_True()
    {
        await _handler.Handle(_query, _token);

        _repository.Verify(r => r.GetJobs(), Times.Once());
    }

    
}