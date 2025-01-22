using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Review content is required")]
        [StringLength(500)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "BookId is required")]
        public int BookId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public int Id { get; set; }
        //public string Content { get; set; }
        //public int Rating { get; set; }
        //public string BookTitle { get; set; }
        //public string BookGenre { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
