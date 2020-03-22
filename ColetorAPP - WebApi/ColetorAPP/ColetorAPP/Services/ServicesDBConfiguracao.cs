using ColetorAPP.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColetorAPP.Services
{
    public class ServicesDBConfiguracao
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public ServicesDBConfiguracao(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Configuracao>();
        }

        public void Inserir(Configuracao usu)
        {
            try
            {
                if (string.IsNullOrEmpty(usu.config_porta))
                {
                    usu.config_porta = "Padrao";
                }
                int result = conn.Insert(usu);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registros adicionado(s):" +
                    " [Usuario: {1} ", result, usu.config_porta);
                }
                else
                {
                    this.StatusMessage = string.Format("0 registros adicionado(s): " +
                    "Informe o Usuario e a Senha!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Configuracao> Buscar()
        {
            List<Configuracao> lista = new List<Configuracao>();
            try
            {
              var  m = from p in conn.Table<Configuracao>()
                    where p.config_id == 1
                    select p;
            lista =  m.ToList();
               // StatusMessage = "Encontrou uma nota";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
        public void Alterar_Usuario(Configuracao usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.config_porta))
                {
                    throw new Exception("Nome não informado!");
                }
                if (string.IsNullOrEmpty(usuario.config_ip))
                {
                    throw new Exception("Senha não informada");
                }
                int result = conn.Update(usuario);
                StatusMessage = string.Format("{0} Registros alterados!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0} ", ex.Message));
            }
        }
    }
}
