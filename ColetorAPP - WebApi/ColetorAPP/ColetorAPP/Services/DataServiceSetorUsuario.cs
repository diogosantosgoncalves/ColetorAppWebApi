using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ColetorAPP.Services
{
    public class DataServiceSetorUsuario
    {
        HttpClient httpCliente = new HttpClient();
        public string messagem = "";
        public async Task<List<String>> GetPermissao(String nome)
        {
            try
            {
                string ur = "http://" + Globais.Ip + ":" + Globais.Porta;
                string url = ur + "/api/SetorUsuario/?nome=" + nome;
                //string url = "http://192.168.18.5:3000/api/produtos";
                var response = await httpCliente.GetStringAsync(url);
                var lista_setores = JsonConvert.DeserializeObject<List<String>>(response);
                return lista_setores;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao atualizar Permissao");
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("Erro ao atualizar Permissao");
            }
        }

    }
}
