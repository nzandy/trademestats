﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TradeMeAPI.Models;

namespace TradeMeAPI.Connectors {
	public abstract class TrademeAPIConnector {

		const string CONSUMER_KEY = "10E46E6F1019249C17FDF2DE6F6787EA";
		const string CONSUMER_SECRET = "7560BA2CAB4AF54FF2300F5D4C327E74";
		const string SIGNATURE_METHOD = "PLAINTEXT";

		protected JsonSerializerSettings Settings = new JsonSerializerSettings() {
			Error = delegate (object sender, ErrorEventArgs errorArgs) {
				Console.WriteLine(errorArgs.ErrorContext.Error);
			}
		};

		public HttpClient Client { get; set; }
		public JsonSerializerSettings JsonSettings { get; set; }

		protected TrademeAPIConnector() {
			Client = new HttpClient();
			Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", GetAuthorizationHeader());
			Settings = new JsonSerializerSettings() {
				Error = delegate (object sender, ErrorEventArgs errorArgs) {
					Console.WriteLine(errorArgs.ErrorContext.Error);
				}
			};
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