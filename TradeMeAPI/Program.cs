using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TradeMeAPI.Models;
using TradeMeAPI.Connectors;

namespace TradeMeAPI {
	class Program {


		static void Main(string[] args) {

			var rentalConnector = new TrademeRentalConnector();
			IEnumerable <RentalListing> rentalListings = rentalConnector.GetRentals();
			//save listings to database.


			// try build a GET request for trademe rental properties.
		}

	}
}
