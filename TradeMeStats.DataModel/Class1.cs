using System.Data.Entity;
using TradeMeAPI.Models;

namespace TradeMeStats.DataModel {
	public class TrademeStatsContext : DbContext {
		DbSet<RentalListing> RentalListings { get; set; }
		DbSet<Agency> Angencies { get; set; }
	}
}
