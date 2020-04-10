using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ColetorAPP.Models
{
    [Table("Inventario")]
    public class Inventario
    {
        [PrimaryKey, AutoIncrement]
        public int inv_codigo {get;set;}
        //public DateTime inv_dtabertura { get; set; }
        //public DateTime inv_dtfechamento { get; set; }
    }
}
