﻿using ColetorAPP.Models;
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

        public async Task<bool> GetBuscar_Usuario(Usuario modelUsuario)
        {
            try
            {
                string ur = "http://" + Globais.Ip + ":" + Globais.Porta;
                string url = ur + "/api/usuario";

                //string url = "http://192.168.18.5:3000/api/usuario";
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
            }catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }

        }

        public async Task<bool> GetLogin(String nome,String senha)
        {
            try
            {
                string ur = "http://" + Globais.Ip + ":" + Globais.Porta;
                string url = ur + "/api/usuario/?nome=" + nome +"&&senha=" + senha;

                //string url = "http://192.168.18.5:3000/api/usuario";
                var response = await httpClient.GetStringAsync(url);
                var resposta = JsonConvert.DeserializeObject<bool>(response);
                return resposta;

            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException();
            }

        }

    }
}
