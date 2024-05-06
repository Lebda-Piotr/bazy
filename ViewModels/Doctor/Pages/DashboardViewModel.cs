using bazy1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels.Doctor.Pages {
	public class DashboardViewModel : ViewModelBase {
		private User _currentUser;
		private string _t1 = "xd";

		public DashboardViewModel(User user) {
			CurrentUser = user;
			Test = new BasicCommand((object obj) =>  T1 = "lmao");
		}
		public ICommand Test{  get; set; }



		public User CurrentUser { get => _currentUser; set => _currentUser = value; }
		public string T1 { 
			get => _t1;
			set { _t1 = value; OnPropertyChanged(nameof(T1)); }
		}
	}
}
