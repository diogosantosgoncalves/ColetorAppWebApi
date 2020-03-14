using ColetorAPP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ColetorAPP.Services
{
    public class DataServiceUsuario
    {
        HttpClient httpClient = new HttpClient();

        public async Task<bool> GetBuscar_Usuario(ModelUsuario modelUsuario)
        {
            string url = "http://192.168.18.5:3000/api/usuario";
            var response = await httpClient.GetStringAsync(url);
            var lista_produtos = JsonConvert.DeserializeObject<List<ModelUsuario>>(response);
           
            //return lista_produtos;
            //if (!response.IsSuccessStatusCode)
            if(lista_produtos == null)
            {
                //throw new Exception("Erro ao atualizar Produtos");
                return false;
            }
            return true;
        } 

    }
}
