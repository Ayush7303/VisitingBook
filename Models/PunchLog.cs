using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class PunchLog
    {
        public int PunchID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? PunchDate { get; set; }
        public DateTime? PunchInTime { get; set; }
        public DateTime? PunchOutTime { get; set; }
        public required string Image { get; set; }
        public required string PunchLocation { get; set; }
        public bool? NotificationSent { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}