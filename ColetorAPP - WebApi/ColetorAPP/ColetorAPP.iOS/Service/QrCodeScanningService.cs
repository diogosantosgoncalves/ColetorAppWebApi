using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using ColetorAPP.Services;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(ColetorAPP.iOS.Service.QrCodeScanningService))]
namespace ColetorAPP.iOS.Service
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
                BottomText = "Toque na tela para focar"
            };
            var scanResults = await scanner.Scan(optionsCustom);
            return (scanResults != null) ? scanResults.Text : string.Empty;
        }
    }
}