using System.ComponentModel.DataAnnotations.Schema;
using RazorClassLibrary.Models.Entities.Base;

namespace RazorClassLibrary.Models.Entities.Parking
{
	public class ParkingOpeningHour : BaseEntity<Guid>
    {
		public Guid ParkingId { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime OpeningTime { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime ClosingTime { get; set; }
		public int Day { get; set; }

		[ForeignKey("ParkingId")]
		[InverseProperty("ParkingOpeningHour")]
		public virtual Parking Parking { get; set; }
	}
}