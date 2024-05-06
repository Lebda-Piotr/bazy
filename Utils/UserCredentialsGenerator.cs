using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace bazy1.Utils {
	public class UserCredentialsGenerator {

		public string generateLogin(User tempUser) {

			//Login w formacie [1 litera imienia][1 litera nazwiska][0-9][0-9][0-9][0-9][0-9]
			var numbers = Enumerable.Range(1, 5).Select(x => new Random().Next(0, 9));
			var tempLogin = tempUser.Name.First().ToString().ToLower() + tempUser.Surname.First().ToString().ToLower();
			foreach (var number in numbers)
			{
				tempLogin+= number;
			}
			Console.WriteLine(numbers);
			return tempLogin;
		}

		public string generatePassword() {
			string tempPass="";
			var randomGenerator = new Random();

			//Pierwsze hasło w formacie [0-9][0-9][0-9][0-9][Znak ASCII od !-~][Znak ASCII od !-~][Znak ASCII od !-~]
			for (int i = 0; i < 7; i++)
			{
				if (i < 4) tempPass += randomGenerator.Next(0, 9);
				else tempPass += Convert.ToChar(randomGenerator.Next(33, 126));
			}
			return tempPass;
		}
	}
}
