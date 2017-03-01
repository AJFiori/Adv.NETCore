using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class MaintenanceRecord
    {
        //MaintenanceRecordId
        [Required(ErrorMessage = "MaintenanceRecordId is required")]
        public Guid MaintenanceRecordId { get; set; }

        //MaintenanceActionId
        [Required(ErrorMessage = "MaintenanceActionId is required")]
        public Guid MaintenanceActionId { get; set; }

        //InspectorId
        [Required(ErrorMessage = "InspectorId is required")]
        public Guid InspectorId { get; set; }

        //MaintenanceProjectedStart
        [Required(ErrorMessage = "InspectionDate is required")]
        public DateTime MaintenanceProjectedStart { get; set; }

        //MaintenanceProjectedEnd
        [Required(ErrorMessage = "InspectionDate is required")]
        public DateTime MaintenanceProjectedEnd { get; set; }

        //MaintenanceActualStart
        public DateTime? MaintenanceActualStart { get; set; }

        //MaintenanceActualEnd
        public DateTime? MaintenanceActualEnd { get; set; }

        //MaintenanceProjectedCost
        [Required(ErrorMessage = "Maintenance Projected Cost is required")]
        [Range(0, 199999.99, ErrorMessage = "Maintenance Projected Cost out of range")]
        public decimal MaintenanceProjectedCost { get; set; }

        //MaintenanceActualCost
        [Range(0, 199999.99, ErrorMessage = "Maintenance Actual Cost out of range")]
        public decimal? MaintenanceActualCost { get; set; }

        //MaintenanceNotes
        [MinLength(5, ErrorMessage = "Minimum length of Maintenance Notes is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Maintenance Notes is 100 characters")]
        public string MaintenanceNotes { get; set; }

        //InspectorNotes
        [MinLength(5, ErrorMessage = "Minimum length of Inspector Notes is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Inspector Notes is 100 characters")]
        public string InspectorNotes { get; set; }
    }
}
