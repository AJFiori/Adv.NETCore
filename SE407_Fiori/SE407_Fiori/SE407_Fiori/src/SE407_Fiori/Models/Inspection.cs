using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class Inspection
    {
        //InspectionId
        [Required(ErrorMessage = "InspectionId is required")]
        public Guid InspectionId { get; set; }

        //BridgeId
        [Required(ErrorMessage = "BridgeId is required")]
        public Guid BridgeId { get; set; }

        //InspectorId
        [Required(ErrorMessage = "InspectorId is required")]
        public Guid InspectorId { get; set; }

        //DeckInspectoinCodeId
        [Required(ErrorMessage = "InspectorId is required")]
        public Guid DeckInspectoinCodeId { get; set; }

        //SuperstructureInspectionCodeId
        [Required(ErrorMessage = "SuperstructureInspectionCodeId is required")]
        public Guid SuperstructureInspectionCodeId { get; set; }

        //SubstructureInspectionCodeId
        [Required(ErrorMessage = "SubstructureInspectionCodeId is required")]
        public Guid SubstructureInspectionCodeId { get; set; }

        //InspectionDate
        [Required(ErrorMessage = "InspectionDate is required")]
        public DateTime InspectionDate { get; set; }

        //InspectionNotes
        [MinLength(5, ErrorMessage = "Minimum length of Inspection Notes is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Inspection Notes is 100 characters")]
        public string InspectionNotes { get; set; }
    }
}
