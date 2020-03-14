using ColetorAPP.Models;
using ColetorAPP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSetor : ContentPage
    {
        //ModelUsuario usu1;
        public PageSetor()
        {
            InitializeComponent();
        }

        private void Setor_Padrao(object sender, EventArgs e)
        {   
            Navigation.PushModalAsync(new PagePrincipal());
        }

        private void Setor_Produtos(object sender, EventArgs e)
        {
            bt_setor_padrao.IsEnabled = false;
            lbl_setor.IsVisible = true;
            bt_setores_produtos.IsVisible = true;
        }

        private void Ler_CSV(object sender, EventArgs e)
        {
            ListaSetores.IsVisible = true;
            ServicesDBProduto dBNotas = new ServicesDBProduto(App.DbPath);
            ListaSetores.ItemsSource = dBNotas.LocalizarSetores();
            var logFilePath = Path.Combine(DependencyService.Get<IPathService>().PrivateExternalFolder, "produtos.csv");
            if (System.IO.File.Exists(logFilePath))
            {
                string[] lines = System.IO.File.ReadAllLines(logFilePath);
                foreach (string line in lines)
                {
                    //DisplayAlert("Ler Arquivo","\t" + line,"ok");
                }
                //DisplayAlert("Ler Arquivo", "Quantidade de Setores Encontrados: " + lines.Length.ToString(), "ok");
                bt_confirmar_setor.IsVisible = true;
            }
            else
            {
                DisplayAlert("Ler Arquivo", "Arquivo não Encontrado!" , "ok");
                bt_setor_padrao.IsEnabled = true;
            }
        }

        private void Bt_confirmar_setor_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Item!", ListaSetores.SelectedItem.ToString(),"ok");
            Navigation.PushModalAsync(new PagePrincipal());
        }
    }
}