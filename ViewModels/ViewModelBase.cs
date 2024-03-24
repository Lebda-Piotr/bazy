using System.ComponentModel;

namespace bazy1.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged //Klasa bazowa, wszystkie viewmodele b�d� z niej dziedziczy�	
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
