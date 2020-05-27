using System;
using System.Collections.Generic;
using System.Linq;
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
		public readonly NavigationManager _navigationManager;
		public State(HttpClient HttpClient, NavigationManager NavigationManager, IJSRuntime JSRuntime) {
			_httpClient = HttpClient;
			_navigationManager = NavigationManager;
			_js = JSRuntime;
		}
		public bool Sidebar = false;
		public bool SidebarIsOpen = false;
		public void WhatUrl() {
			if (_navigationManager.Uri.Contains("missa")) {
				Sidebar = true;
			}
		}
		public void ToggleSidebar() {
			SidebarIsOpen = !SidebarIsOpen;
			NotifyStateChanged();
		}

		public List<Mass> Mass { get; set; } = new List<Mass>();
		public string today = DateTime.Now.ToString("yyyy-MM-dd");
		public Dictionary<string, Calendar> deserializer = new Dictionary<string, Calendar>();
		public Calendar Calendar { get; set; } = new Calendar();
		public Info Info { get; set; } = new Info();
		public int year = DateTime.Now.Year;
		public async Task GetCalendar() {
			if (!deserializer.Any()) {
				deserializer = await _httpClient.GetFromJsonAsync<Dictionary<string, Calendar>>($"date/{year}.json");
			}
			NotifyStateChanged();
		}
		public async Task GetToday(string todayFromList = "") {
			if (today == todayFromList) { }
			if (todayFromList == "") {
				todayFromList = today;
			}
			today = todayFromList;
			await GetMass(today);
			await GetTodayComponents();
			_navigationManager.NavigateTo($"/missa#{today}");
		}
		public async Task GetTodayComponents(string from = "all") {
			if (!Mass.Any()) {
				await GetMass(today);
			}
			if (from == "introitus" || from == "all") {
				GetIntroitus();
			}
			if (from == "oratio" || from == "all") {
				GetOratio();
			}
			if (from == "lectio" || from == "all") {
				GetLectio();
			}
			if (from == "graduale" || from == "all") {
				GetGraduale();
			}
			if (from == "evangelium" || from == "all") {
				GetEvangelium();
			}
			if (from == "offertorium" || from == "all") {
				GetOffertorium();
			}
			if (from == "secreta" || from == "all") {
				GetSecreta();
			}
			if (from == "prefatio" || from == "all") {
				GetPrefatio();
			}
			if (from == "communio" || from == "all") {
				GetCommunio();
			}
			if (from == "postcommunio" || from == "all") {
				GetPostcommunio();
			}
		}
		public string Title { get; set; }
		public string Description { get; set; }
		public string Color { get; set; }
		public string AdditionalInfo { get; set; }
		public List<Section> Sections = new List<Section>();
		public List<List<string>> Body = new List<List<string>>();
		public async Task GetMass(string today) {
			if (Mass.Select(c => c.Info.Date).FirstOrDefault().ToString("yyyy-MM-dd") != today || Mass.Select(c => c.Info.Date).FirstOrDefault().ToString("yyyy-MM-dd") == null) {
				Mass = await _httpClient.GetFromJsonAsync<List<Mass>>($"date/{today}.json");
				string jsonString = JsonSerializer.Serialize(Mass);
				var br = Regex.Replace(jsonString, @"(\*)([^\*]+)(\*)",
					m => string.Format("<br><em>{1}</em><br>",
						m.Groups[1].Value,
						m.Groups[2].Value,
						m.Groups[3].Value));
				Mass = JsonSerializer.Deserialize<List<Mass>>(br);
				foreach (var item in Mass) {
					if (item.Info.Title != null) {
						Title = item.Info.Title;
					} else {
						Title = item.Info.Tempora;
					}
					Description = item.Info.Description;
					if (item.Info.AdditionalInfo.Any()) {
						AdditionalInfo = item.Info.AdditionalInfo.FirstOrDefault();
					}
					Info = Mass.Select(c => c.Info).FirstOrDefault();
					Color = Info.Colors.FirstOrDefault();
					Sections = Mass.Select(c => c.Sections).FirstOrDefault();
					Body = Sections.Select(c => c.Body).FirstOrDefault();
				}
				NotifyStateChanged();
			}
		}
		public string IntroitusLabel = "Intróito";
		public string IntroitusId = "Introitus";
		public List<List<string>> IntroitusBody { get; set; } = new List<List<string>>();
		public void GetIntroitus() {
			int index = 0;
			foreach (var item in Mass) {
				IntroitusBody = item.Sections
					.Where(c => c.Id == "Introitus")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}
		public string OratioLabel = "Oração";
		public string OratioId = "Oratio";
		public List<List<string>> OratioBody { get; set; } = new List<List<string>>();
		public void GetOratio() {
			int index = 0;
			foreach (var item in Mass) {
				OratioBody = item.Sections
					.Where(c => c.Id == "Oratio")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}
		public string LectioLabel = "Epístola";
		public string LectioId = "Lectio";
		public List<List<string>> LectioBody { get; set; } = new List<List<string>>();
		public void GetLectio() {
			int index = 0;
			foreach (var item in Mass) {
				LectioBody = item.Sections
					.Where(c => c.Id == "Lectio")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}

		public string GradualeLabel = "Gradual";
		public string GradualeId = "Graduale";
		public List<List<string>> GradualeBody { get; set; } = new List<List<string>>();
		public void GetGraduale() {
			int index = 0;
			foreach (var item in Mass) {
				GradualeBody = item.Sections
					.Where(c => c.Label == "Gradual")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}

		public string EvangeliumLabel = "Evangelho";
		public string EvangeliumId = "Evangelium";
		public List<List<string>> EvangeliumBody { get; set; } = new List<List<string>>();
		public void GetEvangelium() {
			int index = 0;
			foreach (var item in Mass) {
				EvangeliumBody = item.Sections
					.Where(c => c.Id == "Evangelium")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}
		public string OffertoriumLabel = "Ofertório";
		public string OffertoriumId = "Offertorium";
		public List<List<string>> OffertoriumBody { get; set; } = new List<List<string>>();
		public void GetOffertorium() {
			int index = 0;
			foreach (var item in Mass) {
				OffertoriumBody = item.Sections
					.Where(c => c.Id == "Offertorium")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}

		public string SecretaLabel = "Secreta";
		public string SecretaId = "Secreta";
		public List<List<string>> SecretaBody { get; set; } = new List<List<string>>();
		public void GetSecreta() {
			int index = 0;
			foreach (var item in Mass) {
				SecretaBody = item.Sections
					.Where(c => c.Id == "Secreta")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}
		public string PrefatioLabel = "Prefácio";
		public string PrefatioId = "Prefatio";
		public List<List<string>> PrefatioBody { get; set; } = new List<List<string>>();
		public void GetPrefatio() {
			int index = 0;
			foreach (var item in Mass) {
				PrefatioBody = item.Sections
					.Where(c => c.Id == "Prefatio")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}
		public string CommunioLabel = "Comúnio";
		public string CommunioId = "Communio";
		public List<List<string>> CommunioBody { get; set; } = new List<List<string>>();
		public void GetCommunio() {
			int index = 0;
			foreach (var item in Mass) {
				CommunioBody = item.Sections
					.Where(c => c.Id == "Communio")
					.SelectMany(c => c.Body).ToList();
				index++;
			}
			NotifyStateChanged();
		}
		public string PostcommunioLabel = "Postcomúnio";
		public string PostcommunioId = "Postcommunio";
		public List<List<string>> PostcommunioBody { get; set; } = new List<List<string>>();
		public void GetPostcommunio() {
			int index = 0;
			foreach (var item in Mass) {
				PostcommunioBody = item.Sections
					.Where(c => c.Id == "Postcommunio")
					.SelectMany(c => c.Body).ToList();
				index++;
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