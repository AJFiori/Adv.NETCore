using System;
using System.ComponentModel.DataAnnotations;


namespace SE407_Fiori
{
    public class County
    {
        //CountyId
        [Required(ErrorMessage = "CountyId is required")]
        public Guid CountyId { get; set; }

        //CountyName
        [Required(ErrorMessage = "County Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of County Name is 5 characters")]
        [MaxLength(255, ErrorMessage = "Max length of County Name is 255 characters")]
        public string CountyName { get; set; }
    }
}
