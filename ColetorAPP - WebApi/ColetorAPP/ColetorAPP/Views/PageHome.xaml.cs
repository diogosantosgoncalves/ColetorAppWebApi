
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ColetorAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHome : ContentPage
    {
        public PageHome()
        {
            InitializeComponent();
        }
    private void tap_IniciarScanner(object sender, System.EventArgs e)
        {
            //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            //p.Detail = new NavigationPage(new PageScanner());
            //p.IsPresented = false;
            if(Globais.contagem_ativa == false)
            {
                DisplayAlert("Aviso","A Contagem precisa estar Ativa!","ok");
                return;
            }
            Navigation.PushModalAsync(new PageScanner());
        }

        private void tap_LocalizarProduto(object sender, System.EventArgs e)
        {
            //MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            //p.Detail = new NavigationPage(new PageListar());
            //p.IsPresented = false;
            Navigation.PushModalAsync(new PageListar());
        }
    }
}