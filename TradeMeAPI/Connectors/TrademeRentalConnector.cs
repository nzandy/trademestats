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

		public TrademeRentalConnector() : base() {

		}

		public IEnumerable<RentalListing> GetRentals() {
			ListingContainer listings;

			Client.BaseAddress = new Uri("https://api.tmsandbox.co.nz/");
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage response = Client.GetAsync("v1/Search/Property/Rental.json").Result;
			string json = response.Content.ReadAsStringAsync().Result;
			listings = JsonConvert.DeserializeObject<ListingContainer>(json, Settings);


			return listings.List;
		}
	}
}
	
	
