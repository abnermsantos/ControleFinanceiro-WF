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
    public partial class Relatorio : PhoneApplicationPage
    {
        IsolatedStorageSettings varLogin = IsolatedStorageSettings.ApplicationSettings;

        public Relatorio()
        {
            InitializeComponent();
            using (var conexao = new BancoDados())
            {
                if (!conexao.DatabaseExists())
                {
                    conexao.CreateDatabase();
                }

                var m = conexao.movimentos;

                ObservableCollection<Movimentacao> lista = new ObservableCollection<Movimentacao>(m);

                popularLista(lista.ToList());
            }
        }

        //Método para Listar dados do Banco
        private void popularLista(List<Movimentacao> lista)
        {
            this.ListaBanco.Items.Clear();
            for (int i = 0; i < lista.Count; i++)
            {
                //Utiliza apenas dados daquele usuário
                if (lista[i].usuario == (Int32)varLogin["id"])
                {
                    this.ListaBanco.Items.Add(lista[i]);
                    if (lista[i].tipo == 1)
                    {
                        //Muda a cor da fonte para verde
                    }
                    else
                    {
                        //Muda a cor da fonte para vermelho
                    }
                }
            }
        }

        //Chamada do Logout
        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            varLogin.Remove("id");
            varLogin.Remove("login");
            varLogin.Remove("senha");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}