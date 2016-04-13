using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeMeAPI.Models {

	public class ListingContainer {
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public IEnumerable<RentalListing> List { get; set; }
	}

	public class RentalListing {
		public int ListingId { get; set; }
		public string Title { get; set; }
		public string Category { get; set; }
		public int StartPrice { get; set; }
		public string StartDate { get; set; }
	}
}
