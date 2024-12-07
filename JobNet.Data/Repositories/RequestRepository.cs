using JobNet.Core.Entities;
using JobNet.Core.Repositories;
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

        public RequestRepository(DataContext context)
        {
            _context = context;
        }
        public List<Request> GetAll()
        {
            return _context.Requests.ToList();
        }
    }
}
