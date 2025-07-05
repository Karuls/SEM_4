using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using CarSharing;
using System.Windows;



public class SimpleCommand : ICommand
{
    private readonly Action<object> _execute;

    public SimpleCommand(Action<object> execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object parameter) => true;
    public void Execute(object parameter)
    {
        if (CanExecute(parameter)) // Проверяем, можно ли выполнить
        {
            _execute(parameter); // Выполняем действие
        }
    }




    public event EventHandler CanExecuteChanged;
}

public class MainViewModel
{
    public ICommand ButtonClickCommand { get; } // Свойство для команды

    public MainViewModel()
    {
        ButtonClickCommand = new SimpleCommand(parameter =>
        {
            if (parameter is string text)
            {
            //    Shop_window shop = new Shop_window();

                MainWindow currentWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

                //shop.Show();
                currentWindow.Close(); // Закрытие текущего окна


            }
        });
    }

}

