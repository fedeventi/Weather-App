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
using System.Net.Http;
using Newtonsoft.Json;
using System.Timers;
using System.Diagnostics;
using System.Windows.Threading;

namespace Weather_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Line> lines=new List<Line>();
        Stopwatch stopwatch;
        DispatcherTimer timer;
        public string defaultLine = "Set your city";
        public MainWindow()
        {
            isOpen = false;
            InitializeComponent();
            DataContext = this;
            stopwatch=new Stopwatch();
            stopwatch.Start();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.8f);
            timer.Tick += UpdateData;
            timer.Start();
            
            titlebar.MouseDown += TitleBar_MouseDown;
            SetUpWindow();
        }
        public async void GetWeatherData()
        {
            HttpClient Client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.openweathermap.org/data/2.5/forecast?lat=-34.61315&lon=-58.37723&units=metric&appid=c79704f3754bff6163aa126fc6de2111");


            HttpResponseMessage response = await Client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {

                return;
            }

            HttpContent body = response.Content;
            string result = await body.ReadAsStringAsync();

            dynamic weatherResult = JsonConvert.DeserializeObject(result);
            Temperature.Text = weatherResult.list[0].main.temp;
        }
        public void UpdateData(object sender, EventArgs e)
        {
            TimeSpan timePass = stopwatch.Elapsed;
            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", timePass.Hours, timePass.Minutes, timePass.Seconds);
            if(isOpen)
                GetWeatherData();
           
           
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
            isOpen = true;
            

            
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
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            
            
            CreateLines(4, 35);
        }

        private void City_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if(textbox.Text.Length < 3 || textbox.Text==defaultLine)
                Resize(false);
            else
                Resize(true);
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
