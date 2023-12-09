using TCC_Web.Models.Enums.Base;

namespace TCC_Web.Models.DTOs.Base
{
    public class BaseDTO
    {
        public BaseDTO()
        {
            Id = Guid.NewGuid();
            SystemStatus = SystemStatusType.Active;
            WhenCreated = DateTime.UtcNow;
        }

        #region Base Properties
        public Guid Id { get; set; }
        public DateTime WhenCreated { get; set; }
        public DateTime? WhenUpdated { get; set; }
        public DateTime? WhenRemoved { get; set; }
        public SystemStatusType SystemStatus { get; set; }
        #endregion
    }
}
