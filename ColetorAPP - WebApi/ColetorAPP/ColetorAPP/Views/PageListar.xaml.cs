using ColetorAPP.Models;
using ColetorAPP.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            Atualizalista();
        }

        public void Atualizalista()

        {
            String nome = "";
            if(txt_Nota.Text != null)
            {
                nome = txt_Nota.Text;
            }
            swFavorito.IsToggled = true;
            ServicesDBProduto dBNotas = new ServicesDBProduto(App.DbPath);
            
            
            if (swFavorito.IsToggled)
            {
                ListaNotas.ItemsSource = dBNotas.Localizar(nome, false);
            }
            else
            {
                ListaNotas.ItemsSource = dBNotas.Localizar(nome);
            }
        }
        private void ListaNotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto nota = (Produto)ListaNotas.SelectedItem;
            //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            //p.Detail = new NavigationPage(new PageCadastrar(nota));
            Navigation.PushModalAsync(new PageCadastrar(nota));
        }

        private void SwFavorito_Toggled(object sender, ToggledEventArgs e)
        {
                Atualizalista();
        }

        private void bt_Localizar_Clicked(object sender, System.EventArgs e)
        {
            Atualizalista();
        }
    }
}