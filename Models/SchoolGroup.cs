using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class SchoolGroup
    {
        public int SchoolGroupID { get; set; }
        public string SchoolGroupName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string? ContactPerson { get; set; }
        public string? SchoolGroupImage { get; set; }
        public string? SchoolGroupImageName { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? ProductWebsite { get; set; }
        public bool IsActive { get; set; } = false;
        public int? Sequence { get; set; }
        public string? MobileAppShortName { get; set; }
        public string? PatchUpdateServiceURL { get; set; }
        public string? ServerIP { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Database1 { get; set; }
        public string? Database2 { get; set; }
        public string? Database3 { get; set; }
        public string? Database4 { get; set; }
        public string? Database5 { get; set; }
        public bool? IsClientSendAlert { get; set; } = false;
        public string? UploadMessage { get; set; }
        public string? CustomFields { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? RecordState { get; set; }
        public int? ExtColInt1 { get; set; }
        public int? ExtColInt2 { get; set; }
        public DateTime? ExtColDate1 { get; set; }
        public DateTime? ExtColDate2 { get; set; }
        public string? ExtColText1S { get; set; }
        public string? ExtColText2S { get; set; }
        public string? ExtColText1M { get; set; }
        public string? ExtColText2M { get; set; }
        public string? ExtColText1L { get; set; }
        public string? ExtColText2L { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}