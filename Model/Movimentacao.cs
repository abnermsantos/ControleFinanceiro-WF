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
    public class Movimentacao : INotifyPropertyChanging
    {
        public string data;
        public double valor;
        public int tipo; // 0 = Despesa e 1 = Receita 
        public string terceiro; // Origem ou destino do valor
        public int usuario; // Id do Usuario
        public event PropertyChangingEventHandler PropertyChanging;

        public Movimentacao() { }

        //Definindo as especificações do banco de dados
        //Coluna ID chave primária
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity",
            CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id { get; set; }

        //Coluna Data
        [Column(CanBeNull = false)]
        public string Data
        {
            get { return data; }
            set
            {
                if (data == value) return;
                NotifyPropertyChanging("Data");
                data = value;
            }
        }

        //Coluna Valor
        [Column(CanBeNull = false)]
        public double Valor
        {
            get { return valor; }
            set
            {
                if (valor == value) return;
                NotifyPropertyChanging("Valor");
                valor = value;
            }
        }

        //Coluna Tipo
        [Column(CanBeNull = false)]
        public int Tipo
        {
            get { return tipo; }
            set
            {
                if (tipo == value) return;
                NotifyPropertyChanging("Tipo");
                tipo = value;
            }
        }

        //Coluna Terceiro
        [Column(CanBeNull = false)]
        public string Terceiro
        {
            get { return terceiro; }
            set
            {
                if (terceiro == value) return;
                NotifyPropertyChanging("Terceiro");
                terceiro = value;
            }
        }

        //Coluna Usuário ForeignKey
        //[Association(Storage = "usuario", ThisKey = "Usuario", OtherKey = "Id", IsForeignKey = true)]
        [Column(CanBeNull = false)]
        public int Usuario
        {
            get { return this.usuario; }
            set
            {
                if (usuario == value) return;
                NotifyPropertyChanging("Usuario");
                usuario = value;
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