using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Weather_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            titlebar.MouseDown += TitleBar_MouseDown;
            City.TextChanged += TextHasChanged;
        }
        void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState=WindowState.Minimized;
        }
        void TextHasChanged(object sender, TextChangedEventArgs e)
        {
            List<string> suggestionsList= new List<string>() {"Monte Grande", "San Francisco", "Los Angeles", "Frankfurt"  };

           // suggestionMenu.IsOpen = true;
        }

        private void minimize_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void close_MouseEnter(object sender, MouseEventArgs e)
        {
            close.Background = Brushes.DarkOrange;
        }
    }
}
