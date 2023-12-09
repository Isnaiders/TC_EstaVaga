using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RazorClassLibrary.Interfaces.Entities;
using RazorClassLibrary.Models.Enums.Base;

namespace RazorClassLibrary.Models.Entities.Base
{
    public class BaseEntity<TEntityIdentifier> : IBaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            WhenCreated = DateTime.UtcNow;
            SystemStatus = SystemStatusType.Active;
            ValidationResult = new ValidationResult(null);
        }

        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime WhenCreated { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? WhenUpdated { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? WhenRemoved { get; set; }

        public SystemStatusType SystemStatus { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
    }
}
