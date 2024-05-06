using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace bazy1.Views
{
	public partial class LoginView
	{
		public LoginView()
		{

			InitializeComponent();
		}

		private void BtnMinimizeClick(object sender, RoutedEventArgs e)
		{
			Window.GetWindow(this).WindowState = WindowState.Minimized;
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton.Equals(MouseButtonState.Pressed)) Window.GetWindow(this).DragMove();
		}

		private void BtnLoginClick(object sender, RoutedEventArgs e)
		{

		}


		private void BtnCloseClick(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

    }

}