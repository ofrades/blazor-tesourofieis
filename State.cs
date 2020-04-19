using System;
using System.Net.Http;
using System.Net.Http.Json;

using System.Threading.Tasks;

namespace blazor_tesourofieis {
	public class State {

		private readonly HttpClient _httpClient;
		public State(HttpClient HttpClient) {
			_httpClient = HttpClient;
		}

		public Mass Mass { get; set; } = new Mass();
		public DateTime today = DateTime.Now;
		public async Task GetMass() {
			Mass = await _httpClient.GetFromJsonAsync<Mass>($"date/{today.ToString("yyyy-MM-dd")}.json");
		}


	}
}