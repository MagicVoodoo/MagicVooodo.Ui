using System;
using System.Threading;
using Xamarin.Forms;

namespace MagicVoodoo.Xamarin
{
    public class HybridWebView : WebView
    {
        public object Uri { get; set; }

        public event EventHandler<Message> MessageRecieved;

        public virtual void OnMessageRecieved( Message message) => Volatile.Read(ref MessageRecieved)?.Invoke(this, message);

        public virtual void CleanUp() => MessageRecieved = null;

    }
}

