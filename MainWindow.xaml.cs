﻿using System;
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
using System.Reflection;

namespace Weather_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        List<UIElement> UIElements=new List<UIElement>();
        Stopwatch stopwatch;
        bool search;
        DispatcherTimer timer;
        Controller _controller;
        string _searchedCity;
        public string defaultLine = "Set your city";
        float _lat= -34.61315f;
        float _long= -58.37723f;
        public MainWindow()
        {
            isOpen = false;
            InitializeComponent();
            _controller= new Controller(this);
            City.DataContext = this;
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
                $"https://api.openweathermap.org/data/2.5/forecast?lat={_lat}&lon={_long}&units=metric&appid=c79704f3754bff6163aa126fc6de2111");


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

            Search();
        }
        public void Search()
        {
            
            if (UIElements.Count < 1) return;
            City.Text = UIElements.Where(x => x is TextBlock).Select(x=>((TextBlock)x).Text).ToList()[_index];
            Resize(true);
            OpenDropdown(false);
        }

      

        private void City_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            _searchedCity = textbox.Text;
            if (textbox.Text.Length < 3 || textbox.Text == defaultLine)
            {
                _search.IsEnabled = false;
                OpenDropdown(false);
                Resize(false);
            }
            else
            {
                OpenDropdown(true);
                _search.IsEnabled = true;
            }
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
            
            for (int i =0; i < amount; i++)
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
                gradientBrush.GradientStops.Add(new GradientStop(Colors.DarkOrange, 0.1f));
                gradientBrush.GradientStops.Add(new GradientStop(Colors.DarkOrange, 0.9f));
                gradientBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1));

                gradientLine.Stroke = gradientBrush;
                UIElements.Add(gradientLine);
                mainCanvas.Children.Add(gradientLine);
            }
            
        }
        private void DarkenBackground(object sender, RoutedEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = (Brush)new BrushConverter().ConvertFromString("#AAAAA5B6");
        }
        private void LightenBackground(object sender, RoutedEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = (Brush)new BrushConverter().ConvertFromString("#FFFFE5B6");
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
                
            ClearUIElements(UIElements);
            if (open)
            {
                
                CreateSuggestion(true);
                suggestion.Height = 35* (UIElements.Count(x => x is TextBlock)+1);
                CreateLines(UIElements.Count(x => x is TextBlock), 35);
                
            }
            else
            {
                suggestion.Height = 35;

            }
        }

        void CreateSuggestion(bool show)
        {
            List<string> suggestionsList = new List<string>() { "Monte Grande", "San Francisco", "Los Angeles","Monte Chingolo", "Monte Paraiso"};
            var results = suggestionsList.Where(word => word.ToLower().Contains(_searchedCity.ToLower())).Take(10);

            for (int i = 0; i <results.ToList().Count; i++)
            {
                TextBlock _suggestion = new TextBlock();
                _suggestion.Text = results.ToList()[i];
                _suggestion.FontSize = 20;
                _suggestion.Padding = new Thickness(21, 5, 0, 0);
                _suggestion.VerticalAlignment = VerticalAlignment.Center;
                _suggestion.FontWeight= FontWeights.Normal;
                _suggestion.FontStyle = FontStyles.Normal;
                _suggestion.Foreground = Brushes.Gray;
                _suggestion.HorizontalAlignment = HorizontalAlignment.Left;
                Border border= new Border();
                border.Height = 35;
                border.Width = 230;
                border.Background = (Brush)new BrushConverter().ConvertFromString("#FFFFE5B6");
                Canvas.SetLeft(border, 10);
                Canvas.SetTop(border, 75+35*i);
                border.HorizontalAlignment= HorizontalAlignment.Center;
                border.VerticalAlignment = VerticalAlignment.Top;
                border.CornerRadius = i < results.ToList().Count - 1 ?new CornerRadius(0): new CornerRadius(0,0,20,20) ;
                Canvas.SetTop(_suggestion, 35 *(i+2));
                border.MouseEnter += DarkenBackground;
                border.MouseLeave += LightenBackground;
                border.MouseDown += (sender,e)=> {
                    TextBlock _textblock = border.Child as TextBlock;
                    _searchedCity = _textblock.Text;
                    City.Text = _searchedCity;
                };
                border.Child = _suggestion;
                UIElements.Add(_suggestion);
                UIElements.Add(border);
                mainCanvas.Children.Add(border);
                //mainCanvas.Children.Add(_suggestion);
                
            }

        }
        
        int _index = 0;
        bool _indexFirstUse= true;
        public void NavigateList(Key k)
        {
            //UIElements
            var list= UIElements.Where(x => x is Border).ToList();
            if (list.Count <= 0) return;
            if (_indexFirstUse)
            {
                _indexFirstUse = false;
                DarkenBackground(list[_index], null);
                return;
            }
            if(k is Key.Up)
            {
                if (_index > 0)
                    _index--;
                else
                    _index = list.Count - 1;
            }
            else if(k is Key.Down)
            {
                if (_index < list.Count - 1)
                    _index++;
                else
                    _index = 0;
            }
            DarkenBackground(list[_index],null);
            for(int i=0; i<list.Count;i++)
            {
                if (i == _index) continue;
                LightenBackground(list[i], null);
            }
            

        }

        void ClearUIElements(List<UIElement> suggestions)
        {
            foreach (var item in suggestions)
            {
                if (mainCanvas.Children.Contains(item))
                    mainCanvas.Children.Remove(item);
            }
            suggestions.Clear();
        }
    }
    
}
