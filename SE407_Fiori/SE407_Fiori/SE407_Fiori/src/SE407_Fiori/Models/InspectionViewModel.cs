using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace SE407_Fiori
{
    public class InspectionViewModel
    {
        public List<Inspection> InspectionList { get; set; }
        public Inspection NewInspection { get; set; }
        public List<SelectListItem> Bridges { get; set; }
        public List<SelectListItem> Inspectors { get; set; }
        public List<SelectListItem> InspectionCodes { get; set; }

        /* BridgeId - Bridges
           InspectorId - Inspectors
           DeckInspectoinCodeId - InspectionCodes
           SuperstructureInspectionCodeId - InspectionCodes
           SubstructureInspectionCodeId - InspectionCodes
       */
    }
}
