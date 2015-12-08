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
using ControleFinanceiro.Control;

namespace ControleFinanceiro.View
{
    public partial class Despesas : PhoneApplicationPage
    {
        Movimentacao movimentacao = new Movimentacao();
        IsolatedStorageSettings varLogin = IsolatedStorageSettings.ApplicationSettings;
        
        public Despesas()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string op = "";
            if(NavigationContext.QueryString.TryGetValue("op", out op)) {
                movimentacao.tipo = Int32.Parse(op);
            }
            if (movimentacao.tipo == 1)
            {
                PageTitle.Text = "Receitas";
                labelTerceiro.Text = "Origem";
            }
            else if (movimentacao.tipo == 0)
            {
                PageTitle.Text = "Despesas";
                labelTerceiro.Text = "Local";
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            txtLocal.Text = "";
            txtData.Text = "";
            txtValor.Text = "";
        }

        //Chamada do Logout
        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            varLogin.Remove("login");
            varLogin.Remove("senha");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        //Cadastrar Movimentação
        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            if (txtValor.Text != "" && txtData.Text != "" && txtLocal.Text != "")
            {
                movimentacao.data = txtData.Text;
                movimentacao.terceiro = txtLocal.Text;
                movimentacao.valor = double.Parse(txtValor.Text);
                movimentacao.usuario = (Int32)varLogin["id"];

                //Cadastra no Banco de Dados
                try
                {
                    inserirMovimento();
                    MessageBox.Show("Dados cadastrados com sucesso");
                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao inserir dados no banco.");
                }
                

                //Retorna para a tela anterior
                NavigationService.Navigate(new Uri("/View/Principal.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Preencha os campos corretamente.");
            }
        }

        //Método para cadastrar movimento no Banco de Dados
        private void inserirMovimento()
        {
            using (var conexao = new BancoDados())
            {
                if (!conexao.DatabaseExists())
                {
                    conexao.CreateDatabase();
                }
                conexao.movimentos.InsertOnSubmit(movimentacao);
                conexao.SubmitChanges();
            }
        }
    }
}