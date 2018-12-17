using System;
using System.IO;
using Foundation;
using MagicVoodoo.Xamarin;
using Newtonsoft.Json;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(MagicVoodoo.Xamarin.iOS.HybridWebViewRenderer))]
namespace MagicVoodoo.Xamarin.iOS
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler
    {
        
        const string JAVASCRIPT_FUNCTION = "function sendCSharpMessage(topic, content){window.webkit.messageHandlers.invokeAction.postMessage({'topic':topic, 'content':content});}";
        const string MESSAGE_HANDLER_KEY = "sendMessage";

        WKUserContentController userController;

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                userController = new WKUserContentController();
                var script = new WKUserScript(new NSString(JAVASCRIPT_FUNCTION), WKUserScriptInjectionTime.AtDocumentStart, false);
                userController.AddUserScript(script);
                userController.AddScriptMessageHandler(this, MESSAGE_HANDLER_KEY);

                var config = new WKWebViewConfiguration { UserContentController = userController };
                var webView = new WKWebView(Frame, config);
                SetNativeControl(webView);
            }

            if (e.OldElement != null)
            {
                userController.RemoveAllUserScripts();
                userController.RemoveScriptMessageHandler(MESSAGE_HANDLER_KEY);
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.CleanUp();
            }

            if (e.NewElement != null)
            {
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, WEB_ROOT, Element.Uri );
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
            }
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            Element.OnMessageRecieved(JsonConvert.DeserializeObject<Message>(message.Body.ToString()));
        }
    }
}
