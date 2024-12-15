﻿using JobNet.Core.Entities;

namespace JobNet.Models
{
    public class JobPostModel
    {
   
        public int EmployerID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public int Salary { get; set; }

        public DateTime PostedDate { get; set; }
    }
}
