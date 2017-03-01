using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class InspectionCode
    {
        //InspectionCodeId
        [Required(ErrorMessage = "InspectionCodeId is required")]
        public Guid InspectionCodeId { get; set; }

        //InspectionCodeName
        [Required(ErrorMessage = "Inspection Notes is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspection Notes is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Inspection Notes is 100 characters")]
        public string InspectionCodeName { get; set; }

        //InspectoinCodeDesc
        [Required(ErrorMessage = "Inspectoin Code Desc is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspectoin Code Desc is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Inspectoin Code Desc is 100 characters")]
        public string InspectoinCodeDesc { get; set; }
    }
}
