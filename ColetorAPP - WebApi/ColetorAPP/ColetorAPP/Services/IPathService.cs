using System;
using System.Collections.Generic;
using System.Text;

namespace ColetorAPP.Services
{
    public interface IPathService
    {
        string InternalFolder { get; }
        string PublicExternalFolder { get; }
        string PrivateExternalFolder { get; }
        string PrivateExternalFolder2 { get; }
    }
}
