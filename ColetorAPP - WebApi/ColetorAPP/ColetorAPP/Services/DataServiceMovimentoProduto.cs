using ColetorAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ColetorAPP.Services
{
    public class DataServiceMovimentoProduto
    {
        HttpClient httpCliente = new HttpClient();
        public async Task<bool> SalvarMovimentoProduto(List<MovimentoProduto> lista_mv)
        {
            try
            {
                string ur = "http://" + Globais.Ip + ":" + Globais.Porta;
                string url = ur + "/api/movimentoproduto";
                //string url = "http:192.168.18.5:3000/api/produtos/{0}";
                //var uri = new Uri(string.Format(url, id));

                //string url = "http://192.168.18.5:3000/api/produtos";
                var data = JsonConvert.SerializeObject(lista_mv);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await httpCliente.PostAsync(url, content);

                if (response.IsSuccessStatusCode == true)
                {
                    return true;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar MovimentoProdutos");
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na Atualização dos MovimentoProdutos");
                //messagem = ex.Message;
            }
        }
    }
}
