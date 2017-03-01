using System;
using System.ComponentModel.DataAnnotations;
namespace SE407_Fiori
{
    public class StatusCode
    {
        //StatusCodeId
        [Required(ErrorMessage = "StatusCodeId is required")]
        public Guid StatusCodeId { get; set; }

        //StatusName
        [Required(ErrorMessage = "Status Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Status Name is 5 characters")]
        [MaxLength(100, ErrorMessage = "Max length of Status Name is 100 characters")]
        public string StatusName { get; set; }
    }
}
