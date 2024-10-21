using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class Approval
    {
        public int ApprovalID { get; set; }
        public required int VisitID { get; set; }
        public required int ApprovedBy { get; set; }
        public required string ApprovedStatus { get; set; }
        public required DateTime ApprovedDate { get; set; }
        public required string Remarks { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}