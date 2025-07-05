using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarSharing.Controls
{
    public static class CustomCommands// RoutedUICommand
    {
        public static readonly RoutedUICommand ShowMessageCommand = new RoutedUICommand(
            "Show Message", 
            "ShowMessageCommand", 
            typeof(CustomCommands),
            new InputGestureCollection
            {
            new KeyGesture(Key.Enter, ModifierKeys.Control) // Гор Ctrl+Enter
            });
    }
}