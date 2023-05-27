using System.Windows.Input;

namespace Friendsbook.Controls
{
    public class ClickableGrid : Grid
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ClickableGrid), propertyChanged: CommandPropertyChanged);

        private TapGestureRecognizer _tapGestureRecognizer;

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set
            {
                if (_tapGestureRecognizer != null)
                {
                    GestureRecognizers.Remove(_tapGestureRecognizer);
                }

                SetValue(CommandProperty, value);
                _tapGestureRecognizer = new TapGestureRecognizer() { Command = value };

                GestureRecognizers.Add(_tapGestureRecognizer);
            }
        }

        private static void CommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not ClickableGrid clickableGrid)
            {
                return;
            }

            clickableGrid.Command = newValue as ICommand;
        }
    }
}
