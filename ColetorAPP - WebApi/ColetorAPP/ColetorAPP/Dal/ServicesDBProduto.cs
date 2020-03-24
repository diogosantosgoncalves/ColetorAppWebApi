using ColetorAPP.Models;
using SQLite;
using System;
using System.Collections.Generic;


namespace ColetorAPP.Services
{
    public class ServicesDBProduto
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }
        public ServicesDBProduto(string dbPath)
        {
            //if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<Produto>();
        }
        public int  Atualizar_Quantidade(string nome)
        {
            int quant = 0;
            Produto produto1 = new Produto();

            var query = from p in conn.Table<Produto>()
                        where p.Nome == nome
                        select p;

            foreach (Produto produto in query)
            {

                quant = produto.Quantidade;
            }
            return quant;
        }

        public void Inserir(Produto nota)
        {
            try
            {
                if (string.IsNullOrEmpty(nota.Nome))
                {
                    throw new Exception("Titulo dos produtos não informado!");
                }
                if (string.IsNullOrEmpty(nota.Setor))
                {
                    throw new Exception("Dados dos produtos não informado!");
                }
                
                
                //if (nota.Qtde > 0)
                //{
                //    throw new Exception("Quantidade de produtos não informado!");
                //}
                int result = conn.Insert(nota);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registros adicionado(s):" +
                    " [Nota: {1} ", result, nota.Nome);
                }
                else
                {
                    this.StatusMessage = string.Format("0 registros adicionado(s): " +
                    "Informe o título e os Dados dos produtos!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Produto> Listar()
        {
            List<Produto> lista = new List<Produto>();
            try
            {
                lista = conn.Table<Produto>().ToList();

                this.StatusMessage = "listagem de notas";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return lista;
        }
        public void Alterar(Produto notas)
        {
            try
            {
                if (string.IsNullOrEmpty(notas.Nome))
                {
                    throw new Exception("Titulo da nota não informado!");
                }
                if (string.IsNullOrEmpty(notas.Setor))
                {
                    throw new Exception("Dados da nota não informado!");
                }
                if (notas.Id <= 0)
                {
                    throw new Exception("Id da nota não informado!");
                }
                int result = conn.Update(notas);
                StatusMessage = string.Format("{0} Registros alterados!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0} ", ex.Message));
            }
        }
        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<Produto>().Delete(registro => registro.Id == id);
                StatusMessage = string.Format("{0} Registros deletados!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0} ", ex.Message));
            }
        }
        public List<Produto> Localizar(string titulo)
        {
            List<Produto> lista = new List<Produto>();

            try
            {
                var resp = from p in conn.Table<Produto>()
                           where p.Nome.ToLower().Contains(titulo.ToLower())
                           select p;
                lista = resp.ToList();
                
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }

        public List<Produto> Localizar(string titulo, Boolean favoritos)
        {
            List<Produto> lista = new List<Produto>();

            try
            {
                var resp = from p in conn.Table<Produto>()
                           where p.Nome.ToLower().Contains(titulo.ToLower())
                           && p.Inativo == favoritos
                           select p;
                lista = resp.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }

        public List<Produto> ListarFavoritos()
        {
            List<Produto> lista = new List<Produto>();

            try
            {
                var resp = from p in conn.Table<Produto>()
                           where p.Inativo == true
                           select p;
                lista = resp.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }

        public Produto GetNota(int id)
        {
            Produto m = new Produto();
            try
            {
                m = conn.Table<Produto>().First(n => n.Id == id);
                StatusMessage = "Encontrou uma nota";
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return m;
        }

        public List<Produto> LocalizarSetores()
        {
            List<Produto> lista = new List<Produto>();

            try
            {
                var resp = from p in conn.Table<Produto>()
                           orderby p.Setor
                           select p;
                lista = resp.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
    }
}
