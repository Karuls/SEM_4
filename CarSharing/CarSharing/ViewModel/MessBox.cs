using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;


    class MessBox
    {
        public static void ShowCustomMessageBox(string message)
        {
            Window window = new Window
            {
                Width = 300,
                Height = 150,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.Black,
                AllowsTransparency = true,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            
            window.Owner = Application.Current.MainWindow;

            Grid grid = new Grid();

            
            RectangleGeometry clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, 300, 150),
                RadiusX = 20, 
                RadiusY = 20
            };
            window.Clip = clip;

       
        string text = Application.Current.TryFindResource($"{message}") as string ?? "❤";

        TextBlock textBlock = new TextBlock
        {
            Text = $"{text} ☺",
            Foreground = Brushes.White,
            FontSize = 18,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };


            
            Button button = new Button
            {
                Content = "OK",
                Width = 80,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 0, 10)
            };
            button.Click += (s, e) => window.Close();

            grid.Children.Add(textBlock);
            grid.Children.Add(button);
            window.Content = grid;

            
            window.ShowDialog();

        }
    }
