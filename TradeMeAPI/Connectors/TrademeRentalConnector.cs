﻿using System.Diagnostics;
using TradeMeAPI.Models;

namespace TradeMeAPI.Connectors {
	public class TrademeRentalConnector : TrademeApiConnector<RentalListing>, IRentalConnector {
		public TrademeRentalConnector(string relativeUri, TraceListener trace) : base(relativeUri, trace) {
		}
	}
}
	
	
