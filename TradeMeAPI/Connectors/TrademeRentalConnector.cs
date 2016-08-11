using System.Collections.Generic;
using System.Diagnostics;
using TradeMeAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace TradeMeAPI.Connectors {
	public class TrademeRentalConnector : TrademeApiConnector<RentalListing>, IRentalConnector {
		public TrademeRentalConnector(string relativeUri, TraceListener trace) : base(relativeUri, trace) {
		}
	}
}
	
	
