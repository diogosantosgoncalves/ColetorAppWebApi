using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColetorAPP.Models
{
    [Table("Setor")]
    public class Setor
    {
        public int? setor_id { get; set; }
        public string setor_nome { get; set; }
        public Setor()
        {
 
        }
    }
}
