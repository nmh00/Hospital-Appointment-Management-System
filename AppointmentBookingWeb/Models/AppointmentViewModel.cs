using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingWeb.Models
{
    public class AppointmentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Number")]
        public string ContactNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public DateTime Schedule { get; set; }
    }
}
