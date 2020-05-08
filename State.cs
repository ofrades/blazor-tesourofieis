using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazor_tesourofieis {
	public class State {
		public event Action OnChange;
		private readonly HttpClient _httpClient;
		private readonly IJSRuntime _js;
		private readonly NavigationManager _navigationManager;
		public State(HttpClient HttpClient, NavigationManager NavigationManager, IJSRuntime JSRuntime) {
			_httpClient = HttpClient;
			_navigationManager = NavigationManager;
			_js = JSRuntime;
		}
		public List<Mass> Mass { get; set; } = new List<Mass>();
		public string today = DateTime.Now.ToString("yyyy-MM-dd");
		public Dictionary<string, Calendar> deserializer = new Dictionary<string, Calendar>();
		public Calendar Calendar { get; set; } = new Calendar();
		public int year = DateTime.Now.Year;
		public async Task GetCalendar() {
			try {
				deserializer = await _httpClient.GetFromJsonAsync<Dictionary<string, Calendar>>($"date/{year}.json");
			} finally {
				await Focus();
			}
			NotifyStateChanged();
		}
		public async Task GetToday(string todayFromList) {
			if (todayFromList != null) {
				today = todayFromList;
			} else {
				await GetCalendar();
			}
			try {
				_navigationManager.NavigateTo($"/missa#{today}");
				Mass = await _httpClient.GetFromJsonAsync<List<Mass>>($"date/{today}.json");
				string jsonString = JsonSerializer.Serialize(Mass);
				var br = Regex.Replace(jsonString, @"(\*)([^\*]+)(\*)",
					m => string.Format("<br><em>{1}</em><br>",
						m.Groups[1].Value,
						m.Groups[2].Value,
						m.Groups[3].Value));
				Mass = JsonSerializer.Deserialize<List<Mass>>(br);
			} finally {
				await Focus();
			}
			NotifyStateChanged();
		}
		public async Task Focus() {
			var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
			await _js.InvokeVoidAsync("blazorHelpers.scrollToFragment", uri.Fragment.Substring(1));
			NotifyStateChanged();
		}
		private void NotifyStateChanged() => OnChange?.Invoke();
	}
}