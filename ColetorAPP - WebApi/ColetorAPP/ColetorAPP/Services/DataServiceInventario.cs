using ColetorAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ColetorAPP.Services
{
    public class DataServiceInventario
    {
        HttpClient httpClient = new HttpClient();
        public async Task<int> BuscarInventarioAberto()
        {
            int Codigo = 0;
            Inventario inventario1 = new Inventario();
            try
            {
                string ur = "http://" + Globais.Ip + ":" + Globais.Porta;
                string url = ur + "/api/inventario";
                var response = await httpClient.GetStringAsync(url);
                Codigo = JsonConvert.DeserializeObject<int>(response);
                return Codigo;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Erro ao atualizar Produtos");
            }
            catch (ArgumentNullException e)
            {
                throw new Exception("Erro ao atualizar Produtos");
            }
        }
    }
}
