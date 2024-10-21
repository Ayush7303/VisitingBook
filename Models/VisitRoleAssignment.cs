using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class VisitRoleAssignment
    {
        public int AssignmentID { get; set; }
        public required int VisitID { get; set; }
        public required string AssignedBy { get; set; }
        public required string Role { get; set; } // e.g., Moderator, Creator, Editor
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}