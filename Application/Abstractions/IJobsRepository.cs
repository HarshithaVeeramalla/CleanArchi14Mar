using Domain.Entities;

namespace Application.Abstractions
{
    public interface IJobsRepository
    {
        Task<List<Job>> GetJobs();
        int GetJobsCount();
        Task<Job> GetJob(Guid id);
        Task<int> UpdateJob(Guid id, Job job);
        Task<int> DeleteJob(Guid id);
        Task<int> CreateJob(Job job);
    }
}