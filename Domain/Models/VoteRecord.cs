﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class VoteRecord
    {
        [Key]
        public Guid id { get; set; }
        public Guid PollId { get; set; }
        public string? Email {  get; set; } 
        public string IpAddress { get; set; }
    }
}
