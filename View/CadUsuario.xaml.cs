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
using ControleFinanceiro.Control;

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

                    //cadastra no banco de dados
                    try
                    {
                        inserirUsuário();
                        MessageBox.Show("Usuário cadastrado com sucesso.");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erro ao inserir dados no banco.");
                    }

                    //Armazena os dados do Usuário enquanto logado
                    //varLogin["login"] = usuario.nome;
                    //varLogin["senha"] = usuario.senha;

                    //vai para a tela de login
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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

        //Método para cadastrar Usuário no Banco de Dados
        private void inserirUsuário()
        {
            using (var conexao = new BancoDados())
            {
                if (!conexao.DatabaseExists())
                {
                    conexao.CreateDatabase();
                }
                Usuario u = new Usuario() { nome = txtUsuario.Text, senha = txtSenha.Password };
                conexao.usuarios.InsertOnSubmit(u);
                conexao.SubmitChanges();
            }
        }
    }
}