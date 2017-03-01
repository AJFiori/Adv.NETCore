using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class ConstructionDesign
    {
        //ConstructionDesignId
        [Required(ErrorMessage = "ConstructionDesignId is required")]
        public Guid ConstructionDesignId { get; set; }

        //ConstructionDesignType
        [Required(ErrorMessage = "Construction Design Type is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Construction Design Type is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Construction Design Type is 100 characters")]
        public string ConstructionDesignType { get; set; }
    }
}
