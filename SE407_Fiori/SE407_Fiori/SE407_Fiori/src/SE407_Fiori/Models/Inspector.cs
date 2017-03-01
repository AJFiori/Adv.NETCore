using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class Inspector
    {
        //InspectorId
        [Required(ErrorMessage = "InspectionCodeId is required")]
        public Guid InspectorId { get; set; }

        //InspectorFirst
        [Required(ErrorMessage = "Inspector First is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspector First is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Inspector First is 100 characters")]
        public string InspectorFirst { get; set; }

        //InspectorLast
        [Required(ErrorMessage = "Inspector Last is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspector Last is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Inspector Last is 100 characters")]
        public string InspectorLast { get; set; }

        //InspectorOrg
        [Required(ErrorMessage = "Inspector Org is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Inspector Org is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Inspector Org is 100 characters")]
        public string InspectorOrg { get; set; }

        //InspectorCertEffective
        [Required(ErrorMessage = "InspectionDate is required")]
        public DateTime InspectorCertEffective { get; set; }

        //InspectorCertExpires
        [Required(ErrorMessage = "InspectionDate is required")]
        public DateTime InspectorCertExpires { get; set; }
    }
}
