using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Diagnostics;
using System.Windows;

namespace CarSharing.View
{
    public class FavoriteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            var assembly = Assembly.GetExecutingAssembly();
            string assemblyName = assembly.GetName().Name;
            string baseUri = $"pack://application:,,,/{assemblyName};component/";

            // Определяем какое изображение нужно
            string imagePath = (value is int i && i == 1)
                ? "Model/Images/liked.png"
                : "Model/Images/heart.png";

            // Пытаемся загрузить
            try
            {
                var uri = new Uri(baseUri + imagePath, UriKind.Absolute);
                Debug.WriteLine($"Trying to load: {uri}");
                return new BitmapImage(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading image: {ex.Message}");

                // Пытаемся загрузить заглушку
                try
                {
                    var fallbackUri = new Uri(baseUri + "Model/Images/Avatar.png", UriKind.Absolute);
                    return new BitmapImage(fallbackUri);
                }
                catch
                {
                    // Возвращаем пустое изображение
                    return new BitmapImage();
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}