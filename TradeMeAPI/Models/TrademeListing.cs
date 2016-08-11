using System.ComponentModel.DataAnnotations;

namespace TradeMeAPI.Models {
	public abstract class TrademeListing {
		[Key]
		public int ListingId { get; set; }
	}
}