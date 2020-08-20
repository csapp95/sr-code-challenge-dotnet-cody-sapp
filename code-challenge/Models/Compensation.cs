﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        [JsonIgnore]
        public String Id { get; set; }
        public Employee Employee { get; set;}
        public int Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
