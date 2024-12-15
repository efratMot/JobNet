using JobNet.Core.Entities;

namespace JobNet.Models
{
    public class RequestPostModel
    {
        public int JobID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
