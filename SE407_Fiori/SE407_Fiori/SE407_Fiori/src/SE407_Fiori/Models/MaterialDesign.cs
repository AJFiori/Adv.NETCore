using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class MaterialDesign
    {
        //MaterialDesignId
        [Required(ErrorMessage = "MaterialDesignId is required")]
        public Guid MaterialDesignId { get; set; }

        //MaterialDesignType
        [Required(ErrorMessage = "Material Design Type is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Material Design Type is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Material Design Type is 100 characters")]
        public string MaterialDesignType { get; set; }
    }
}
