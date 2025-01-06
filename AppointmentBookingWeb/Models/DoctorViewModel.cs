using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingWeb.Models
{
    public class DoctorViewModel
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
