using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class MaintenanceAction
    {
        //MaintenanceActionId
        [Required(ErrorMessage = "MaintenanceActionId is required")]
        public Guid MaintenanceActionId { get; set; }

        //MaintenanceActionName
        [Required(ErrorMessage = "MaintenanceActionName is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Maintenance Action Name is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Maintenance Action Name is 100 characters")]
        public string MaintenanceActionName { get; set; }
    }
}
