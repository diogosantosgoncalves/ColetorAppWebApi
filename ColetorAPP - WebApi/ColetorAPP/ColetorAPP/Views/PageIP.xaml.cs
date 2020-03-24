using ColetorAPP.Models;
using ColetorAPP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageIP : ContentPage
    {
        DataServiceProduto dataServiceProduto = new DataServiceProduto();
        ServicesDBConfiguracao dBConfiguracao = new ServicesDBConfiguracao(App.DbPath);
        bool vazia = true;
        int id = 0;
        public PageIP()
        {
            InitializeComponent();
            List<Configuracao> lista_conf = new List<Configuracao>();
            lista_conf= dBConfiguracao.Buscar();
            if(lista_conf != null)
            {
                foreach(var i in lista_conf)
                {
                    id = i.config_id;
                    txt_ip.Text = i.config_ip;
                    txt_porta.Text = i.config_porta;
                }
                vazia = false;
            }
            else
            {
                vazia = true;
            }

            txt_ip.Focus();
        }

        private async void Conectar_Servidor(object sender, EventArgs e)
        {
            Configuracao configuracao = new Configuracao();
            try
            {
                Globais.Ip = txt_ip.Text;
                Globais.Porta = txt_porta.Text;
                if (await dataServiceProduto.GetTodosProdutos() == null)
                {
                    await DisplayAlert("Aviso", "Endereço IP ou Porta Inválida", "ok");
                }
                else
                {
                    if(vazia == true)
                    {
                        configuracao.config_porta = txt_porta.Text;
                        configuracao.config_ip = txt_ip.Text;
                        dBConfiguracao.Inserir(configuracao);
                        await Navigation.PushModalAsync(new PageLogin());
                    }
                    else
                    {
                        configuracao.config_id = id;
                        configuracao.config_porta = txt_porta.Text;
                        configuracao.config_ip = txt_ip.Text;
                        dBConfiguracao.Alterar(configuracao);
                        await Navigation.PushModalAsync(new PageLogin());
                    }
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Aviso", "Erro ao conectar no Servidor", "ok");
            }
        }
    }
}