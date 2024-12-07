using JobNet.Core.Entities;
using JobNet.Core.Repositories;
using JobNet.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNet.Service
{
    public class JobService:IJobService
    {
        private readonly IJobRepository _JobRepository;
        public JobService(IJobRepository jobRepository)
        {
            _JobRepository = jobRepository;
        }

        public List<Job> GetList()
        {
            //לוגיקה עיסקית
            //קבלת נתונים מה db
            //לוגיקה עסקית
            return _JobRepository.GetAll();

        }
    }
}
