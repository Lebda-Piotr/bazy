using bazy1.Models.Part;
using bazy1.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Windows.Input;
using System.Windows.Navigation;
using MvvmDialogs;
using System.Windows;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace bazy1.ViewModels.Admin.Pages {
	public class ListUserViewModel : ViewModelBase {

		private AdminViewModel _adminViewModel;
		private ObservableCollection<User> _users = new(DbContext.Users);
		private User _selectedUser;
		private string _name, _surname, _login, _lastLogin;
		private Visibility _editFormVisible = Visibility.Hidden;
		public ICommand ShowModifyPanel { get; set; }

		public ICommand ModifyUserCommand { get; set; }
		public ICommand DeleteUserCommand { get; }
		public ObservableCollection<User> Users {
			get => _users;
			set {
				_users = value;
				OnPropertyChanged(nameof(Users));
			}
		}

		public User SelectedUser {
			get => _selectedUser;
			set {
				_selectedUser = value;
				OnPropertyChanged(nameof(SelectedUser));
			}
		}

		public string Name {
			get => _name;
			set{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public string Surname {
			get => _surname;
			set {
				_surname = value;
				OnPropertyChanged(nameof(Surname));
			}
		}
		public string Login {
			get => _login;
			set {
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}

		public AdminViewModel AdminViewModel { get => _adminViewModel; set => _adminViewModel = value; }
		public Visibility EditFormVisible {
			get => _editFormVisible; set {
				_editFormVisible = value;
				OnPropertyChanged(nameof(EditFormVisible));
			}
		}

		public string LastLogin { get => _lastLogin;
			set {
				_lastLogin = value;
				OnPropertyChanged(nameof(LastLogin));
			}
		}

		public ListUserViewModel(AdminViewModel adminViewModel) {

			_adminViewModel = adminViewModel;
			ShowModifyPanel = new BasicCommand((object obj) => EditFormVisible = Visibility.Visible);
			ModifyUserCommand = new BasicCommand((object obj) =>
			{
				var selected = DbContext.Users.Where(user => SelectedUser.Id == user.Id).First();
				var doctors = DbContext.Doctors.Where(doctor => doctor.UserId == selected.Id).First();
				doctors.Offices.Add(new Office { Number = 12 });

				if (!string.IsNullOrEmpty(Name)) selected.Name = Name;
				if (!string.IsNullOrEmpty(Surname)) selected.Surname = Surname;
				if (!string.IsNullOrEmpty(Login)) selected.Login = Login;

				DbContext.SaveChanges();
				_adminViewModel.CurrentViewModel = new ListUserViewModel(adminViewModel);


			});
			DeleteUserCommand = new BasicCommand((object obj) =>
			{
				var selected = DbContext.Users.Where(user => SelectedUser.Id == user.Id).First();
                var doctors = DbContext.Doctors.Where(doctor => doctor.UserId == selected.Id).First();
				doctors.User = null;
				DbContext.Doctors.Remove(doctors);
				doctors.Specializations.Clear();
				doctors.Offices.Clear();
				doctors.Workhours.Clear();
				doctors.Patients.Clear();

                DbContext.Users.Remove(selected);

				DbContext.SaveChanges();
				_adminViewModel.CurrentViewModel = new ListUserViewModel(adminViewModel);

			});

		}
	}
}