using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class AgendaMaster
    {
        public int AgendaMasterID { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}