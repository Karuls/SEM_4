using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CarSharing.View
{
    public partial class laba6 : Window
    {
        public laba6()
        {
            InitializeComponent();
            multiTriggerButton.PreviewMouseLeftButtonDown += MultiTriggerButton_PreviewMouseDown;
            multiTriggerButton.PreviewMouseLeftButtonUp += MultiTriggerButton_PreviewMouseUp;
        }

        private void MultiTriggerButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (multiTriggerButton.IsMouseOver)
            {
                mainGrid.Background = new SolidColorBrush(Color.FromRgb(0xF5, 0xE0, 0x7F)); // Светло-желтый
            }
        }

        private void MultiTriggerButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            mainGrid.Background = Brushes.White;
        }

        private void ResetBackground_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Background = Brushes.White;
            dataTriggerCheck.IsChecked = false;
        }
    }
}