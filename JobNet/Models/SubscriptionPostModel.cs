using JobNet.Core.Entities;

namespace JobNet.Models
{
    public class SubscriptionPostModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
