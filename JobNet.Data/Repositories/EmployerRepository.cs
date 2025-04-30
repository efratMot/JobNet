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
    public class EmployerRepository:IEmployerRepository
    {
        private readonly DataContext _context;

        public EmployerRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Employer>> GetAllAsync()
        {
            return await _context.Employers.Where(e => !string.IsNullOrEmpty(e.CompanyName)).Include(s=>s.User).ToListAsync();
        }

        public Employer Get(int id)
        {
            try
            {
                return _context.Employers.Include(s => s.User).First(s => s.EmployerID == id);
            }
            catch
            {
                throw new Exception("Employer not found");
            }   
        }

        public async Task<Employer> AddAsync(Employer employer)
        {
            validateEmployer(employer);
            _context.Employers.Add(employer);
            await _context.SaveChangesAsync();
            return employer;
        }
        public async Task<Employer> DeleteAsync(int id) {

            Employer employer = Get(id);
            if (employer is null)
                throw new Exception("Employer not found.");
            _context.Employers.Remove(employer);
            await _context.SaveChangesAsync();
            return employer;
        }
        public async Task<Employer> UpdateAsync(Employer employer, int id)
        {
            Employer e = Get(id);
            if (e is null)
                throw new Exception("Employer not found.");
            validateEmployer(employer);
            e.CompanyName = employer.CompanyName;
            e.Industry = employer.Industry;
            await _context.SaveChangesAsync();
            return e;
        }

        public void validateEmployer(Employer e)
        {
            if (string.IsNullOrEmpty(e.CompanyName) || e.CompanyName == "string")
                throw new ArgumentException("CompanyName cannot be null or empty.");
            if (string.IsNullOrEmpty(e.Industry) || e.Industry == "string")
                throw new ArgumentException("Industry cannot be null or empty.");
        }
    }
}
