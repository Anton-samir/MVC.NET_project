using Hospital.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage="You have a Valid Name")]
        [MinLength(2,ErrorMessage ="Name mustnot be less than 2 charachters")]
        [MaxLength(20,ErrorMessage="Name Mustnot Exceed 20 Characters")]
        public string Name { get; set;}
        [Required(ErrorMessage = "You have to provide a Valid Description")]
        [MinLength(5, ErrorMessage = "Description mustnot be less than 5 charachters")]
        [MaxLength(50, ErrorMessage = "Description Mustnot Exceed 50 Characters")]
        public string Description { get; set; }
        [ValidateNever]
        public ICollection<Doctor> Doctors { get; set; }
    }
}
