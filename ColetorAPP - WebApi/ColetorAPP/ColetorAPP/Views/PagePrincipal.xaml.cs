using ColetorAPP.Models;
using ColetorAPP.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagePrincipal : MasterDetailPage
    {
        DataServiceProduto DataServiceProduto = new DataServiceProduto();
        SQLiteConnection conn;
        public PagePrincipal()
        {
            InitializeComponent();
            //testeDB.Text = App.DbPath;
            Detail = new PageHome();
            bt_home_Clicked(new object(), new EventArgs());
        }
        public PagePrincipal(Plugin.Media.Abstractions.MediaFile Foto)
        {
            InitializeComponent();
            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = Foto.GetStream();
                Foto.Dispose();
                return stream;
            });
            //testeDB.Text = App.DbPath;
            Detail = new PageHome();
            bt_home_Clicked(new object(), new EventArgs());
        }

        private void bt_home_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageHome());
            IsPresented = false;
        }

        private void bt_cadastrar_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageCadastrar());
            IsPresented = false;
        }

        private void bt_listar_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageListar());
            IsPresented = false;
        }
        private async void bt_FecharContagem(object sender, EventArgs e)
        {
            try
            {
                List<Produto> lista_produtos = new List<Produto>();
                Produto produto = new Produto();
                ServicesDBProduto dBProdutos = new ServicesDBProduto(App.DbPath);

                lista_produtos = dBProdutos.Listar();
                foreach (var item in lista_produtos)
                {
                    produto.Id = item.Id;
                    produto.Nome = item.Nome;
                    produto.Setor = item.Setor;
                    produto.Quantidade = item.Quantidade;
                    produto.Inativo = item.Inativo;
                    await DataServiceProduto.Atualiza_Produto(item.Id, produto);

                }
                await DisplayAlert("Atualizado no Servidor: ", lista_produtos.Count.ToString() + " Produtos", "ok");
                Desabilitar_Botoes();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Atualizar","Erro ao atualizar produtos","ok");
                await DisplayAlert("Atualizar", DataServiceProduto.messagem, "ok");
            }


        }
        async private void bt_sobre_Clicked(object sender, EventArgs e)
        {
            
            List<Produto> produtos = new List<Produto>();
            produtos = await DataServiceProduto.GetTodosProdutos();
            ServicesDBProduto dBProdutos = new ServicesDBProduto(App.DbPath);
            foreach (var item in produtos)
            {
                Produto produto = new Produto();
                produto.Id = item.Id;
                produto.Nome = item.Nome;
                produto.Setor = item.Setor;
                produto.Quantidade = 0;
                //produto.Quantidade = item.Quantidade;
                produto.Inativo = item.Inativo;
                dBProdutos.Inserir(produto);
            }

            //servicesDBProduto.Inserir();
            await DisplayAlert("Importação", "Produtos Atualizados produtos", "ok");
            await DisplayAlert("Importação", "Produtos Atualizados: " + produtos.Count.ToString(), "ok");
            Habilitar_Botoes();
            Detail = new NavigationPage(new PageHome());
            IsPresented = false;
        }

        private void bt_scanner_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageScanner());
            IsPresented = false;
        }

        private void bt_exportar_csv_Clicked(object sender, EventArgs e)
        {
            //string a = DependencyService.Get<IPathService>().PrivateExternalFolder;PublicExternalFolder;
            //var downloadDirectory = Path.Combine(DependencyService.Get<IPathService>().PrivateExternalFolder, DependencyService.Get<IPathService>().PrivateExternalFolder2);
            //var logFilePath = Path.Combine(downloadDirectory, "produtos.csv");
            var logFilePath = Path.Combine(DependencyService.Get<IPathService>().PrivateExternalFolder, "produtos.csv");
            conn = new SQLiteConnection(App.DbPath);
            conn.CreateTable<Produto>();
            List<Produto> prod = conn.Table<Produto>().ToList();
            using (StreamWriter sw = File.CreateText(logFilePath))
            {
                foreach (var linha in prod)
                {
                    sw.WriteLine(linha.Nome.ToString() + ";" + linha.Quantidade + ";");
                }
                DisplayAlert("Gravou", "Quantidade de Produtos gravados: " + prod.Count.ToString(), "ok");
            }
        }

        private void bt_ler_csv_Clicked(object sender, EventArgs e)
        {
            //var downloadDirectory = Path.Combine(DependencyService.Get<IPathService>().PublicExternalFolder, DependencyService.Get<IPathService>().PrivateExternalFolder2);
            //var logFilePath = Path.Combine(downloadDirectory, "produtos.csv");
            var logFilePath = Path.Combine(DependencyService.Get<IPathService>().PrivateExternalFolder, "produtos.csv");
            string[] lines = System.IO.File.ReadAllLines(logFilePath);
            foreach (string line in lines)
            {
                //DisplayAlert("Ler Arquivo","\t" + line,"ok");
            }    
            DisplayAlert("Ler Arquivo","Quantidade de Produtos Encontrados: "+lines.Length.ToString(), "ok");
        }

        private void bt_EnviarEmail_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageEmail());
            IsPresented = false;
        }
        private void bt_TirarFoto(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new PageFoto());
            IsPresented = false;
        }

        private async void TirarFoto(object sender, EventArgs e)

        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Demo"
                });
            if (file == null)
                return;
            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        public void Habilitar_Botoes()
        {
            bt_cadastrar.IsEnabled = true;
            bt_Email.IsEnabled = true;
            bt_exportar_csv.IsEnabled = true;
            bt_home.IsEnabled = true;
            bt_ler_csv.IsEnabled = true;
            bt_listar.IsEnabled = true;
            bt_scanner.IsEnabled = true;
            bt_Tirar_foto.IsEnabled = true;
        }
        public void Desabilitar_Botoes()
        {
            bt_cadastrar.IsEnabled = false;
            bt_Email.IsEnabled = false;
            bt_exportar_csv.IsEnabled = false;
            bt_home.IsEnabled = false;
            bt_ler_csv.IsEnabled = false;
            bt_listar.IsEnabled = false;
            bt_scanner.IsEnabled = false;
            bt_Tirar_foto.IsEnabled = false;
        }
    }
}