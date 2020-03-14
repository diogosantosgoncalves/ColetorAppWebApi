using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;

namespace ColetorAPP.Services
{
    public interface IQrCodeScanningService
    {
        Task<string> ScanAsync();
    }
}
