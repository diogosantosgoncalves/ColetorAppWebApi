﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using ColetorAPP.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace ColetorAPP.Services
{
    public class DataServiceProduto
    {
        HttpClient httpCliente = new HttpClient();
        public string messagem = "";

        public async Task<List<Produto>> GetTodosProdutos()
        {
            try
            {
                string ur = "http://" + Globais.Ip + ":" + Globais.Porta;
                string url = ur + "/api/produtos";
            //string url = "http://192.168.18.5:3000/api/produtos";
                var response = await httpCliente.GetStringAsync(url);
                var lista_produtos = JsonConvert.DeserializeObject<List<Produto>>(response);
                return lista_produtos;
            }
            catch(HttpRequestException ex)
            {
                throw new Exception("Erro ao atualizar Produtos");
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("Erro ao atualizar Produtos");
            }
        }

        public async Task<bool> Atualiza_Produto(int id, Produto produto)
        {
            try
            {
                string ur = "http://" + Globais.Ip + ":" + Globais.Porta;
                string url = ur + "/api/produtos";
                //string url = "http://192.168.18.5:3000/api/produtos";
                var data = JsonConvert.SerializeObject(produto);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await httpCliente.PutAsync(url, content);

                if(response.IsSuccessStatusCode == true)
                {
                    return true;
                }
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar Produtos");
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Atualização dos Produto");
                //messagem = ex.Message;
            }
        }
    }
}
