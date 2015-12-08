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
using System.Collections.ObjectModel;

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
            buscaSaldo();
        }

        //Carregar informações ao entrar na página
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            buscaSaldo();
        }

        //Chamada do Logout
        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            varLogin.Remove("id");
            varLogin.Remove("login");
            varLogin.Remove("senha");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        
        //Chamadas para a tela de Relatório
        private void btnRelatorioGeral_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Relatorio.xaml", UriKind.Relative));
        }

        //Chamadas para a tela de Movimento
        private void btnReceitas_Click(object sender, RoutedEventArgs e)
        {
            //op = 1 (receita) op = 0 (despesa)
            NavigationService.Navigate(new Uri("/View/Movimento.xaml?op=1", UriKind.Relative));
        }

        //Chamadas para a tela de Movimento
        private void btnDespesas_Click(object sender, RoutedEventArgs e)
        {
            //op = 1 (receita) op = 0 (despesa)
            NavigationService.Navigate(new Uri("/View/Movimento.xaml?op=0", UriKind.Relative));
        }

        //Função para calcular o saldo
        private void buscaSaldo()
        {
            double receitas = 0.0;
            double despesas = 0.0;
            double saldo = 0.0;

            using (var conexao = new BancoDados())
            {
                if (!conexao.DatabaseExists())
                {
                    conexao.CreateDatabase();
                }

                var m = conexao.movimentos;

                ObservableCollection<Movimentacao> lista = new ObservableCollection<Movimentacao>(m);

                for (int i = 0; i < lista.Count; i++)
                {
                    //Utiliza apenas dados daquele usuário
                    if (lista[i].usuario == (Int32)varLogin["id"])
                    {
                        if (lista[i].tipo == 1)
                        {
                            receitas += lista[i].valor;
                        }
                        else
                        {
                            despesas += lista[i].valor;
                        }
                    }
                }
            }
            saldo = receitas - despesas;
            txtSaldo.Text = "R$ "+ saldo.ToString();
        }
    }
}