using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using TradeMeAPI.Connectors;
using TradeMeAPI.Models;
using TradeMeStats.DataModel;
using System.Data.Entity;

namespace TrademeListingMonitor {
	class Program {
		static void Main(string[] args) {
			TraceListener trace = new ConsoleTraceListener();
			var rentalConnector = new TrademeRentalConnector("v1/Search/Property/Rental.json?", trace);
			IEnumerable<RentalListing> rentalListings = rentalConnector.GetListings();
			//save listings to database.
			TrademeStatsRepository respository = new TrademeStatsRepository(new TrademeStatsContext());
			foreach (var rentalListing in rentalListings) {
				trace.WriteLine($"Adding listing ID: {rentalListing.ListingId}");
				respository.AddRentalListing(rentalListing);
			}

			respository.SaveChanges();
			trace.WriteLine("Finished fetching listings, press enter to exit.");
			Console.ReadLine();
		}
}
}
