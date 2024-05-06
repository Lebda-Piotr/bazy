using bazy1.ViewModels;
using bazy1.ViewModels.Admin;
using bazy1.ViewModels.Doctor;
using bazy1.Views;
using bazy1.Views.Admin;
using bazy1.Views.Doctor;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MvvmDialogs;
using System.Configuration;
using System.Data;
using System.Windows;

namespace bazy1
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
		}

		protected void ApplicationStartup(object sender, StartupEventArgs e) {
			var vm = new LoginViewModel();
			var loginView = new LoginView
			{
				DataContext = vm
			};
			vm.LoginCompleted += (s, e) =>
			{
				if (((UserEventArgs)e).UserType.Equals("lekarz"))
				{
					var mainView = new DoctorView();
					loginView.Close();
					mainView.Show();
				}else if (((UserEventArgs)e).UserType.Equals("admin")){
					var mainView = new AdminView();
					loginView.Close();
					mainView.Show();
				}
			};
			loginView.Show();
			
		}

		private void Vm_LoginCompleted(object? sender, EventArgs e) {
			throw new NotImplementedException();
		}
	}

}
