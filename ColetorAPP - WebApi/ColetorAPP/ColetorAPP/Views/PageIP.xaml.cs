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

        public PageIP()
        {
            InitializeComponent();
            Configuracao configuracao = new Configuracao();
            Configuracao configuracao_inicial = new Configuracao();
            List<Configuracao> lista_conf = new List<Configuracao>();
            lista_conf= dBConfiguracao.Buscar();
            if(lista_conf != null)
            {
                foreach(var i in lista_conf)
                {
                    txt_ip.Text = i.config_ip;
                    txt_porta.Text = i.config_porta;
                }
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
                        configuracao.config_porta = txt_porta.Text;
                        configuracao.config_ip = txt_ip.Text;
                        dBConfiguracao.Inserir(configuracao);
                    await Navigation.PushModalAsync(new PageLogin());
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Aviso", "Endereço IP ou Porta Inválida", "ok");
            }
        }
    }
}