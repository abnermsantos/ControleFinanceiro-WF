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
using System.Data.Linq;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace ControleFinanceiro.Model
{
    [Table]
    public class Usuario : INotifyPropertyChanging
    {
        public string nome;
        public string senha;
        public event PropertyChangingEventHandler PropertyChanging;

        public Usuario() { }

        //Definindo as especificações do banco de dados
        //Coluna ID chave primária
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", 
            CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id;

        //Coluna Nome
        [Column(CanBeNull = false)]
        public string Nome
        {
            get { return nome; }
            set
            {
                if (nome == value) return;
                NotifyPropertyChanging("Nome");
                nome = value;
            }
        }

        //Coluna Senha
        [Column(CanBeNull = false)]
        public string Senha
        {
            get { return senha; }
            set
            {
                if (senha == value) return;
                NotifyPropertyChanging("Senha");
                senha = value;
            }
        }
    
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

    }
}
