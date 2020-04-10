using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColetorAPP.Models
{

    [Table("Movimento_Produto")]
    public class MovimentoProduto
    {
        [PrimaryKey, AutoIncrement]
        public int mp_id
        {
            get;
            set;
        }
        public int mp_inventario { get; set; }
        public string mp_produto { get; set; }
        public int mp_quant { get; set; }

    }
}
