﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoping.Models
{
    public class Showproduct
    {
        public List<Category> cat { get; set; }

        public List<Product> pro { get; set; }
    }
}