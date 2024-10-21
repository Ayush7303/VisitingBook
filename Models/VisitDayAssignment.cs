using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class VisitDayAssignment
    {
        public int VisitDayAssignmentID { get; set; }
        public required int VisitID { get; set; }
        public int? AgendaID { get; set; }
        public string? Description { get; set; } // Description of Agenda
        public int? EmployeeID { get; set; }
        public required DateTime Date { get; set; }
        public required string LogRemark { get; set; }
        public DateTime? LogDate { get; set; }
        public required int ApprovalID { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}