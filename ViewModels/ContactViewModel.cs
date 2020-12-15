using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        [DisplayName("Phone #")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public string Message { get; set; } = string.Empty;
    }
}
