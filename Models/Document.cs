using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public required int MasterID { get; set; }
        public required string ShortName { get; set; }
        public required string DocumentName { get; set; }
        public required string DocumentPath { get; set; }
        public required DateTime UploadDate { get; set; }
        public required DateTime CreatedOn { get; set; }
        public required string CreatedBy { get; set; }
        public required DateTime UpdatedOn { get; set; }
        public required string UpdatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}