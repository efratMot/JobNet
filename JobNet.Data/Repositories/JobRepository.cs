using JobNet.Core.Entities;
using JobNet.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobNet.Data.Repositories
{
    public class JobRepository:IJobRepository
    {
        private readonly DataContext _context;

        public JobRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.Where(s => !string.IsNullOrEmpty(s.Title)).Include(s => s.Employer).ToListAsync();
        }

        public Job Get(int id)
        {
            try
            {
                return _context.Jobs.Include(j => j.Employer).First(j => j.JobID == id);
            }
            catch
            {
                throw new Exception("Job not found.");
            }
        }

        public async Task<Job> AddAsync(Job job)
        {
            validateJob(job);
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job;
        }
        public async Task<Job> DeleteAsync(int id)
        {
            Job job = Get(id);
            if (job is null)
                throw new Exception("Job not found.");
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<Job> UpdateAsync(Job job,int id)
        {
            Job j = Get(id);
            if (j is null)
                throw new Exception("Job not found.");
            validateJob(job);
            j.Salary = job.Salary;
            j.Location = job.Location;
            j.Title = job.Title;
            j.Description = job.Description;
            j.PostedDate = job.PostedDate;
            await _context.SaveChangesAsync();
            return j;
        }

        public void validateJob(Job j)
        {
            if (string.IsNullOrEmpty(j.Location) || j.Location == "string")
                throw new ArgumentException("Location cannot be null or empty.");
            if (string.IsNullOrEmpty(j.Title) || j.Title == "string")
                throw new ArgumentException("Title cannot be null or empty.");
            if (string.IsNullOrEmpty(j.Description) || j.Description == "string")
                throw new ArgumentException("Description cannot be null or empty.");
            if (j.PostedDate == default(DateTime))
                throw new ArgumentException("Posted date cannot be null or empty.");
            if (j.PostedDate > DateTime.Now)
                throw new ArgumentException("Posted date cannot be in the future.");
            if (j.Salary <= 0)
                throw new ArgumentException("Salary must be a positive number.");
        }
    }
}
