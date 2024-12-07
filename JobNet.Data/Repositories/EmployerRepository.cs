using JobNet.Core.Entities;
using JobNet.Core.Repositories;
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
        public List<Employer> GetAll()
        {
            return _context.Employers.ToList();
        }
    }
}
