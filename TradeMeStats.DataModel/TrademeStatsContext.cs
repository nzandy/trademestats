using System.Data.Entity;
using TradeMeAPI.Models;

namespace TradeMeStats.DataModel {
	public class TrademeStatsContext : DbContext {
		public DbSet<RentalListing> RentalListings { get; set; }
		public DbSet<Agency> Angencies { get; set; }

		public TrademeStatsContext() : base("TrademeStats") {

		}
	}
}
