﻿using JobNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNet.Core.Services
{
    public interface ISubscriptionService
    {
        public List<Subscription> GetList();
    }
}
