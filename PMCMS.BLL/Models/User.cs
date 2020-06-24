﻿using System.Collections.Generic;

namespace PMCMS.BLL.Models
{
    public class User
    {
        public User()
        {
        }

        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}