using System.ComponentModel.DataAnnotations;

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
