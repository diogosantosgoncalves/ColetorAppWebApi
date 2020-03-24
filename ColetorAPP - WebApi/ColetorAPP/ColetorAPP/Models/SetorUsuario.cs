using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColetorAPP.Models
{
    [Table("SetorUsuario")]
    public class SetorUsuario
    {
        [PrimaryKey, AutoIncrement]
        public int setorusuario_id { get; set; }
        public int setorusuario_usu_id { get; set; }
        public int setorusuario_setor_id { get; set; }
    }
}
