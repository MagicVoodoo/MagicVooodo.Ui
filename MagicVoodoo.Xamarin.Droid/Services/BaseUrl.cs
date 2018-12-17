using System;
using MagicVoodoo.Xamarin.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(MagicVoodoo.Xamarin.Droid.Services.BaseUrl))]
namespace MagicVoodoo.Xamarin.Droid.Services
{
    public class BaseUrl : IBaseUrl
    {
        string IBaseUrl.Get => "file:///android_asset/";
    }
}
