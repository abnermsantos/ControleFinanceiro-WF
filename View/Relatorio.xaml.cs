using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using ControleFinanceiro.Model;
using System.IO.IsolatedStorage;

namespace ControleFinanceiro.View
{
    public partial class Relatorio : PhoneApplicationPage
    {
        IsolatedStorageSettings varLogin = IsolatedStorageSettings.ApplicationSettings;

        public Relatorio()
        {
            InitializeComponent();
        }

        //Chamada do Logout
        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            varLogin.Remove("login");
            varLogin.Remove("senha");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}