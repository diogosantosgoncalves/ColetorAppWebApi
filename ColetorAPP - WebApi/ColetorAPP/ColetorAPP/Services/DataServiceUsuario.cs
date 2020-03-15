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
            try
            {
                string url = "http://192.168.18.5:3000/api/usuario";
                //var response = await httpClient.GetStringAsync(url);
                var json = JsonConvert.SerializeObject(modelUsuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                //response = await client.PostAsync(uri, content);
                HttpResponseMessage response = null;

                response = await httpClient.PutAsync(url, content);

                //if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    //return lista_produtos;
                    //if (!response.IsSuccessStatusCode)

                    return true;
            }catch(Exception ex)
            {
                throw new Exception();
            }

        } 

    }
}
