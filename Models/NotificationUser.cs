using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class NotificationUser
    {
        public int NotificationUserID { get; set; }
        public required int NotificationID { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public string? CC { get; set; }
        public string? BCC { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}