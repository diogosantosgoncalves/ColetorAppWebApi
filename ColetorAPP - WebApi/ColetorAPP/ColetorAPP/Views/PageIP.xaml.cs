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
        public PageIP()
        {
            InitializeComponent();
        }

        private async void Conectar_Servidor(object sender, EventArgs e)
        {
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