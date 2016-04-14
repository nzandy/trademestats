using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradeMeAPI.Models {

	public class ListingContainer {
		public int TotalCount { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }
		public IEnumerable<RentalListing> List { get; set; }
	}

	public class RentalListing {
		public int ListingId { get; set; }
		public string Title { get; set; }
		public string Category { get; set; }
		public int StartPrice { get; set; }

		private string _trademeFormatStartDate;
		[JsonProperty("StartDate")]
		public string TrademeFormatStartDate {
			get { return _trademeFormatStartDate; }
			set {
				_trademeFormatStartDate = value;
				StartDateConverted = GetDateTimeFromUnixEpoch(GetUnixEpochFromTrademeDate(value)); }
		}

		public DateTime? StartDateConverted { get; private set; }

		public double GetUnixEpochFromTrademeDate(string trademeDate) {
			string insideQuotes = Regex.Match(trademeDate, @"\(([^)]*)\)").Groups[1].Value;
			return Double.Parse(insideQuotes);
		}

		public DateTime? GetDateTimeFromUnixEpoch(double unixEpoch) {
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return dateTime.AddMilliseconds(unixEpoch);
		}
	}
}
