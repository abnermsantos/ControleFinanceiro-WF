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
    public partial class Principal : PhoneApplicationPage
    {
        Usuario usuario = new Usuario();
        IsolatedStorageSettings varLogin = IsolatedStorageSettings.ApplicationSettings;
        public Principal()
        {
            InitializeComponent();
            usuario.nome = (string)varLogin["login"];
            PageTitle.Text = "Olá " + usuario.nome + "!";
        }

        //Chamada do Logout
        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            varLogin.Remove("login");
            varLogin.Remove("senha");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        //Chamadas para a tela de Movimento
        private void btnReceitas_Click(object sender, RoutedEventArgs e)
        {
            //op = 1 (receita) op = 0 (despesa)
            NavigationService.Navigate(new Uri("/Movimento.xaml?op=1", UriKind.Relative));
        }

        private void btnDespesas_Click(object sender, RoutedEventArgs e)
        {
            //op = 1 (receita) op = 0 (despesa)
            NavigationService.Navigate(new Uri("/Movimento.xaml?op=0", UriKind.Relative));
        }
    }
}