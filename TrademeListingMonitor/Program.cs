using System.Collections.Generic;
using System.Linq;
using TradeMeAPI.Connectors;
using TradeMeAPI.Models;
using TradeMeStats.DataModel;

namespace TrademeListingMonitor {
	class Program {
		static void Main(string[] args) {
			var rentalConnector = new TrademeRentalConnector();
			IEnumerable<RentalListing> rentalListings = rentalConnector.GetRentals();
			//save listings to database.
			TrademeStatsContext dbContext = new TrademeStatsContext();

			foreach (var rentalListing in rentalListings) {
				dbContext.RentalListings.Add(rentalListing);
			}

			dbContext.SaveChanges();

			// try build a GET request for trademe rental properties.
		}
}
}
