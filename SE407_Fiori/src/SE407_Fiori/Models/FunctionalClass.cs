using System;
using System.ComponentModel.DataAnnotations;


namespace SE407_Fiori
{
    public class FunctionalClass
    {
        //FunctionalClassId
        [Required(ErrorMessage = "FunctionalClassId is required")]
        public Guid FunctionalClassId { get; set; }

        //FunctionalClassType
        [Required(ErrorMessage = "Functional Class Type is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Functional Class Type is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Functional Class Type is 100 characters")]
        public string FunctionalClassType { get; set; }
    }
}
