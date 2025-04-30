using JobNet.Core.Entities;
using JobNet.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNet.Data.Repositories
{
    public class RequestRepository:IRequestRepository
    {
        private readonly DataContext _context;
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;

        public RequestRepository(DataContext context, IJobRepository jobRepository, IUserRepository userRepository)
        {
            _context = context;
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            return await _context.Requests.Where(s => !string.IsNullOrEmpty(s.Message)).Include(s => s.User).Include(s => s.Job).Include(s => s.Job.Employer).ToListAsync();
        }

        public Request Get(int id)
        {
            try
            {
                return _context.Requests.Include(s => s.User).Include(s => s.Job).Include(s => s.Job.Employer).First(s => s.RequestID == id);
            }
            catch
            {
                throw new Exception("Request not found.");
            }
        }

        public async Task<Request> AddAsync(Request request)
        {
            validateRequest(request);
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }
        public async Task<Request> DeleteAsync(int id)
        {
            Request request = Get(id);
            if (request is null)
                throw new Exception("Request not found.");
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Request> UpdateAsync(Request request,int id)
        {
            Request r = Get(id);
            if (r is null)
                throw new Exception("Request not found.");
            validateRequest(request);
            r.Message = request.Message;
            r.UserID = request.UserID;
            r.JobID = request.JobID;
            r.RequestDate = request.RequestDate;
            await _context.SaveChangesAsync();
            return r;
        }

        public void validateRequest(Request r)
        {
            if (_jobRepository.Get(r.JobID) is null)
                throw new Exception("Job not found.");
            if (_userRepository.Get(r.UserID) is null)
                throw new Exception("User not found.");
            if (string.IsNullOrEmpty(r.Message) || r.Message == "string")
                throw new ArgumentException("Message cannot be null or empty.");
            if (r.RequestDate == default(DateTime))
                throw new ArgumentException("Request date cannot be null or empty.");
            if (r.RequestDate > DateTime.Now)
                throw new ArgumentException("Request date cannot be in the future.");
        }
    }
}
