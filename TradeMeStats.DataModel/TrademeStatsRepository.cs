using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMeAPI.Models;

namespace TradeMeStats.DataModel {

	public class TrademeStatsRepository : ITrademeStatsRepository {

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
