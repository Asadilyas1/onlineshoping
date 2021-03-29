﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace shoping.Models
{
    public class FilterModel
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Category { get; set; }
        public int? Product { get; set; }

    }
}