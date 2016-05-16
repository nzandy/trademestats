using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMeAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TradeMeAPI.Connectors {
	public class TrademeRentalConnector : TrademeAPIConnector, IRentalConnector {

		const int PAGE_SIZE = 500;
		const string RENTAL_PATH = "v1/Search/Property/Rental.json?";
		public TrademeRentalConnector() : base() {

		}

		public IEnumerable<RentalListing> GetRentals() {
			var listingResponse = new ListingContainer();
			var allListings = new List<RentalListing>();

			int numResults;
			int pageSize;
			int pageNum = 1;

			do {
				HttpResponseMessage response = Client.GetAsync(GetRequestUri(pageNum)).Result;
				string json = response.Content.ReadAsStringAsync().Result;

				listingResponse = JsonConvert.DeserializeObject<ListingContainer>(json, Settings);

				numResults = listingResponse.TotalCount;

				allListings.AddRange(listingResponse.List);

				pageNum++;
			} while (PAGE_SIZE * (pageNum-1) < numResults);

			return allListings;
		}

		private string GetRequestUri(int pageNum) {
			return String.Format("v1/Search/Property/Rental.json?rows={0}&page={1}", PAGE_SIZE, pageNum);
		}
	}
}
	
	
