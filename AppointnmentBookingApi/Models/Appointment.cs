using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingApi.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Department { get; set; }  
        [Required]
        public DateTime Schedule { get; set; }
    }
}