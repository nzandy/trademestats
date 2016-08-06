using System.Collections.Generic;
using System.Diagnostics;
using TradeMeAPI.Connectors;
using TradeMeAPI.Models;
using TradeMeStats.DataModel;

namespace TrademeListingMonitor {
	class Program {
		static void Main(string[] args) {
			TraceListener trace = new ConsoleTraceListener();
			var rentalConnector = new TrademeRentalConnector(trace);
			IEnumerable<RentalListing> rentalListings = rentalConnector.GetRentals();
			//save listings to database.
			TrademeStatsContext dbContext = new TrademeStatsContext();

			foreach (var rentalListing in rentalListings) {
				trace.WriteLine($"Adding listing ID: {rentalListing.Id}");
				dbContext.RentalListings.Add(rentalListing);
			}

			dbContext.SaveChanges();

			// try build a GET request for trademe rental properties.
		}
}
}
