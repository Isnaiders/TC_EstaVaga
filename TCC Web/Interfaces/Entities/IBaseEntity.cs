using TCC_Web.Models.Enums.Base;

namespace TCC_Web.Interfaces.Entities
{
    public interface IBaseEntity
    {
        DateTime WhenCreated { get; set; }
        DateTime? WhenUpdated { get; set; }
        DateTime? WhenRemoved { get; set; }
        SystemStatusType SystemStatus { get; set; }
    }
}
