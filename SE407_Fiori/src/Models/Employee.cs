using System;
using System.ComponentModel.DataAnnotations;

namespace SE407_Fiori
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        //Name
        [Required(ErrorMessage = "Name is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Name is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Name is 50 characters")]
        public string Name { get; set; }

        //Designation
        [Required(ErrorMessage = "Designation is required")]
        [MinLength(5, ErrorMessage = "Minimum length of Designation is 5 characters")]
        [MaxLength(50, ErrorMessage = "Max length of Designation is 50 characters")]
        public string Designation { get; set; }

        //Salary
        [Required(ErrorMessage = "Salary is required")]
        [Range(1000, 199999.99, ErrorMessage = "Salary out of range")]
        public decimal Salary { get; set; }
    }
}
