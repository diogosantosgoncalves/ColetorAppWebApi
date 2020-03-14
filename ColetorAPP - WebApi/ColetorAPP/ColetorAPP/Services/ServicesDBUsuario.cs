using ColetorAPP.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ColetorAPP.Services
{
    public class ServicesDBUsuario
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public ServicesDBUsuario(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModelUsuario>();
        }

        public void Inserir(ModelUsuario usu)
        {
            try
            {
                if (string.IsNullOrEmpty(usu.Setor))
                {
                    usu.Setor = "Padrao";
                }
                int result = conn.Insert(usu);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registros adicionado(s):" +
                    " [Usuario: {1} ", result, usu.Nome);
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
        public int LocalizarUsuario(ModelUsuario usuario)
        {
            //List<ModelUsuario> lista = new List<ModelUsuario>();
            var resp = from p in conn.Table<ModelUsuario>()
                       where  p.Nome == usuario.Nome || p.Senha == usuario.Senha
                       select p;
            int lista = resp.Count();
            return lista;

        }
        public void Alterar_Usuario(ModelUsuario usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Nome))
                {
                    throw new Exception("Nome não informado!");
                }
                if (string.IsNullOrEmpty(usuario.Senha))
                {
                    throw new Exception("Senha não informada");
                }
                int result = conn.Update(usuario);
                StatusMessage = string.Format("{0} Registros alterados!", result);
            }
            catch(Exception ex)
            {
               throw new Exception(string.Format("Erro: {0} ", ex.Message));
            }
        }
    }
}
