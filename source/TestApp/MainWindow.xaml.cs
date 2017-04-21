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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonTest_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TextButtonCoord.Text = e.GetPosition((Button)sender).ToString();
            TextButtonButton.Text = e.ChangedButton.ToString();
            TextButtonModifier.Text = string.Empty;

            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                TextButtonModifier.Text += "CTRL";
            }

            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                TextButtonModifier.Text += "SHIFT";
            }

            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
            {
                TextButtonModifier.Text += "ALT";
            }
        }
    }
}
