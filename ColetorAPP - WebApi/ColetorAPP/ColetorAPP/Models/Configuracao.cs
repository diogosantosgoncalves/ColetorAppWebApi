using SQLite;
using System;
//using System.Collections.Generic;
//using System.Text;

namespace ColetorAPP.Models
{  
    [Table("Configuracao")]
    public class Configuracao
    {
        [PrimaryKey, AutoIncrement]
        public int config_id { get; set; }
        public string config_ip { get; set; }
        public string config_porta { get; set; }

        public Configuracao()
        {
            //this.config_id 0;
            //this.config_ip = "";
            //this.config_porta = "";
        }
    }
}
