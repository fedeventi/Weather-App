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
using System.Windows.Media.Animation;
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
            SetUpWindow();
        }
        
        void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
         void Resize(bool expand)
         {
            
            Storyboard showWin = (Storyboard)this.Resources["showWin"];
            Storyboard hideWin = (Storyboard)this.Resources["hideWin"];
            BeginStoryboard(expand?showWin:hideWin);
            

            
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
        void SetUpWindow()
        {
            Application.Current.MainWindow.Height = 100;
        }

        private void close_MouseEnter(object sender, MouseEventArgs e)
        {
            close.Background = Brushes.DarkOrange;
        }
        bool _expand=true;
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            _expand = !_expand;
            Resize(!_expand);
        }

        private void City_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void City_OnFocus(object sender, RoutedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            text.Clear();
        }
        private void City_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox text= (TextBox)sender;
            if (text.Text.Length <= 0) text.Text = "Set your city";
        }
    }
    
}
