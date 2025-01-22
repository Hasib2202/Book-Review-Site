using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class VoteDTO
    {
        public int Id { get; set; }

        [Required]
        public bool IsUpvote { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ReviewId { get; set; }
    }
}
