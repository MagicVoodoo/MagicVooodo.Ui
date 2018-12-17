using System;

using Xamarin.Forms;
using static Xamarin.Forms.Button;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace MagicVoodoo.Xamarin
{
    [ContentProperty("Content")]
    public class TabView : ContentView
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create("Title", typeof(string), typeof(TabView), null, propertyChanged: TitlePropertyChanged);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        static void TitlePropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self._tabButton.Text = (string)newValue;
        }


        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create("TitleColor", typeof(Color), typeof(TabView), null, propertyChanged: TitleColorPropertyChanged);


        public Color TitleColor
        {
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        static void TitleColorPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self._tabButton.TextColor = (Color)newValue;
        }

        public static readonly BindableProperty TitleSizeProperty =
            BindableProperty.Create("TitleSize", typeof(double), typeof(TabView), null, propertyChanged: TitleSizePropertyChanged);


        public double TitleSize
        {
            get => (double)GetValue(TitleSizeProperty);
            set => SetValue(TitleSizeProperty, value);
        }

        static void TitleSizePropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self._tabButton.FontSize = (double)newValue;
        }


        public static readonly BindableProperty IconProperty =
            BindableProperty.Create("Icon", typeof(FileImageSource), typeof(TabView), null, propertyChanged: IconPropertyChanged);

        public FileImageSource Icon
        {
            get => (FileImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        static void IconPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self._tabButton.Image = (FileImageSource)newValue;
        }


        public static readonly BindableProperty TabProperty =
            BindableProperty.Create("Tab", typeof(StackLayout), typeof(TabView), null);

        public StackLayout Tab
        {
            get => (StackLayout)GetValue(TabProperty);
            private set => SetValue(TabProperty, value);
        }

        public static readonly BindableProperty TabPaddingProperty =
           BindableProperty.Create("TabPadding", typeof(Thickness), typeof(TabView), propertyChanged: TabPaddingPropertyChanged);

        public Thickness TabPadding
        {
            get => (Thickness)GetValue(TabPaddingProperty);
            set => SetValue(TitleProperty, value);
        }

        static void TabPaddingPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self.Tab.Padding = (Thickness)newValue;
        }


        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(TabBarOrientations), typeof(TabView), TabBarOrientations.Default, propertyChanged: LayoutPropertyChanged);


        public TabBarOrientations Orientation
        {
            get => (TabBarOrientations)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        static void LayoutPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self.Layout();
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create("IsSelected", typeof(bool), typeof(TabView), false, propertyChanged: IsSelectedPropertyChanged);

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        static void IsSelectedPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self._highliter.Color = self.IsSelected ? self.SelectedColor : Color.Transparent;
        }

        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create("SelectedColor", typeof(Color), typeof(TabView), Color.Accent, propertyChanged: SelectedColorPropertyChanged);

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        static void SelectedColorPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (!(sender is TabView self))
                return;

            self._highliter.Color = (Color)newValue;
        }

        Button _tabButton = new Button
        {
            BackgroundColor = Color.Transparent,
            BorderColor = Color.Transparent,
           
            ContentLayout = new ButtonContentLayout(ImagePosition.Top, 5),
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand
        };


        BoxView _highliter = new BoxView
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.End,
            HeightRequest = 5
        };


        public event EventHandler Clicked;


        public TabView()
        {
            Tab = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent
            };

            _tabButton.Command = new Command(HandleClickAction, (sender) => !IsSelected);

            Layout();
        }

        protected void Layout()
        {
            Tab.Children.Clear();
            switch (Orientation)
            {
                case TabBarOrientations.Top:
                    Tab.Orientation = StackOrientation.Vertical;

                    Tab.Children.Add(_tabButton);

                    _highliter.Color = IsSelected ? SelectedColor : Color.Transparent;
                    _highliter.HorizontalOptions = LayoutOptions.FillAndExpand;
                    _highliter.VerticalOptions = LayoutOptions.End;
                    _highliter.HeightRequest = 5;

                    Tab.Children.Add(_highliter);
                    break;

                case TabBarOrientations.Left:
                    Tab.Orientation = StackOrientation.Horizontal;

                    _highliter.Color = IsSelected ? SelectedColor : Color.Transparent;
                    _highliter.HorizontalOptions = LayoutOptions.Start;
                    _highliter.VerticalOptions = LayoutOptions.FillAndExpand;
                    _highliter.WidthRequest = 5;

                    Tab.Children.Add(_highliter);
                    Tab.Children.Add(_tabButton);
                    break;

                default:
                    goto case TabBarOrientations.Top;
            }
        }

        void HandleClickAction(object obj) => Clicked?.Invoke(this, new EventArgs());


    }
}

