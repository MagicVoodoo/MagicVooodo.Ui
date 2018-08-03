using System;

using Xamarin.Forms;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace MagicVoodoo.Xamarin
{
    public class TabBarItem : Button
    {
        public TabBarItem()
        {
            BackgroundColor = Color.Transparent;
            ContentLayout = new ButtonContentLayout(ImagePosition.Top, 5);
            HorizontalOptions = LayoutOptions.End;
            VerticalOptions = LayoutOptions.End;
        }
    }
}

