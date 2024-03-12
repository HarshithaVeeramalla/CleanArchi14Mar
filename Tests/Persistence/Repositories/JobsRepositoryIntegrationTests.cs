using Application.Abstractions;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests.Persistence.Repositories
{
    public class JobsRepositoryIntegrationTests
    {
        private IJobsRepository _actualJobsRepository;
        private WorklogDbContext _worklogDbContext;
        private DbSet<Job> _expectedJobsRepository;
        private Guid _guid;
        private Job _job1;
        private Job _job2;

        public JobsRepositoryIntegrationTests()
        {
            var builder = new ConfigurationBuilder();
            
            _worklogDbContext = new WorklogDbContext(builder);

            _guid = Guid.Parse(CONSTANTS.IdForTestingSuccess);

            _actualJobsRepository = new JobsRepository(builder);

            _expectedJobsRepository = _worklogDbContext.Jobs;
            
            _job1 = new Job(_guid)
            {
                Name = "Repair the laundry floor sink",
                Description = "The laundry floor drain is broken in my apartment.",
                Address = "27 Churchill Crescent, Enfield",
                Notes = "The door pin is 1234",
                Phone = "0412397209"
            };

            _job2 = new Job(Guid.NewGuid()) {
                Name = "Repair the sliding door",
                Description = "The sliding door in my apartment is broken and is jammed.",
                Address = "27 Drummond Lane, Burwood",
                Notes = "The door pin is x24s",
                Phone = "0412327111"
            };
        }

        [Fact]
        public void GetJob_ReturnsSingleJob_ShouldBeEqual()
        {
            var expectedJobsp1 = _expectedJobsRepository.FindAsync(_guid).Result;
            var expectedJobsp2 = JobsRepositoryIntegrationTests.GetJobUsingAdoNet(_guid);
            var actualJobs = _actualJobsRepository.GetJob(_guid).Result;

            Assert.Equal(expectedJobsp1, actualJobs);
            Assert.Equal(expectedJobsp2, actualJobs);
        }

        [Fact]
        public void UpdateJob_ValidId_ShouldBeEqual()
        {
            var expected = 1;

            var actual = _actualJobsRepository.UpdateJob(_guid, _job1).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdateJob_InvalidId_ShouldBeEqual()
        {
            _guid = Guid.Parse(CONSTANTS.IdForTestingFail);

            var expected = -1;

            var actual = _actualJobsRepository.UpdateJob(_guid, _job1).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetJobs_ReturnsTotalJobCounts_ShouldBeEqual()
        {
            var expectedJobs = _expectedJobsRepository;
            var actualJobs = _actualJobsRepository.GetJobs().Result;

            Assert.NotNull(actualJobs);
            Assert.Equal(expectedJobs.Count(), actualJobs.Count);
        }

        [Fact]
        public void DeleteJob_InvalidId_ShouldBeEqual()
        {
            _guid = Guid.Parse(CONSTANTS.IdForTestingFail);

            var expected = -1;

            var actual = _actualJobsRepository.DeleteJob(_guid).Result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateJob_JobNotNull_ShouldBeEqual()
        {
            var expected = 1;

            var actual = _actualJobsRepository.CreateJob(_job2).Result;

            Assert.Equal(expected, actual);
        }

        private static Job GetJobUsingAdoNet(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-BQU5IHO;Initial Catalog=Worklog;Integrated Security=True;TrustServerCertificate=True;"))
            {
                using (var command = new SqlCommand($"SELECT * FROM Jobs ", connection))
                {
                    Job _fakeJob;
                    connection.Open();
                    command.CommandText += $"WHERE job_id = '{id}'";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _fakeJob = new Job(Guid.Parse(reader[0].ToString()))
                        {
                            Name = $"{reader[1]}",
                            Description = $"{reader[2]}",
                            Address = $"{reader[3]}",
                            Notes = $"{reader[4]}",
                            Phone = $"{reader[5]}",
                        };
                        return _fakeJob;
                    }
                }
            }
            return null;
        }
    }
}