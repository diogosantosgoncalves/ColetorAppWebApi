using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using ZXing.Mobile;
using ColetorAPP.Services;
using System.Threading.Tasks;

[assembly: Dependency(typeof(ColetorAPP.Droid.Service.QrCodeScanningService))]
namespace ColetorAPP.Droid.Service
{
    public class QrCodeScanningService : IQrCodeScanningService
    {
        public async Task<string> ScanAsync()

        {

            var optionsCustom = new MobileBarcodeScanningOptions()
            {
                //UseFrontCameraIfAvailable = true
            };
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Aproxime a câmera do código de barra",
                CancelButtonText = "Voltar",
                FlashButtonText = "Ativar Flash",
                BottomText = "Toque na tela para focar"
            };
            //scanner.Cancel();// = "Cancelar";
            scanner.CancelButtonText = "Testando botão voltar";
            var scanResults = await scanner.Scan(optionsCustom);
            return (scanResults != null) ? scanResults.Text : string.Empty;
        }
    }
}