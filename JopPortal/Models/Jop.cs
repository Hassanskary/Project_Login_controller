using JopPortal.Models;
using System.ComponentModel.DataAnnotations;


namespace JopPortal.Models
{
    public enum JobCategory
    {
        Customer_Service,
        Sales,
        Software_Development,
        Marketing,
        Instructor,
        Education,
        Media,
        Medical
    }
    public enum JobType
    {
        Full_Time,
        Internship,
        Freelance,
        Part_Time,
    }
    public class Job
    {
        public int JobId { get; set; }

        [Required(ErrorMessage = "Jop Category is required")]
        public JobCategory JobCategory { get; set; }

        [Required(ErrorMessage = "Jop Type is required")]
        public JobType JobType { get; set; }

        [Required(ErrorMessage = "Jop Location is required")]
        public string JopLocation { get; set; }

        [Required(ErrorMessage = "Jop Salary is required")]
        public int JopSalary { get; set; }

        [Required(ErrorMessage = "AvailablePlaces is required")]
        public int AvailablePlaces { get; set; }

        public DateTime JobDate { get; set; } = DateTime.Now;

        public string? JobDescription { get; set; }

        public string? JobStatus { get; set; }

        public Company Company { get; set; }
    }
}
