using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JopPortal.Models
{

    public class FindCompany
    {
        [Required]
        public string CompanyEmail { get; set; }
        [Required]
        public string CompanyPassword { get; set; }
    }
    public class Company
    {

        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Company Email is required")]
        [EmailAddress]
        public string CompanyEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must consist of at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password requirements not met")]
        public string CompanyPassword { get; set; }

        [Compare("CompanyPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }

        public string? PhotoPath { get; set; }
        [NotMapped]
        public IFormFile? CompanyPhoto { get; set; }

        [Required(ErrorMessage = "Company Description is required")]

        public string? CompanyDescription { get; set; }

        public virtual ICollection<Job>? Jobs { get; set; }


    }
}
