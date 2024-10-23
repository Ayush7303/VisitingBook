using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class UserProfile
    {
        public int UserID { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? ContactMobileNumber { get; set; }
        public string? EmailID { get; set; }
        public string? AlternateEmailID { get; set; }
        public string? Designation { get; set; }
        public DateTime? DOB { get; set; }
        public bool IsActive { get; set; } = false;
        public string? UserImage { get; set; }
        public string? UserImageName { get; set; }
        public string? UserImageTitle { get; set; }
        public int? Sequence { get; set; }
        public bool? IsForPin { get; set; }
        public string? UserNamePin { get; set; }
        public string? Pin { get; set; }
        public string? ERPRoleNameAllowed { get; set; }
        public string? StorageQuotaInMB { get; set; }
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
        public DateTime? DeletedOn { get; set; }
    }
}