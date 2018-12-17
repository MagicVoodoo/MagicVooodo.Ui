using System;
using Foundation;
using MagicVoodoo.Xamarin.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(MagicVoodoo.Xamarin.iOS.Services.BaseUrl))]
namespace MagicVoodoo.Xamarin.iOS.Services
{
    public class BaseUrl : IBaseUrl
    {
        public string Get => NSBundle.MainBundle.BundlePath;
    }
}
