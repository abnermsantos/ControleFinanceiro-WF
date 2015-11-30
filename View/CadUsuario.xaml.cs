using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
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

namespace ControleFinanceiro.View
{
    public partial class CadUsuario : PhoneApplicationPage
    {
        Usuario usuario = new Usuario();
        IsolatedStorageSettings varLogin = IsolatedStorageSettings.ApplicationSettings;

        public CadUsuario()
        {
            InitializeComponent();
        }

        //Cadastrar novo usuário
        private void btnCadastro_Click(object sender, RoutedEventArgs e)
        {
            String login = txtUsuario.Text;
            String senha = txtSenha.Password;
            String confirma = txtConfirmar.Password;
            if (login != "" && senha != "" && confirma != "")
            {
                bool verifica = verificarSenha(senha, confirma);
                if (verifica)
                {
                    usuario.nome = login;
                    usuario.senha = senha;

                    //Armazena os dados do Usuário enquanto logado
                    varLogin["login"] = usuario.nome;
                    varLogin["senha"] = usuario.senha;

                    //cadastra no banco de dados

                    //vai para a tela principal
                    NavigationService.Navigate(new Uri("/Principal.xaml", UriKind.Relative));

                }
                else
                {
                    MessageBox.Show("Senhas não conferem.");
                }

            }
            else
            {
                MessageBox.Show("Preencha os campos corretamente.");
            }

        }

        //Método para confirmar senha
        private bool verificarSenha(String senha, String confirma)
        {
            bool verifica = false;

            if(senha.Equals(confirma))
            {
                verifica = true;
            }else{
                verifica = false;
            }

            return verifica;
        }

    }
}