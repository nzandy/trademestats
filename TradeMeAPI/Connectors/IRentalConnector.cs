using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeMeAPI.Models;

namespace TradeMeAPI.Connectors {
	interface IRentalConnector {
		Task<IEnumerable<RentalListing>> GetRentals();
	}
}
