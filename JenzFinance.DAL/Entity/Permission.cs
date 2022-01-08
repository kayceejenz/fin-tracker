﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenzFinance.DAL.Entity
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } 
        public string Url { get; set; }
    }
}
