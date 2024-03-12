using System.Diagnostics;
using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly IConfigurationBuilder _builder;

        public JobsRepository(IConfigurationBuilder builder)
        {
            _builder = builder;
        }

        public async Task<List<Job>> GetJobs()
        {
            try
            {
                using var context = new WorklogDbContext(_builder);
                return await context.Jobs.AsNoTracking().ToListAsync();
            }
            catch (AggregateException aggregateException)
            {
                Debug.WriteLine(aggregateException.Message);
                throw;
            }
            catch (InvalidOperationException invalidOperation)
            {
                Debug.WriteLine(invalidOperation.Message);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }

        public int GetJobsCount()
        {
            using var context = new WorklogDbContext(_builder);
            return context.Jobs.Count();
        }

        public async Task<Job> GetJob(Guid id)
        {
            try
            {
                using var context = new WorklogDbContext(_builder);
                return await context.Jobs.FindAsync(id);
            }
            catch (AggregateException aggregateException)
            {
                Debug.WriteLine(aggregateException.Message);
                throw;
            }
            catch (InvalidOperationException invalidOperation)
            {
                Debug.WriteLine(invalidOperation.Message);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }

        public async Task<int> UpdateJob(Guid id, Job job)
        {
            try
            {
                using var context = new WorklogDbContext(_builder);
                var result = await context.Jobs.FindAsync(id);

                if (result == null) return -1;

                result.Name = job.Name;
                result.Description = job.Description;
                result.Address = job.Address;
                result.Phone = job.Phone;
                result.Notes = job.Notes;

                context.Jobs.Update(result);

                return await context.SaveChangesAsync();
            }
            catch (DbUpdateException DbUpdateException)
            {
                Debug.WriteLine(DbUpdateException.Message);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }

        public async Task<int> DeleteJob(Guid id)
        {
            try
            {
                using var context = new WorklogDbContext(_builder);
                var result = await context.Jobs.FindAsync(id);

                if (result == null) return -1;

                context.Jobs.Remove(result);

                return await context.SaveChangesAsync();
            }
            catch (AggregateException aggregateException)
            {
                Debug.WriteLine(aggregateException.Message);
                throw;
            }
            catch (DbUpdateException DbUpdateException)
            {
                Debug.WriteLine(DbUpdateException.Message);
                throw;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }
    
        public async Task<int> CreateJob(Job job)
        {
            try
            {
                using var context = new WorklogDbContext(_builder);
                
                if (job == null) return -1;

                context.Jobs.Add(job);

                return await context.SaveChangesAsync();
            }
            catch (DbUpdateException DbUpdateException)
            {
                Debug.WriteLine(DbUpdateException.Message);
                throw DbUpdateException;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                throw;
            }
        }
    }
}