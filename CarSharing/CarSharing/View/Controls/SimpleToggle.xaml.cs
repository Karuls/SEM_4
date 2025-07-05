using System.Windows;
using System.Windows.Controls;

namespace CarSharing.Controls
{
    public partial class SimpleToggle : UserControl
    {
        // DependencyProperty с валидацией и коррекцией
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register(
                "IsActive",
                typeof(bool),
                typeof(SimpleToggle),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnIsActiveChanged),
                    new CoerceValueCallback(CoerceIsActive)),
                new ValidateValueCallback(ValidateIsActive));

        // RoutedEvent с Direct маршрутизацией
        public static readonly RoutedEvent ToggledEvent =
            EventManager.RegisterRoutedEvent(
                "Toggled",
                RoutingStrategy.Direct,
                typeof(RoutedEventHandler),
                typeof(SimpleToggle));

        public event RoutedEventHandler Toggled
        {
            add { AddHandler(ToggledEvent, value); }
            remove { RemoveHandler(ToggledEvent, value); }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public SimpleToggle()
        {
            InitializeComponent();
        }

        private static bool ValidateIsActive(object value)
        {
            // Простая валидация - принимаем только true/false
            return value is bool;
        }

        private static object CoerceIsActive(DependencyObject d, object baseValue)
        {
            // Коррекция - если контрол выключен, всегда возвращаем false
            SimpleToggle toggle = (SimpleToggle)d;
            return !toggle.IsEnabled ? false : baseValue;
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SimpleToggle toggle = (SimpleToggle)d;
            toggle.toggleButton.Content = (bool)e.NewValue ? "ON" : "OFF";

            // Вызываем событие
            toggle.RaiseEvent(new RoutedEventArgs(ToggledEvent));
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            IsActive = !IsActive;
        }
    }
}