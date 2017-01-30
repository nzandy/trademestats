using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using TradeMeAPI.Connectors;
using TradeMeAPI.Models;
using TradeMeStats.DataModel;
using System.Data.Entity;
using System.Threading;

namespace TrademeListingMonitor {
	class Program {
		static void Main(string[] args) {
			TraceListener trace = new ConsoleTraceListener();
			var rentalConnector = new TrademeRentalConnector("v1/Search/Property/Rental.json?", trace);
			TrademeStatsRepository respository = new TrademeStatsRepository(new TrademeStatsContext());

			while (true) {

				IEnumerable<RentalListing> rentalListings = rentalConnector.GetListings();
				Stopwatch sw = Stopwatch.StartNew();
				sw.Start();
				foreach (var rentalListing in rentalListings) {
					trace.WriteLine($"Adding listing ID: {rentalListing.ListingId}");
					respository.AddRentalListing(rentalListing);
				}

				respository.SaveChanges();
				sw.Stop();
				trace.WriteLine($"Finished fetching listings, took {sw.Elapsed.TotalSeconds} seconds. Press enter to exit.");
				Thread.Sleep(600000);
			}
		}
}
}
