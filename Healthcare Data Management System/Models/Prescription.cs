using System.ComponentModel.DataAnnotations;

namespace Healthcare_Data_Management_System.Models
{
    public class Prescription
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        [StringLength(100)]
        public string Medication { get; set; }

        [Required]
        [StringLength(100)]
        public string Dosage { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }


    }
}
