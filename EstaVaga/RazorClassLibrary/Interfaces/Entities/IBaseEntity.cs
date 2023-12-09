using System.ComponentModel.DataAnnotations;
using RazorClassLibrary.Models.Enums.Base;

namespace RazorClassLibrary.Interfaces.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime WhenCreated { get; set; }
        DateTime? WhenUpdated { get; set; }
        DateTime? WhenRemoved { get; set; }
        SystemStatusType SystemStatus { get; set; }
        ValidationResult ValidationResult { get; set; }
    }
}
