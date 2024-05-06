using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Models.Part {
	internal class DiseasePart {
		public string Name { get; set; } = null!;

		public DateOnly? DateFrom { get; set; }

		public DateOnly? DateTo { get; set; }

		public sbyte? IsEnded { get; set; }

		public string? Comments { get; set; }

		public int Id { get; set; }
	}
}
