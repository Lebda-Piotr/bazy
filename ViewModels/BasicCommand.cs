using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace bazy1.ViewModels
{
	class BasicCommand : ICommand //klasa bazowa, wszystkie komendy będą z niej dziedziczyć
	{
		public event EventHandler? CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}

			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}
		private readonly Action<object> _executeAction;
		private readonly Predicate<object> _canExecutePredicate;

		public BasicCommand(Action<object> executeAction, Predicate<object> canExecutePredicate)
		{
			_executeAction = executeAction;
			_canExecutePredicate = canExecutePredicate;
		}

		public BasicCommand(Action<object> executeAction)
		{
			_executeAction = executeAction;
		}

		public bool CanExecute(object? parameter) => true;

		public void Execute(object? parameter)
		{
			_executeAction(parameter);
		}
	}
}
