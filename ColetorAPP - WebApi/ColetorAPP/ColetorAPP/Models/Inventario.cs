using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ColetorAPP.Models
{
    [Table("Inventario")]
    class Inventario
    {
        [PrimaryKey, AutoIncrement]
        public int Inv_Codigo
        {
            get;
            set;
        }
        [NotNull]
        public List<Usuario> Usuario
        {
            get;
            set;
        }
        //[ForeignKey(typeof(ModelUsuario))]

        public int EmployeeId { get; set; }
    }
}
