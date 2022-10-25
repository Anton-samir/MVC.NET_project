using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Display(Name ="Full Name")]
        [Required(ErrorMessage = "You have to provide a Valid Description")]
        [MinLength(2, ErrorMessage = "FullName mustnot be less than 5 charachters")]
        [MaxLength(20, ErrorMessage = "FullName Mustnot Exceed 50 Characters")]
        public string FullName { get; set; }
        [Display(Name = "Occupation")]
        [Required(ErrorMessage = "You have to provide a Valid Description")]
        [MinLength(2, ErrorMessage = "Postion mustnot be less than 5 charachters")]
        [MaxLength(20, ErrorMessage = "Postion Mustnot Exceed 50 Characters")]
        public string Postion { get; set; }
        [Range(2500, 25000, ErrorMessage = "Bouns must be Between than EGP1000")]
        public double Salary { get; set; }
        [Range(1000, double.MaxValue, ErrorMessage = "Bonus mustn't be less than EGP 1000")]
        public double Bonus { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }


        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address.")]
        public string EmailAddress { get; set; }

        [NotMapped]
        [Compare("EmailAddress", ErrorMessage = "Password not match")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Password not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public DateTime HiringDateTime { get; set; }


        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [DataType(DataType.Time)]
        public DateTime AttendanceTime { get; set; }


        [DataType(DataType.Time)]
        public DateTime LeavingTime { get; set; }

        [ValidateNever]
        public DateTime CreatedAt { get; set; }

        [ValidateNever]
        public DateTime LastUpdatedAt { get; set; }
        [ValidateNever]
        [Display(Name = "Department")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a valid department.")]
        public int DepartmentId { get; set; }

        [ValidateNever]
        public Department Department { get; set; }
        [ValidateNever]
        public string ImageUrl { get; internal set; }
    }
}
