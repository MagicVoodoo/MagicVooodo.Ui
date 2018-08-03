using System;

using Xamarin.Forms;

namespace MagicVoodoo.Xamarin
{
    [ContentProperty("Content")]
    public class TabView : ContentView
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(TabbedLayout), null);
        
        public String Title
        {
            get => (String)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create("Icon", typeof(FileImageSource), typeof(TabbedLayout), null);

        public FileImageSource Icon
        {
            get => (FileImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public TabView()
        {
        }
    }
}

