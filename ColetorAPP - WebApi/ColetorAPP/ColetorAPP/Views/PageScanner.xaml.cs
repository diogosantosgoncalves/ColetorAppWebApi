using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColetorAPP.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ColetorAPP.Models;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using ColetorAPP.Views;
using Xamarin.KeyboardHelper;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageScanner : ContentPage
    {
        public string quant;
        int resultado = 1;
        public string codigo_barra;
        List<Produto> lista_produtos;
        public PageScanner()
        {
            InitializeComponent();

            txt_qtde.IsEnabled = false;
            //this.Appearing += MainPage_Appearing;
            //this.Disappearing += MainPage_Disappearing;
            //bt_focus.Clicked += bt_focus_Clicked;
        }
        void bt_focus_Clicked(object sender, EventArgs e)
        {
            txt_qtde.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void MainPage_Disappearing(object sender, EventArgs e)
        {
            SoftKeyboard.Current.VisibilityChanged -= Current_VisibilityChanged;

        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            txt_qtde.Focus();
        }

        private void Current_VisibilityChanged(SoftKeyboardEventArgs e)
        {
            if (e.IsVisible)
            {
                // do your things
            }
            else
            {
                // do your things
            }
        }

        private void button2_clicked(object sender, EventArgs e)
        {
            txt_qtde.Focus();
        }

        private async void bt_ScannerAutomatico(object sender, EventArgs e) {
            //=> 
            while (resultado == 1)
            {
                await ScannerAutomatico();
            }
            //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            //p.Detail = new NavigationPage(new PageHome());
            await Navigation.PushModalAsync(new PageScanner()); 
        }
        private async Task ScannerAutomatico()
        {
            int quant = 0;
            var scanner = DependencyService.Get<IQrCodeScanningService>();
            var result = await scanner.ScanAsync();
            try
            {
                if (!string.IsNullOrEmpty(result))
                {
                    resultado = 1;                    
                    var QrCode = result;                    
                    Vibration.Vibrate();
                    var duration = TimeSpan.FromSeconds(1);
                    Vibration.Vibrate(duration);
                    ServicesDBProduto dBNotas = new ServicesDBProduto(App.DbPath);
                    Produto nota = new Produto();
                    nota.Nome = QrCode.ToString();
                    nota.Setor = "teste";
                    //nota.Quantidade = 1;
                    //nota.Favorito = swFavorito.IsToggled;

                    lista_produtos = dBNotas.Localizar(nota.Nome);
                    if (lista_produtos.Count == 0)
                    {
                        await DisplayAlert("Aviso!", "Produto não encontrado no Banco de Dados", "ok");
                        return;
                    }

                    foreach (var prod in lista_produtos)
                    {
                        nota.Id = prod.Id;
                        nota.Setor = prod.Setor;
                    }

                    foreach (var set in Globais.Setores)
                    {
                        if (set.ToString() != nota.Setor)
                        {
                            await DisplayAlert("Aviso!", "Sem permissão para contar esse produto", "ok");
                            return;
                        }
                    }

                    quant = dBNotas.Atualizar_Quantidade(nota.Nome);
                    nota.Quantidade = quant + 1;
                    dBNotas.Alterar(nota);
                    //dBNotas.Inserir(nota);
                    DependencyService.Get<IMessage>().ShortAlert("Código: " + QrCode.ToString() + " \n Quantidade adicionada: 1");
               
                }
                else
                {
                    resultado = 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        private async Task ScannerManual()
        {
            
            var scanner = DependencyService.Get<IQrCodeScanningService>();
            var result = await scanner.ScanAsync();
            try
            {
                if (!string.IsNullOrEmpty(result))
                {
                    resultado = 1;
                    var QrCode = result;
                    codigo_barra = QrCode;

                    Vibration.Vibrate();
                    var duration = TimeSpan.FromSeconds(1);
                    Vibration.Vibrate(duration);
                }
                else
                {
                    //await Navigation.PushModalAsync(new PageScanner());
                    resultado = 0;
                    txt_qtde.Focus();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            txt_qtde.Focus();
        }

        private async void bt_ScannerManual(object sender, EventArgs e)
        {
            await ScannerManual();
            ServicesDBProduto dBNotas = new ServicesDBProduto(App.DbPath);
            lista_produtos = dBNotas.Localizar(codigo_barra);
            if (lista_produtos.Count == 0)
            {
                await DisplayAlert("Aviso!", "Produto não encontrado no Banco de Dados", "ok");
                return;
            }
            List<Produto> lista = new List<Produto>();
            //bt_focus.Clicked += bt_focus_Clicked;
            this.Appearing += MainPage_Appearing;
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            txt_qtde.Focus();
            txt_qtde.IsEnabled = true;

            ServicesDBProduto dBNotas1 = new ServicesDBProduto(App.DbPath);
            lista = dBNotas1.Localizar(codigo_barra);
            if(lista == null)
            {
                await DisplayAlert("Aviso!", "Produto não encontrado no Banco de Dados", "ok");
                return;
            }
            string setor = "";
            foreach(var i in lista)
            {
                setor = i.Setor;
            }

            if (!string.IsNullOrEmpty(setor))
            {
                //if(Globais.Setores.IndexOf(setor) == -1)
                //{
                foreach(var set in Globais.Setores)
                {
                    if(set.ToString() != setor)
                    {
                        await DisplayAlert("Aviso!", "Sem permissão para contar esse produto", "ok");
                    }
                }

                //}
                base.OnAppearing();
                this.Appearing += MainPage_Appearing;
                this.Disappearing += MainPage_Disappearing;
                bt_gravarbanco.IsVisible = true;
                txt_qtde.Focus();
                txt_qtde.IsEnabled = true;
                bt_gravarbanco.IsEnabled = true;
                this.Appearing += MainPage_Appearing;
                SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
                txt_qtde.Focus();
                //bt_focus.Clicked += bt_focus_Clicked;


            }
            this.Appearing += MainPage_Appearing;
            SoftKeyboard.Current.VisibilityChanged += Current_VisibilityChanged;
            txt_qtde.Focus();
            //bt_focus.Clicked += bt_focus_Clicked;
        }
        private void Gravar_banco(object sender, EventArgs e)
        {
            txt_qtde.IsEnabled = true;
            bt_gravarbanco.IsEnabled = true;
            int quant = 0;
            Produto nota = new Produto();
            nota.Nome = codigo_barra;
            nota.Setor = "teste";
            nota.Quantidade = Int32.Parse(txt_qtde.Text);
            ServicesDBProduto dBNotas = new ServicesDBProduto(App.DbPath);
            ///////////////////////////////////////////////////////////
            lista_produtos = dBNotas.Localizar(nota.Nome);
            foreach (var prod in lista_produtos)
            {
                nota.Id = prod.Id;
                nota.Setor = prod.Setor;
            }

            quant = dBNotas.Atualizar_Quantidade(nota.Nome);
            nota.Quantidade = quant + Int32.Parse(txt_qtde.Text); 
            dBNotas.Alterar(nota);
            /////////////////////////////////////////////////////////////////
            //dBNotas.Inserir(nota);
            DependencyService.Get<IMessage>().ShortAlert("Código: " + codigo_barra + " \n Quantidade adicionada: " + txt_qtde.Text);
            txt_qtde.Text = "";
            txt_qtde.IsEnabled = false;
            bt_gravarbanco.IsEnabled = false;
        }
        private void bt_home_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new PageHome());
            Navigation.PopModalAsync();
            //Navigation.PushModalAsync(new PagePrincipal());

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PagePrincipal());
        }
    }
}