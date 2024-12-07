using JobNet.Core.Entities;
using JobNet.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNet.Data.Repositories
{
    public class JobRepository:IJobRepository
    {
        private readonly DataContext _context;

        public JobRepository(DataContext context)
        {
            _context = context;
        }
        public List<Job> GetAll()
        {
            return _context.Jobs.ToList();
        }
    }
}
