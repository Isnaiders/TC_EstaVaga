using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCC_API.Interfaces.Entities;
using TCC_API.Models.Enums.Base;

namespace TCC_API.Models.Entities.Base
{
    [Serializable]
    public class BaseEntity<TEntityIdentifier> : IBaseEntity
    {
        public BaseEntity()
        {
			Id = Guid.NewGuid();
			SystemStatus = SystemStatusType.Active;
            ValidationResult = new ValidationResult("");
            WhenCreated = DateTime.UtcNow;
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
