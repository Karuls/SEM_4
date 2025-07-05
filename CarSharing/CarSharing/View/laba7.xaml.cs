using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;  
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CarSharing.View
{
    /// <summary>
    /// Логика взаимодействия для laba7.xaml
    /// </summary>
    public partial class laba7 : Window
    {
        public laba7()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ShowMessageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Hello, this is a custom command!");
        }

        private void ShowMessageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // Команда всегда доступна
        }
    }
}
