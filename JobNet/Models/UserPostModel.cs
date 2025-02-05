using JobNet.Core.Entities;

namespace JobNet.Models
{
    public class UserPostModel
    {

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public eRole Role { get; set; }
    }
}
