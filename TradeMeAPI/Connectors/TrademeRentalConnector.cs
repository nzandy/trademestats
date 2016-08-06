using System.Collections.Generic;
using System.Diagnostics;
using TradeMeAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace TradeMeAPI.Connectors {
	public class TrademeRentalConnector : TrademeAPIConnector, IRentalConnector {

		const int PAGE_SIZE = 500;
		const string RENTAL_PATH = "v1/Search/Property/Rental.json?";

		private TraceListener _trace;
		public TrademeRentalConnector(TraceListener trace) : base() {
			_trace = trace;
		}

		public IEnumerable<RentalListing> GetRentals() {
			var listingResponse = new ListingContainer();
			var allListings = new List<RentalListing>();

			int numResults;
			int pageNum = 1;

			_trace.WriteLine($"Attempting to fetch rental listings from {Client.BaseAddress}");
			_trace.WriteLine($"Page size is {PAGE_SIZE}");
			do {
				string requestUri = GetRequestUri(pageNum);
				
				HttpResponseMessage response = Client.GetAsync(requestUri).Result;
				string json = response.Content.ReadAsStringAsync().Result;

				listingResponse = JsonConvert.DeserializeObject<ListingContainer>(json, Settings);

				numResults = listingResponse.TotalCount;

				allListings.AddRange(listingResponse.List);

				pageNum++;
			} while (PAGE_SIZE * (pageNum-1) < numResults);
			_trace.WriteLine($"Successfuly parsed {allListings.Count} rental listings.");
			return allListings;
		}

		private string GetRequestUri(int pageNum) {
			_trace.WriteLine($"Fetching page number {pageNum} of results");
			return string.Format($"v1/Search/Property/Rental.json?rows={PAGE_SIZE}&page={pageNum}");
		}
	}
}
	
	
