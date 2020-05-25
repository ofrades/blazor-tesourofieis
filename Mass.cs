using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace blazor_tesourofieis {
	public partial class Mass {
		[JsonPropertyName("info")]
		public Info Info { get; set; }

		[JsonPropertyName("sections")]
		public List<Section> Sections { get; set; }
	}

	public partial class Info {
		[JsonPropertyName("additional_info")]
		public List<string> AdditionalInfo { get; set; }

		[JsonPropertyName("colors")]
		public List<string> Colors { get; set; }

		[JsonPropertyName("date")]
		public DateTimeOffset Date { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("rank")]
		public long Rank { get; set; }

		[JsonPropertyName("supplements")]
		public List<object> Supplements { get; set; }

		[JsonPropertyName("tempora")]
		public string Tempora { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }
	}

	public partial class Section {
		[JsonPropertyName("body")]
		public List<List<string>> Body { get; set; }

		[JsonPropertyName("id")]
		public string Id { get; set; }

		[JsonPropertyName("label")]
		public string Label { get; set; }
	}
}