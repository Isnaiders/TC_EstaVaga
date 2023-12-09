﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RazorClassLibrary.Models.Entities.Base;
using RazorClassLibrary.Models.Enums.User;

namespace RazorClassLibrary.Models.Entities.User
{
    public class User : BaseEntity<Guid>
    {
        [StringLength(255)]
        public string Name { get; set; }
        public UserType Type { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BirthDate { get; set; }
        [Required]
        [StringLength(255)]
        public string LicenseDrive { get; set; }
        [StringLength(255)]
        public string Password { get; set; }
        public UserSession Session { get; set; }
    }
}