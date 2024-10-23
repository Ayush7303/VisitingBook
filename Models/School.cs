using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitingBook.Models
{
    public class School
    {
        public int SchoolID { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public int? AddressID { get; set; }
    public int SchoolGroupID { get; set; }
    public string? SchoolImage { get; set; }
    public string? SchoolImageName { get; set; }
    public string? SchoolBannerImage { get; set; }
    public string? SchoolBannerImageName { get; set; }
    public string? SchoolLogoImage { get; set; }
    public string? SchoolLogoImageName { get; set; }
    public string? Description { get; set; }
    public string? ConfigurationJson { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; } = false;
    public int? Sequence { get; set; }
    public string? Board { get; set; }
    public DateTime? DateOfInstallationAccordingToPo { get; set; }
    public int StudentStrength { get; set; }
    public string? MobileAppShortName { get; set; }
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