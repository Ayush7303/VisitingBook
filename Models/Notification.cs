using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public required int EmployeeID { get; set; }
        public required int VisitID { get; set; }
        public required string NotificationType { get; set; } // e.g., Log Submitted
        public required string Subject { get; set; }
        public required string NotificationBody { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}