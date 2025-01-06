using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingApi.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specialty { get; set; }
        [Required]
        public string Department { get; set; }  
    }
}