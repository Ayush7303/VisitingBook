using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class VisitMaster
    {
        public int VisitID { get; set; }
        public required int SchoolID { get; set; }
        public required string Location { get; set; }
        [Required(ErrorMessage = "Start Date is Required.")]
        public required DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        public required DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public required string Description { get; set; }
        public required string Status { get; set; }
        public string? VerifiedBy { get; set; }
        public string? VisitReportFile { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}