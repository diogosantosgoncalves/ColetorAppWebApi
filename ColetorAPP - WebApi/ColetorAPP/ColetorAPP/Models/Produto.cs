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
        public int setor_id { get; set; }
        //public virtual Setor setor { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Setor objAsPart = obj as Setor;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return setor_id;
        }
        public bool Equals(Setor setor)
        {
            if (setor == null) return false;
            return (this.Setor.Equals(setor.setor_id));
        }

        public Produto()
        {
            this.Id = 0;
            this.Setor = "";
            this.Nome = "";
            this.Inativo = false;
            this.Quantidade = 0;
            //this.setor.setor_id = null;
        }
    }
}
