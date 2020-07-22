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

        public void Inserir(Produto produto)
        {
            try
            {
                if (string.IsNullOrEmpty(produto.Nome))
                {
                    throw new Exception("Titulo dos produtos não informado!");
                }
                if (string.IsNullOrEmpty(produto.Setor))
                {
                    throw new Exception("Dados dos produtos não informado!");
                }
                
                
                //if (nota.Qtde > 0)
                //{
                //    throw new Exception("Quantidade de produtos não informado!");
                //}
                
                int result = conn.Insert(produto);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registro(s) adicionado(s):" +
                    " [Produto: {1} ", result, produto.Nome);
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
            try
            {
                var lista = conn.Table<Produto>().Where(c => c.Inativo == false).ToList();
                StatusMessage = "listagem de Produtos";
                return lista;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            //return lista;
        }
        public void Alterar(Produto produto)
        {
            try
            {
                if (string.IsNullOrEmpty(produto.Nome))
                {
                    throw new Exception("Titulo do Produto não informado!");
                }
                if (string.IsNullOrEmpty(produto.Setor))
                {
                    throw new Exception("Dados do Produto não informado!");
                }
                if (produto.Id <= 0)
                {
                    throw new Exception("Id do Produto não informado!");
                }
                int result = conn.Update(produto);
                
                StatusMessage = string.Format("{0} Registro(s) alterado(s)!", result);
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
                StatusMessage = string.Format("{0} Registro(s) deletado(s)!", result);
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
        public int BuscarProdutoPorCodigo(int codigo)
        {
            try
            {
                var resp = from p in conn.Table<Produto>()
                           where p.Id == codigo
                           select p;
                //lista = resp.ToList();
                return resp.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
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

        public Produto GetProduto(int id)
        {
            Produto m = new Produto();
            try
            {
                m = conn.Table<Produto>().First(n => n.Id == id);
                StatusMessage = "Encontrou um Produto";
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
