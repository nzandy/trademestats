using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using TradeMeAPI.Connectors;
using TradeMeAPI.Models;
using TradeMeStats.DataModel;

namespace TrademeListingMonitor {
	class Program {
		static void Main(string[] args) {
			TraceListener trace = new ConsoleTraceListener();
			var rentalConnector = new TrademeRentalConnector("v1/Search/Property/Rental.json?", trace);
			IEnumerable<RentalListing> rentalListings = rentalConnector.GetListings();
			//save listings to database.
			TrademeStatsContext dbContext = new TrademeStatsContext();

			foreach (var rentalListing in rentalListings) {
				trace.WriteLine($"Adding/Updating listing ID: {rentalListing.ListingId}");
				dbContext.RentalListings.AddOrUpdate(rentalListing);
			}

			dbContext.SaveChanges();
			trace.WriteLine("Finished fetching listings, press enter to exit.");
			Console.ReadLine();
		}
}
}
