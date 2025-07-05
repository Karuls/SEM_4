using System.Windows;
using System.Windows.Controls;

namespace CarSharing.Controls
{
    public partial class SimpleTextBox : UserControl
    {
        //static SimpleTextBox()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(
        //        typeof(SimpleTextBox),
        //        new FrameworkPropertyMetadata(typeof(SimpleTextBox)));
        //}

        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register(
                "TextValue",
                typeof(string),
                typeof(SimpleTextBox),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnTextValueChanged),
               new ValidateValueCallback(ValidateTextValue));

        public static readonly RoutedEvent TextChangingEvent =
            EventManager.RegisterRoutedEvent(
                "TextChanging",
                RoutingStrategy.Tunnel,
                typeof(RoutedEventHandler),
                typeof(SimpleTextBox));

        public static readonly RoutedEvent TextChangedEvent =
            EventManager.RegisterRoutedEvent(
                "TextChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(SimpleTextBox));

        public string TextValue
        {
            get => (string)GetValue(TextValueProperty);
            set => SetValue(TextValueProperty, value);
        }

        public event RoutedEventHandler TextChanging
        {
            add => AddHandler(TextChangingEvent, value);
            remove => RemoveHandler(TextChangingEvent, value);
        }

        public event RoutedEventHandler TextChanged
        {
            add => AddHandler(TextChangedEvent, value);
            remove => RemoveHandler(TextChangedEvent, value);
        }

        private static bool ValidateTextValue(object value)
        {
            return value is string str && str.Length <= 10;
        }

        private static void OnTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (SimpleTextBox)d;
            
            // Tunneling event
            control.RaiseEvent(new RoutedEventArgs(TextChangingEvent));
            
            // Bubble event
            control.RaiseEvent(new RoutedEventArgs(TextChangedEvent));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            if (GetTemplateChild("textBox") is TextBox textBox)
            {
                textBox.TextChanged -= OnTextBoxTextChanged;
                textBox.TextChanged += OnTextBoxTextChanged;
            }
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text != TextValue)
            {
                TextValue = textBox.Text;
            }
        }
    }
}