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
        public Configuracao Buscar()
        {
            Configuracao conf = new Configuracao();
            List<Configuracao> lista = new List<Configuracao>();
            try
            {
              var  m = from p in conn.Table<Configuracao>()
                    where p.config_id == 1
                    select p;
            conf =  m.First();
               // StatusMessage = "Encontrou uma nota";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return conf;
        }
        public void Alterar(Configuracao config)
        {
            try
            {
                if (string.IsNullOrEmpty(config.config_ip) | string.IsNullOrEmpty(config.config_porta))
                {
                    throw new Exception("Informe o IP ou a Porta!");
                }
                int result = conn.Update(config);
                StatusMessage = string.Format("{0} Registros alterados!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0} ", ex.Message));
            }
        }
    }
}
