using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class User
    {
        public Guid UserID { get; set; }

        //FirstName
        [Required(ErrorMessage = "First Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of First Name is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of First Name is 25 characters")]
        public string FirstName { get; set; }

        //LastName
        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Last Name is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Last Name is 25 characters")]
        public string LastName { get; set; }

        //Email
        [Required(ErrorMessage = "Email is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Email is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Email is 100 characters")]
        public string Email { get; set; }

        //Password
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Password is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Password is 255 characters")]
        public string Password { get; set; }

    }
}
