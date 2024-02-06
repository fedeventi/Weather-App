using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Weather_App
{
    public class Controller
    {
        MainWindow _myWindow;
        public Controller(MainWindow myWindow)
        {
            _myWindow = myWindow;
            _myWindow.PreviewKeyDown += NavigationKeys;
            _myWindow.PreviewKeyDown += EnterSearch;
        }
        public void NavigationKeys(object sender,KeyEventArgs e)
        {
           if(e.Key == Key.Up || e.Key == Key.Down)
                   _myWindow.NavigateList(e.Key); 
        }
        public void EnterSearch(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                _myWindow.Search();
                Keyboard.ClearFocus();
                
            }
        }
    }
}
