using TCC_API.Models.Enums.Base;

namespace TCC_API.Interfaces.Entities
{
    public interface IBaseEntity
    {
        DateTime WhenCreated { get; set; }
        DateTime? WhenUpdated { get; set; }
        DateTime? WhenRemoved { get; set; }
        SystemStatusType SystemStatus { get; set; }
    }
}
