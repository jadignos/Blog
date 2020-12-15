using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } 

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(25)]
        [DisplayName("Phone #")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        public DateTime SentDate { get; set; } = DateTime.UtcNow;
    }
}
