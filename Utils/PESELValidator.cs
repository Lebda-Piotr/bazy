using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Utils {

		public static class PeselValidator {

			private static readonly int[] mnozniki = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

		public static bool isValid(string pesel) {
			bool toRet = false;
			try
			{
				if (pesel == null)
				{
					toRet = false;
					return toRet;
				}
				if (pesel.Length == 11)
				{
					toRet = CountSum(pesel).Equals(pesel[10].ToString());
				}
			}
			catch (Exception)
			{
				toRet = false;
			}
			return toRet;
		}
			private static string CountSum(string pesel) {
				int sum = 0;
				for (int i = 0; i < mnozniki.Length; i++)
				{
					sum += mnozniki[i] * int.Parse(pesel[i].ToString());
				}
				int reszta = sum % 10;
				return reszta == 0 ? reszta.ToString() : (10 - reszta).ToString();
			}
		}
	}

