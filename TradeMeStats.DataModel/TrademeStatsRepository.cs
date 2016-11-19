using System.Linq;
using TradeMeAPI.Models;


namespace TradeMeStats.DataModel {

	public class TrademeStatsRepository : IRentalListingRepository {

		private readonly TrademeStatsContext _context;
		public TrademeStatsRepository(TrademeStatsContext context) {
			_context = context;
		}

		public IQueryable<RentalListing> GetRentalListings() {
			return _context.RentalListings;
		}

		public RentalListing AddRentalListing(RentalListing listing) {
			return _context.RentalListings.Add(listing);
		}

		public void SaveChanges() {
			_context.SaveChanges();
		}

	}
}
