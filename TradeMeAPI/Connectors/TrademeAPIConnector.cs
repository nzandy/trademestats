﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TradeMeAPI.Models;

namespace TradeMeAPI.Connectors {
	public abstract class TrademeApiConnector<T> where T : TrademeListing {

		const string ConsumerKey = "10E46E6F1019249C17FDF2DE6F6787EA";
		const string ConsumerSecret = "7560BA2CAB4AF54FF2300F5D4C327E74";
		const string SignatureMethod = "PLAINTEXT";
		const int PageSize = 500;

		protected JsonSerializerSettings Settings;

		public HttpClient Client { get; set; }
		public JsonSerializerSettings JsonSettings { get; set; }
		public string RelativeUri { get; set; }
		public IEnumerable<T> Listings { get; set; }
		private readonly TraceListener _trace;

		protected TrademeApiConnector(string relativeUri, TraceListener trace) {
			RelativeUri = relativeUri;
			_trace = trace;
			Client = new HttpClient();
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", GetAuthorizationHeader());
			Client.BaseAddress = new Uri("https://api.tmsandbox.co.nz/");
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			Settings = new JsonSerializerSettings() {
				Error = delegate (object sender, ErrorEventArgs errorArgs) {
					Console.WriteLine(errorArgs.ErrorContext.Error);
				}
			};
		}

		public IEnumerable<T> GetListings() {
			var listings = new List<T>();

			int numResults;
			int pageNum = 1;

			_trace.WriteLine($"Attempting to fetch {typeof(T).Name} listings from {Client.BaseAddress}");
			_trace.WriteLine($"Page size is {PageSize}");

			do {
				string requestUri = GetRequestUri(pageNum);

				HttpResponseMessage response = Client.GetAsync(requestUri).Result;
				string json = response.Content.ReadAsStringAsync().Result;

				var listingResponse = JsonConvert.DeserializeObject<ListingContainer<T>>(json, Settings);

				numResults = listingResponse.TotalCount;
				listings.AddRange(listingResponse.List);

				pageNum++;
			} while (PageSize * (pageNum - 1) < numResults);
			_trace.WriteLine($"Total count of listings: {numResults}");
			_trace.WriteLine($"Successfuly parsed {listings.Count} rental listings.");
			return listings;
		}

		private string GetRequestUri(int pageNum) {
			_trace.WriteLine($"Fetching page number {pageNum} of results");
			return string.Format($"{RelativeUri}rows={PageSize}&page={pageNum}");
		}

		public static string GetAuthorizationHeader() {
			StringBuilder str = new StringBuilder();
			str.AppendFormat(@"oauth_consumer_key=""{0}"", ", ConsumerKey);
			str.AppendFormat(@"oauth_signature_method=""{0}"", ", SignatureMethod);
			str.AppendFormat(@"oauth_signature=""{0}&""", ConsumerSecret);
			return str.ToString();
		}
	}
}
