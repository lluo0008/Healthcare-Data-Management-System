using System.ComponentModel.DataAnnotations;

namespace Healthcare_Data_Management_System.Models
{
    public class Patient
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }


        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
