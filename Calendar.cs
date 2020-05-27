using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace blazor_tesourofieis {

	public class Calendar {

		[JsonPropertyName("celebration")]
		public List<Celebration> Celebration { get; set; } = new List<Celebration>();

		[JsonPropertyName("commemoration")]
		public List<Celebration> Commemoration { get; set; } = new List<Celebration>();

		[JsonPropertyName("tempora")]
		public List<Celebration> Tempora { get; set; } = new List<Celebration>();
	}

	public class Celebration {
		[JsonPropertyName("colors")]
		public List<string> Colors { get; set; }

		// [JsonPropertyName("id")]
		// public string Id { get; set; }

		// [JsonPropertyName("rank")]
		// public long Rank { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }
	}
}