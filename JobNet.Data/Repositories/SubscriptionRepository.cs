using JobNet.Core.Entities;
using JobNet.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNet.Data.Repositories
{
    public class SubscriptionRepository:ISubscriptionRepository
    {
        private readonly DataContext _context;

        public SubscriptionRepository(DataContext context)
        {
            _context = context;
        }
        public List<Subscription> GetAll()
        {
            return _context.Subscriptions.ToList();
        }
    }
}
