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
using ControleFinanceiro.View;
using System.IO.IsolatedStorage;
using ControleFinanceiro.Control;
using System.Collections.ObjectModel;

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
            NavigationService.Navigate(new Uri("/View/CadUsuario.xaml", UriKind.Relative));
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

                //Consulta usuario no Banco
                try
                {
                    bool busca = buscaUsuario();
                    if (busca)
                    {
                        //Armazena os dados do Usuário enquanto logado
                        varLogin["id"] = usuario.Id;
                        varLogin["login"] = usuario.nome;
                        varLogin["senha"] = usuario.senha;
                        //varLogin["login"] = "Abner";
                        //varLogin["senha"] = "abner";

                        //Muda de tela
                        NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("Usuário não consta no sistema.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao buscar dados no banco.");
                }
            }
            else
            {
                MessageBox.Show("Preencha os campos corretamente.");
            }
        }

        //Consulta Usuário
        private bool buscaUsuario()
        {
            bool busca = false;
            
            //Faz a consulta no banco e retorna
            using (var conexao = new BancoDados())
            {
                if (!conexao.DatabaseExists())
                {
                    conexao.CreateDatabase();
                }

                var u = from usuarios in conexao.usuarios where usuarios.Nome == usuario.nome 
                            && usuarios.Senha == usuario.senha select usuarios;

                ObservableCollection<Usuario> user = new ObservableCollection<Usuario>(u);
           
                if (user.Count() > 0)
                {
                    usuario.Id = user[0].Id;
                    busca = true;
                }
                else
                {
                    busca = false;
                }
            }
            return busca;
        }
    }
}