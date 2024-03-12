using System.Net;
using API.Controllers;
using Application.Jobs;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Controller
{
    public class JobControllerTests
    {
        private Mock<IMediator> _mediatr;
        private Mock<IConfigurationBuilder> _builder;
        private WorklogDbContext _worklogDbContext;

        private JobController _jobController;
        private DbSet<Job> _expectedJobsRepository;
        private Guid _id;
        private Job _job;

        public JobControllerTests()
        {
            var builder = new ConfigurationBuilder();

            _worklogDbContext = new WorklogDbContext(builder); 

            _expectedJobsRepository = _worklogDbContext.Jobs;

            _mediatr = new Mock<IMediator>();

            _jobController = new JobController(_mediatr.Object);

            _id = Guid.Parse(CONSTANTS.IdForTestingSuccess);

            _job = new Job(_id) {
                Name = "Repair the laundry floor sink",
                Description = "The laundry floor sink is broken.",
                Address = "27 Churchill Crescent, Crows Nest",
                Notes = "The door pin is 1234",
                Phone = "0412397209"
            };
        }

        [Fact]
        public async void GetJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK200()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Details.Query>(),
                It.IsAny<CancellationToken>()))
                    .ReturnsAsync(_expectedJobsRepository
                    .FindAsync(_id).Result);

            var result = await _jobController.GetJob(_id);

            int expected = (int)HttpStatusCode.OK;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void UpdateJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK204()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Update.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _jobController.UpdateJob(_id, _job);

            int expected = (int)HttpStatusCode.NoContent;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void UpdateJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK400()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Update.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var result = await _jobController.UpdateJob(_id, _job);

            int expected = (int)HttpStatusCode.BadRequest;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void UpdateJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK404()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Update.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(-1);

            var result = await _jobController.UpdateJob(_id, _job);

            int expected = (int)HttpStatusCode.NotFound;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }

        
        [Fact]
        public async void DeleteJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK204()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Update.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _jobController.DeleteJob(_id);

            int expected = (int)HttpStatusCode.NoContent;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public async void DeleteJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK400()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Update.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var result = await _jobController.DeleteJob(_id);

            int expected = (int)HttpStatusCode.BadRequest;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void DeleteJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK404()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Update.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(-1);

            var result = await _jobController.DeleteJob(_id);

            int expected = (int)HttpStatusCode.NotFound;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }
    
        [Fact]
        public async void CreateJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK204()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Create.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _jobController.CreateJob( _job);

            int expected = (int)HttpStatusCode.NoContent;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void CreateJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK400()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Create.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var result = await _jobController.CreateJob(_job);

            int expected = (int)HttpStatusCode.BadRequest;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void CreateJob_ReturnsHttpRestStatusCode_ShouldBeEqualToOK404()
        {
            _mediatr.Setup(m => m.Send(It.IsAny<Create.Command>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(-1);

            var result = await _jobController.CreateJob(_job);

            int expected = (int)HttpStatusCode.NotFound;
            int? actual = ((IStatusCodeActionResult)result).StatusCode;

            Assert.Equal(expected, actual);
        }
        
    }
}