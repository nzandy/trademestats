﻿using Newtonsoft.Json;
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

namespace TradeMeAPI {
	class Program {
		const string CONSUMER_KEY = "10E46E6F1019249C17FDF2DE6F6787EA";
		const string CONSUMER_SECRET = "7560BA2CAB4AF54FF2300F5D4C327E74";
		const string SIGNATURE_METHOD = "PLAINTEXT";

		static void Main(string[] args) {
			ListingContainer jsonResult;

			JsonSerializerSettings settings = new JsonSerializerSettings() {
				Error = delegate(object sender, ErrorEventArgs errorArgs) {
					Console.WriteLine(errorArgs.ErrorContext.Error);
				}
			};


			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", GetAuthorizationHeader());
			client.GetAsync("https://api.tmsandbox.co.nz/v1/Search/Property/Rental.json").ContinueWith(
				async (requestTask) => {
					HttpResponseMessage response = requestTask.Result;
					string json = await response.Content.ReadAsStringAsync();
					jsonResult = JsonConvert.DeserializeObject<ListingContainer>(json, settings);
				});

			Console.ReadLine();


			// try build a GET request for trademe rental properties.
		}

		public static string GetAuthorizationHeader() {
			StringBuilder str = new StringBuilder();
			str.AppendFormat(@"oauth_consumer_key=""{0}"", ", CONSUMER_KEY);
			str.AppendFormat(@"oauth_signature_method=""{0}"", ", SIGNATURE_METHOD);
			str.AppendFormat(@"oauth_signature=""{0}&""", CONSUMER_SECRET);
			return str.ToString();
		}
	}
}
