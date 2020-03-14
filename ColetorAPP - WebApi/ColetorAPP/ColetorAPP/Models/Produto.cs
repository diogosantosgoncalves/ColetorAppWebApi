using SQLite;
using System;

namespace ColetorAPP.Models
{
    [Table("Produto")]
    public class Produto
    {
        [PrimaryKey, AutoIncrement]

        public int Id
        {
            get;
            set;
        }
        [NotNull]
        public String Nome
        {
            get;
            set;
        }
        [NotNull]
        public String Setor
        {
            get;
            set;
        }
        [NotNull]
        public Boolean Inativo
        {
            get; set;
        }
        //[NotNull]
        public int Quantidade
        {
            get;
            set;

        }

        public Produto()
        {
            this.Id = 0;
            this.Setor = "";
            this.Nome = "";
            this.Inativo = false;
            this.Quantidade = 0;
        }
    }
}
