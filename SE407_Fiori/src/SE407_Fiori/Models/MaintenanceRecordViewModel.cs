using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SE407_Fiori
{
    public class MaintenanceRecordViewModel
    {
        public List<MaintenanceRecord> MaintenanceRecordList { get; set; }
        public MaintenanceRecord NewMaintenanceRecord { get; set; }
        public List<SelectListItem> MaintenanceActions { get; set; }
        public List<SelectListItem> Inspectors { get; set; }
    }
}
