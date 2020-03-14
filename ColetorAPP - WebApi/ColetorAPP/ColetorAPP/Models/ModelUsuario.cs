using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ColetorAPP.Models
{
    [Table("Usuario")]
    public class ModelUsuario
    {
        [PrimaryKey,AutoIncrement]
        public int Id
        {
            get;
            set;
        }
        [NotNull]
        public string Nome
        {
            get;
            set;

        }
        [NotNull]
        public string Senha
        {
            get;
            set;

        }
        [NotNull]
        public string Setor
        {
            get;
            set;
        }

    }
}
