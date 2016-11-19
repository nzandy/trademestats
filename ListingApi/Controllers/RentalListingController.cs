using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using TradeMeAPI.Connectors;
using TradeMeAPI.Models;
using TradeMeStats.DataModel;

namespace ListingApi.Controllers {

	[Route("api/rentallistings")]
	public class RentalListingController : ApiController {

		public RentalListingController() {
			
		}
	

		[HttpGet]
		public IEnumerable<TrademeListing> GetAllListings() {
			TrademeStatsRepository repository = new TrademeStatsRepository(new TrademeStatsContext());
			return repository.GetRentalListings();
		}
	}
}
