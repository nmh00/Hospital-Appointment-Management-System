using System.ComponentModel.DataAnnotations;

namespace AppointmentBookingWeb.Models
{
    public class DepartmentViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
