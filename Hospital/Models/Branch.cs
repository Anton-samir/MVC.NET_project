using System.ComponentModel.DataAnnotations;

namespace Hospital.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [MinLength(5,ErrorMessage ="Name mustnt be less than 5 charaters")]
        [MaxLength(50, ErrorMessage = "Name mustnt be exceed than 50 charaters")]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
