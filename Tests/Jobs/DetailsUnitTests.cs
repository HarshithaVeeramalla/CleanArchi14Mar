using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Jobs;
using Domain.Common;
using Moq;

namespace Tests.Jobs
{
    public class DetailsUnitTests
    {
        private Details.Query _query;
        private CancellationToken _token;
        private Mock<IJobsRepository> _repository;
        private Details.Handler _handler;
        private Guid _guid;

        public DetailsUnitTests()
        {
            _query = new Details.Query();
            _token = new CancellationToken();
            _repository = new Mock<IJobsRepository>();
            _handler = new Details.Handler(_repository.Object);
            _guid = Guid.Parse(CONSTANTS.IdForTestingSuccess);
        }

        [Fact]
        public async void Handle_ShouldVerifyGetJob_GetsCalledAtleastOnce()
        {
            _query.Id = _guid;

            await _handler.Handle(_query, _token);

            _repository.Verify(r => r.GetJob(_guid), Times.Once());
        }
    }
}