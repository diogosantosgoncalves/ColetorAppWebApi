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
using ColetorAPP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ColetorAPP.Droid.Service.PathService))]

namespace ColetorAPP.Droid.Service
{
    public class PathService : IPathService
    {
        public string InternalFolder
        {
            get
            {
                return Android.App.Application.Context.FilesDir.AbsolutePath;
            }
        }

        public string PublicExternalFolder
        {
            get
            {
                return Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            }
        }

        public string PrivateExternalFolder
        {
            get
            {
                //return Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
                return Android.App.Application.Context.GetExternalFilesDir(PrivateExternalFolder2).AbsolutePath;
            }
        }
        public string PrivateExternalFolder2
        {
            get
            {
                return Android.OS.Environment.DirectoryDownloads;
            }
        }
    }
}