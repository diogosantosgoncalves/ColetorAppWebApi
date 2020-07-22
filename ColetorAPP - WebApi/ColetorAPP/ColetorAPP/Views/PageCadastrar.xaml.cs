using ColetorAPP.Models;
using ColetorAPP.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        ServicesDBProduto dBNotas = new ServicesDBProduto(App.DbPath);
        public PageCadastrar()
        {
            InitializeComponent();
        }
        public PageCadastrar(Produto nota)
        {
            InitializeComponent();

            bt_Salvar.Text = "Alterar";
            txt_Codigo.IsVisible = true;
            bt_Excluir.IsVisible = true;
            txt_Qtde.IsVisible = true;

            txt_Codigo.Text = nota.Id.ToString();
            txt_Titulo.Text = nota.Nome;
            txt_Dados.Text = nota.Setor;
            txt_Qtde.Text = nota.Quantidade.ToString();
            swFavorito.IsToggled = nota.Inativo;

        }

        private void bt_Salvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Produto nota = new Produto();
                nota.Nome = txt_Titulo.Text;
                nota.Setor = txt_Dados.Text;
                nota.Quantidade = Convert.ToInt32(txt_Qtde.Text.ToString());
                nota.Inativo = swFavorito.IsToggled;


                if(txt_Titulo.Text == null)
                {
                    DisplayAlert("Digite um Texto", "Digite um Título!", "OK!");
                    return;
                }

                if (txt_Dados.Text == null)
                {
                    DisplayAlert("Digite um Texto", "Digite um Dado da Nota!", "OK!");
                    return;
                }

                if (bt_Salvar.Text == "Inserir")
                {
                    dBNotas.Inserir(nota);
                    DisplayAlert("Resultado da Operação: ", dBNotas.StatusMessage, "OK");
                }
                else
                {// alterar
                    nota.Id = Convert.ToInt32(txt_Codigo.Text.ToString());
                    dBNotas.Alterar(nota);
                    DisplayAlert("Resultado da Operação: ", dBNotas.StatusMessage, "OK");
                }
                //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                //Detail = new NavigationPage(new PageHome());

                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        private void bt_Cancelar_Clicked(object sender, EventArgs e)
        {
            //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            //p.Detail = new PagePrincipal();
            Navigation.PushModalAsync(new PagePrincipal());
        }

        private async void bt_Excluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir Registro?", "Deseja Excluir a Nota?", "Sim", "Não");
            if (resp == true)
            {
                ServicesDBProduto dBNotas = new ServicesDBProduto(App.DbPath);
                int id = Convert.ToInt32(txt_Codigo.Text);
                dBNotas.Excluir(id);
                await DisplayAlert("Resultado da Operação: ", dBNotas.StatusMessage, "OK");

                //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                //p.Detail = new NavigationPage(new PageHome());
                await Navigation.PushModalAsync(new PageHome());
            }
        }
    }
}