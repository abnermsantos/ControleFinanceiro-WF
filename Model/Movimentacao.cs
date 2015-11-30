using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ControleFinanceiro.Model
{
    public class Movimentacao
    {
        public int id { get; set; }
        public string data { get; set; }
        public double valor { get; set; }
        public int tipo { get; set; } // 0 = Despesa e 1 = Receita 
        public string terceiro { get; set; } // Origem ou destino do valor
        public string usuario { get; set; } // Apenas para o Banco de Dados

        public Movimentacao()
        {
            //
        }
    }
}
