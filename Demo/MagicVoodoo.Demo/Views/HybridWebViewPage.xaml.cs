using System;
using System.Collections.Generic;
using MagicVoodoo.Xamarin.Services;
using Xamarin.Forms;

namespace MagicVoodoo.Demo.Views
{
    public partial class HybridWebViewPage : ContentPage
    {
        public HybridWebViewPage()
        {
            InitializeComponent();

            var source = new HtmlWebViewSource();
            source.BaseUrl = DependencyService.Get<IBaseUrl>().Get;

            source.Html = @"<html>
<body>
<script src='http://code.jquery.com/jquery-1.11.0.min.js'></script>
<h1>HybridWebView Test</h1>
<br/>
Enter name: <input type='text' id='name'>
<br/>
<br/>
<button type='button' onclick='javascript:invokeCSCode($('#name').val());'>Invoke C# Code</button>
<br/>
<p id='result'>Result:</p>
<script type='text/javascript'>
function log(str)
{
    $('#result').text($('#result').text() + ' ' + str);
}

function sendMessage(topic, content) {
    try {
        log('Sending: { topic: ' + topic + ' content: ' + content '}');
        sendCSharpMessage({'topic':topic, 'content':content});
    }
    catch (err){
          log(err);
    }
}
</script>
</body>
</html>";
        }
    }
}
