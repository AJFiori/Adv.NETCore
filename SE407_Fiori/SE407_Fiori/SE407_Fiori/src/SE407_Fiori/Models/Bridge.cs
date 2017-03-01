using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class Bridge
    {
        //BridgeId
        [Required(ErrorMessage = "BridgeId is required")]
        public Guid BridgeId { get; set; }

        //MaterialDesignId
        [Required(ErrorMessage = "MaterialDesignId is required")]
        public Guid MaterialDesignId { get; set; }

        //ConstructionDesignId
        [Required(ErrorMessage = "ConstructionDesignId is required")]
        public Guid ConstructionDesignId { get; set; }

        //FunctionalClassId
        [Required(ErrorMessage = "FunctionalClassId is required")]
        public Guid FunctionalClassId { get; set; }

        //StatusCodeId
        [Required(ErrorMessage = "StatusCodeId is required")]
        public Guid StatusCodeId { get; set; }

        //CountyId
        [Required(ErrorMessage = "CountyId is required")]
        public Guid CountyId { get; set; }

        //Rating
        [Range(0, 10, ErrorMessage = "Rating out of range")]
        public decimal? Rating { get; set; }

        //TotalLength
        [Required(ErrorMessage = "Total Length is required")]
        [Range(0, 199999.99, ErrorMessage = "Total Length out of range")]
        public decimal TotalLength { get; set; }

        //Built
        [Required(ErrorMessage = "Built is required")]
        [Range(1600, 2025, ErrorMessage = "Built out of range")]
        public int Built { get; set; }

        //Reconstructed
        [Range(1600, 2025, ErrorMessage = "Built out of range")]
        public int? Reconstructed { get; set; }

        //NbiNumber
        [Required(ErrorMessage = "NBI Number is required")]
        [MinLength(5, ErrorMessage = "Minimum length of NBI NUmber is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of NBI Number is 100 characters")]
        public string NbiNumber { get; set; }

        //RouteNumber
        [MinLength(5, ErrorMessage = "Minimum length of Route NUmber is 5 characters")]
        [MaxLength(25, ErrorMessage = "Max length of Route Number is 25 characters")]
        public string RouteNumber { get; set; }

        //City
        [Required(ErrorMessage = "City is required")]
        [MinLength(5, ErrorMessage = "Minimum length of City is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of City is 50 characters")]
        public string City { get; set; }

        //IntersectingFeature
        [Required(ErrorMessage = "Intersecting Feature is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Intersecting Feature is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of Intersecting Feature is 255 characters")]
        public string IntersectingFeature { get; set; }

        //CarriedFeature
        [Required(ErrorMessage = "Carried Feature is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Carried Feature is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of Carried Feature is 255 characters")]
        public string CarriedFeature { get; set; }

        //Location
        [Required(ErrorMessage = "Location is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Location is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of Location is 255 characters")]
        public string Location { get; set; }

    }
}
