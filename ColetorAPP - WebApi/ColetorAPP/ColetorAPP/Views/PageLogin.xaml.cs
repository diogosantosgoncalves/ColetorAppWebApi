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
    //public partial class PageLogin : MasterDetailPage
    public partial class PageLogin : ContentPage
    {
        DataServiceUsuario dataServiceUsuario = new DataServiceUsuario();
        public PageLogin()
        {
            InitializeComponent();
            
            //MainPage = new NavigationPage(new PagePrincipal());
            //Detail = new PageHome();
        }
        private async void Verificar_Usuario(object sender, EventArgs e)
        {
            bool achou = false;
            ModelUsuario usu = new ModelUsuario();
            usu.Nome = txt_login.Text;
            usu.Senha = txt_senha.Text;
            if (string.IsNullOrEmpty(usu.Nome))
            {
                await DisplayAlert("Aviso!", "Digite um nome de Usuário!", "ok");
                return;
            }
            if (string.IsNullOrEmpty(usu.Senha))
            {
                await DisplayAlert("Aviso!", "Digite uma Senha de Usuário!", "ok");
                return;
            }
            ServicesDBUsuario dbUsu = new ServicesDBUsuario(App.DbPath);


            achou = await dataServiceUsuario.GetBuscar_Usuario(usu);
            //bool acho1u= DataServiceUsuario.GetBuscar_Usuario(usu);
            if(achou == true)
            {
                await DisplayAlert("Aviso!", "Bem Vindo ao Aplicativo!", "ok");
                await Navigation.PushModalAsync(new PageSetor());
            }
            else
            {
                await DisplayAlert("Aviso!", "Usuário ou Senha Incorreto!", "ok");
                return;
            }
            /*/
            int lista = dbUsu.LocalizarUsuario(usu);
            if (lista == 0)
            {
                
                await DisplayAlert("Aviso!", "Usuário ou Senha Incorreto!", "ok");
                return;
                //Navigation.PushModalAsync(new PagePrincipal());
            }
            else
            {
                DisplayAlert("Aviso!", "Bem Vindo ao Aplicativo!", "ok");
                Navigation.PushModalAsync(new PageSetor());
            }
            */
            //Navigation.PushModalAsync(new PagePrincipal());
            //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            //p.Detail = new NavigationPage(new PageScanner());
            //MainPage = new PageLogin();

        }
        private void cadastrar_Usuario(object sender, EventArgs e)
        {
            ModelUsuario usu = new ModelUsuario();
            usu.Nome = txt_login.Text;
            usu.Senha = txt_senha.Text;
            if (string.IsNullOrEmpty(usu.Nome))
            {
                DisplayAlert("Aviso!", "Digite um nome de Usuário!", "ok");
                return;
            }
            if (string.IsNullOrEmpty(usu.Senha))
            {
                DisplayAlert("Aviso!", "Digite uma Senha de Usuário!", "ok");
                return;
            }
            ServicesDBUsuario dbUsu = new ServicesDBUsuario(App.DbPath);
            //List<ModelUsuario> lista = new List<ModelUsuario>();
            int lista = dbUsu.LocalizarUsuario(usu);
            if (lista == 0)
            {
                dbUsu.Inserir(usu);
                DisplayAlert("Aviso!", "Usuário Cadastrado!", "ok");
                DisplayAlert("Aviso!", "Bem Vindo ao Aplicativo!", "ok");
                Navigation.PushModalAsync(new PageSetor());
            }
            else
            {
                Navigation.PushModalAsync(new PageSetor());
                DisplayAlert("Aviso!", "Usuário já cadastrado!", "ok");
            }
        }
    }
}