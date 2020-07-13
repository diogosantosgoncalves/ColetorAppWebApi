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
            //Configuracao configuracao =  dBConfiguracao.Buscar_Config();

            List<Configuracao> list_conf = dBConfiguracao.Buscar();
            //if (list_conf != null)
            if (list_conf.Count != 0)
            {
                foreach (var i in list_conf)
                {
                    id = i.config_id;
                    txt_ip.Text = i.config_ip;
                    txt_porta.Text = i.config_porta;
                    Globais.contagem_ativa = i.contagem_ativa;
                }
                vazia = false;
            }
            else
            {
                vazia = true;
            }
            txt_ip.Focus();
            //if(conf != null){ 
            //    id = conf.config_id;
            //   txt_ip.Text = conf.config_ip;
            //   txt_porta.Text = conf.config_porta;
            //  Globais.contagem_ativa = conf.contagem_ativa;
            //  if (dataServiceProduto.GetTodosProdutos() != null)
            // {
            //      Globais.Ip = txt_ip.Text;
            //      Globais.Porta = txt_porta.Text;
            //      Navigation.PushModalAsync(new PageLogin());
            // }
            ///
            //       }
            //         txt_ip.Focus();
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
                    if(vazia)
                    {
                        configuracao.config_porta = txt_porta.Text;
                        configuracao.config_ip = txt_ip.Text;
                        //configuracao.contagem_ativa = false;
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
                await DisplayAlert("Aviso", "Erro ao conectar no Servidor; " + ex.Message, "ok");
            }
        }
    }
}