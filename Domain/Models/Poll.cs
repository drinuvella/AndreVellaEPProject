using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Poll
    {
        [Key]
        public String id { get; set; }
        public String title { get; set; }
        public String Option1Text { get; set; }
        public String Option2Text { get; set; }
        public String Option3Text { get; set; }
        public int Option1VotesCount {  get; set; }
        public int Option2VotesCount { get; set; }
        public int Option3VotesCount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
