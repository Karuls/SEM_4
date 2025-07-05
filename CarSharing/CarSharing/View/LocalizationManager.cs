using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


    public static class LocalizationManager
    {
        private static ResourceDictionary GetResourceDictionary(string culture)
        {
            return new ResourceDictionary()
            {
                Source = new Uri($"/Model/Strings.{culture}.xaml", UriKind.Relative)
            };
        }

        public static void SetLanguage(string culture)
        {
            var dict = GetResourceDictionary(culture);

            // Удаляем старый словарь, если он есть
            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.StartsWith("/Model/Strings."));

            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }

            // Добавляем новый словарь
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }

