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

namespace ControleFinanceiro
{
    public partial class MainPage : PhoneApplicationPage
    {
        Usuario usuario = new Usuario();
        IsolatedStorageSettings varLogin = IsolatedStorageSettings.ApplicationSettings;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            txtUsuario.Text = "";
            txtSenha.Password = "";
        }

        //Chamar tela de cadastro de usuário
        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CadUsuario.xaml", UriKind.Relative));
        }

        //Chamar a tela principal após o login
        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            String login = txtUsuario.Text;
            String password = txtSenha.Password;

            if (login != "" && password != "")
            {
                usuario.nome = login;
                usuario.senha = password;
                
                //Armazena os dados do Usuário enquanto logado
                varLogin["login"] = usuario.nome;
                varLogin["senha"] = usuario.senha;
                
                //Faça conexão depois mude de tela
                NavigationService.Navigate(new Uri("/Principal.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Preencha os campos corretamente.");
            }

        }
    }
}