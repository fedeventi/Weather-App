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
        List<Line> lines=new List<Line>();
       
        public MainWindow()
        {
            InitializeComponent();
            titlebar.MouseDown += TitleBar_MouseDown;
            SetUpWindow();
        }
        
        void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
         void Resize(bool expand)
         {
            content.Visibility = expand?Visibility.Visible:Visibility.Collapsed;
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

           
        }

        private void minimize_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        void SetUpWindow()
        {
            Body.Height = 100;
            content.Visibility = Visibility.Collapsed;
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
            //CreateLines(4, 35);
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
        bool isOpen;
        private void CreateLines(int amount, int gap)
        {
            ClearLines(lines);
            if(!isOpen)
            for (int i = 0; i < amount; i++)
            {
                Line gradientLine = new Line();
                gradientLine.StrokeThickness = 1;
                gradientLine.X1 = 20;
                gradientLine.Y1=73+gap*i;
                gradientLine.X2 = 230;
                gradientLine.Y2 = 73+gap*i;

                LinearGradientBrush gradientBrush = new LinearGradientBrush();
                gradientBrush.StartPoint = new Point(0, 0.5f);
                gradientBrush.EndPoint = new Point(1, 0.5f);
                gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 0));
                gradientBrush.GradientStops.Add(new GradientStop(Colors.DarkOrange, 0.3f));
                gradientBrush.GradientStops.Add(new GradientStop(Colors.DarkOrange, 0.7f));
                gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

                gradientLine.Stroke = gradientBrush;
                lines.Add(gradientLine);
                mainCanvas.Children.Add(gradientLine);
            }
            isOpen = !isOpen;
        }
        private void ClearLines(List<Line> lines)
        {
            foreach (var item in lines)
            {
                if(mainCanvas.Children.Contains(item))
                        mainCanvas.Children.Remove(item);
            }
            lines.Clear();
        }
        private void OpenDropdown(bool open)
        {
            
        }
    }
    
}
