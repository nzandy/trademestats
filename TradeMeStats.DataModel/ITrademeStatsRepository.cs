using System.Linq;
using TradeMeAPI.Models;

namespace TradeMeStats.DataModel {
	public interface ITrademeStatsRepository {
		IQueryable<RentalListing> GetRentalListings();
		RentalListing AddRentalListing(RentalListing listing);
		void SaveChanges();
	}
}