using System.ComponentModel.DataAnnotations;

namespace Healthcare_Data_Management_System.Models
{
    public class Appointment
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Patient ID is required")]
        public int PatientID { get; set; }

        public Patient Patient { get; set; }

        [Required(ErrorMessage = "Doctor ID is required")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Apopintment date is required")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment time is required")]
        public DateTime AppointmentTime { get; set; }

    }
}
