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
    public class SubscriptionRepository:ISubscriptionRepository
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public SubscriptionRepository(DataContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions.Include(s => s.User).ToListAsync();
        }

        public Subscription Get(int id)
        {
            try
            {
                return _context.Subscriptions.Include(s => s.User).First(s => s.SubscriberID == id);
            }
            catch
            {
                throw new Exception("Subscription not found.");
            }
        }

        public async Task<Subscription> AddAsync(Subscription subscription)
        {
            validateSubscription(subscription);
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }
        public async Task<Subscription> DeleteAsync(int id)
        {
            Subscription subscription = Get(id);
            if (subscription is null)
                throw new Exception("Subscription not found.");
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<Subscription> UpdateAsync(Subscription subscription,int id)
        {
            Subscription s = Get(id);
            if (s is null)
                throw new Exception("Subscription not found.");
            validateSubscription(subscription);
            s.SubscriptionDate = subscription.SubscriptionDate;
            s.UserId = subscription.UserId;
            await _context.SaveChangesAsync();
            return s;
        }

        public void validateSubscription(Subscription s)
        {
            if (_userRepository.Get(s.UserId) is null)
                throw new Exception("User not found.");
            if (s.SubscriptionDate == default(DateTime))
                throw new ArgumentException("Subscription date cannot be null or empty.");
            if (s.SubscriptionDate > DateTime.Now)
                throw new ArgumentException("Subscription date cannot be in the future.");
        }
    }
}
