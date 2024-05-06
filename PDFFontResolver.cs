using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1
{
	class PDFFontResolver : IFontResolver {
		public byte[]? GetFont(string faceName) {
			throw new NotImplementedException();
		}

		public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic) {
				throw new NotImplementedException();
		}
	}
}
