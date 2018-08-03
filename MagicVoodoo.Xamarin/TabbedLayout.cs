using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using static Xamarin.Forms.Button;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace MagicVoodoo.Xamarin
{
    [ContentProperty("Children")]
    public class TabbedLayout : StackLayout
    {
        public TabView Content
        {
            get => _Content;
            protected set => _Content = value;
        }

        ObservableCollection<TabView> _children = new ObservableCollection<TabView>();
        new public IList<TabView> Children => _children;

        ObservableCollection<TabBarItem> _tabBarItem = new ObservableCollection<TabBarItem>();
        public IList<TabBarItem> TabBarItems => _tabBarItem;


        public static readonly BindableProperty SelectedTabProperty =
            BindableProperty.Create("SelectedTab", typeof(TabView), typeof(TabbedLayout), null, propertyChanged: SelectedTabPropertyChanged);

        public TabView SelectedTab
        {
            get => (TabView)GetValue(SelectedTabProperty);
            set => SetValue(SelectedTabProperty, value);
        }

        static void SelectedTabPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as TabbedLayout;
            self?.SlectedTabChanged?.Invoke(self, newValue as TabView);
        }

        public event EventHandler<TabView> SlectedTabChanged;

        new public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(TabBarOrientations), typeof(TabbedLayout), TabBarOrientations.Top, propertyChanged: LayoutPropertyChanged);


        new public TabBarOrientations Orientation
        {
            get => (TabBarOrientations)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty =
            BindableProperty.Create("SelectedColor", typeof(Color), typeof(TabbedLayout), Color.Accent, propertyChanged: LayoutPropertyChanged);

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedTabProperty, value);
        }

        public static readonly BindableProperty BarBackgroundColorProperty =
            BindableProperty.Create("BarBackgroundColor", typeof(Color), typeof(TabbedLayout), NavigationPage.BarBackgroundColorProperty.DefaultValue, propertyChanged: BarBackgroundColorPropertyChanged);

        public Color BarBackgroundColor
        {
            get => (Color)GetValue(BarBackgroundColorProperty);
            set => SetValue(BarBackgroundColorProperty, value);
        }

        static void BarBackgroundColorPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as TabbedLayout;
            self._TabBar.BackgroundColor = (Color)newValue;
        }

        public static readonly BindableProperty SeperatorColorProperty =
            BindableProperty.Create("SeperatorColor", typeof(Color), typeof(TabbedLayout), Color.Silver, propertyChanged: SeperatorColorPropertyChanged);
        
        public Color SeperatorColor
        {
            get => (Color)GetValue(SeperatorColorProperty);
            set => SetValue(SeperatorColorProperty, value);
        }

        static void SeperatorColorPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as TabbedLayout;
            self._Seperator.Color = (Color)newValue;
        }

        public static readonly BindableProperty SeparatorIsVisibleProperty =
            BindableProperty.Create("SeparatorIsVisible", typeof(bool), typeof(TabbedLayout), true, propertyChanged: SeparatorIsVisiblePropertyChanged);

        public bool SeparatorIsVisible
        {
            get => (bool)GetValue(SeparatorIsVisibleProperty);
            set => SetValue(SeparatorIsVisibleProperty, value);
        }

        static void SeparatorIsVisiblePropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as TabbedLayout;
            self._Seperator.IsVisible = (bool)newValue;
        }

        public static readonly BindableProperty SeparatorThicknessProperty =
            BindableProperty.Create("SeparatorThickness", typeof(double), typeof(TabbedLayout), 0.25, propertyChanged: SeparatorThicknessPropertyChanged);

        public double SeparatorThickness
        {
            get => (double)GetValue(SeparatorThicknessProperty);
            set => SetValue(SeparatorThicknessProperty, value);
        }

        static void SeparatorThicknessPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as TabbedLayout;
            if (self?.Orientation == TabBarOrientations.Top)
            {
                self._Seperator.WidthRequest = (double)BoxView.WidthRequestProperty.DefaultValue;
                self._Seperator.HeightRequest = (double)newValue;
                self._Seperator.VerticalOptions = (LayoutOptions)BoxView.VerticalOptionsProperty.DefaultValue;
                self._Seperator.HorizontalOptions = LayoutOptions.FillAndExpand;
            } 
            else
            {
                self._Seperator.HeightRequest = (double)BoxView.HeightRequestProperty.DefaultValue;
                self._Seperator.WidthRequest = (double)newValue;
                self._Seperator.VerticalOptions = LayoutOptions.FillAndExpand;
                self._Seperator.HorizontalOptions = (LayoutOptions)BoxView.HorizontalOptionsProperty.DefaultValue;
            }
        }


        static void LayoutPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as TabbedLayout;
            self?.LayoutChildren();
        }


        protected StackLayout _TabBar = new StackLayout
        {
            Spacing = 0,
            Padding = new Thickness(0, 0, 5, 0)
        };

        protected BoxView _Seperator = new BoxView
        {
            Color = (Color)SeperatorColorProperty.DefaultValue,
            IsVisible = (bool)SeparatorIsVisibleProperty.DefaultValue,
        };

        protected TabView _Content = new TabView();


        public TabbedLayout()
        {
            _children.CollectionChanged += _children_CollectionChanged;
            _tabBarItem.CollectionChanged += _children_CollectionChanged;

            base.Spacing = 0;

            base.Children.Add(_TabBar);
            base.Children.Add(_Seperator);
        }

        public virtual void SelectTab(TabView tabView)
        {
            if (tabView != null || tabView != SelectedTab)
            {
                SelectedTab = tabView;
                LayoutChildren();
            }
        }

        void HandleTabClicked(object sender)
        {
            SelectTab(sender as TabView);
        }

        void LayoutChildren()
        {
            if (Children?.Count < 1)
                return;

            _TabBar.Children.Clear();

            switch (Orientation)
            {
                case TabBarOrientations.Top:
                    _TabBar.Orientation = StackOrientation.Horizontal;

                    _Seperator.WidthRequest = (double)BoxView.WidthRequestProperty.DefaultValue;
                    _Seperator.HeightRequest = SeparatorThickness;
                    _Seperator.VerticalOptions = (LayoutOptions)BoxView.VerticalOptionsProperty.DefaultValue;
                    _Seperator.HorizontalOptions = LayoutOptions.FillAndExpand;

                    break;

                case TabBarOrientations.Left:
                    _TabBar.Orientation = StackOrientation.Vertical;

                    _Seperator.HeightRequest = (double)BoxView.HeightRequestProperty.DefaultValue;
                    _Seperator.WidthRequest = SeparatorThickness;
                    _Seperator.VerticalOptions = LayoutOptions.FillAndExpand;
                    _Seperator.HorizontalOptions = (LayoutOptions)BoxView.HorizontalOptionsProperty.DefaultValue;
                    break;
            }

            foreach (var child in Children)
            {
                if (SelectedTab == default(TabView))
                    SelectedTab = child;

                var tabView = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.Start
                };

                var tabbutton = new Button
                {
                    Image = child.Icon,
                    Text = child.Title,
                    BackgroundColor = Color.Transparent,
                    CommandParameter = child,
                    Command = new Command(HandleTabClicked, (sender) => SelectedTab != child),
                    ContentLayout = new ButtonContentLayout(ImagePosition.Top, 5)
                };

                switch (Orientation)
                {
                    case TabBarOrientations.Top:
                        base.Orientation = StackOrientation.Vertical;
                        break;

                    case TabBarOrientations.Left:
                        base.Orientation = StackOrientation.Horizontal;
                        break;
                }

                switch (Orientation)
                {
                    case TabBarOrientations.Top:
                        tabView.Children.Add(tabbutton);
                        tabView.Children.Add(new BoxView
                        {
                            Color = (SelectedTab == child) ? SelectedColor : Color.Transparent,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.End,
                            HeightRequest = 5
                        });
                        break;

                    case TabBarOrientations.Left:
                        tabView.Orientation = StackOrientation.Horizontal;
                        tabView.Children.Add(new BoxView
                        {
                            Color = (SelectedTab == child) ? SelectedColor : Color.Transparent,
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            WidthRequest = 5
                        });
                        tabView.Children.Add(tabbutton);
                        break;
                }

                _TabBar.Children.Add(tabView);

                if (SelectedTab == child)
                {
                    if (base.Children.Last() is TabView)
                        base.Children.Remove(base.Children.Last());

                    base.Children.Add(child);
                }
                      

            }
            //SpacerView
            _TabBar.Children.Add( new ContentView { 
                HorizontalOptions = LayoutOptions.FillAndExpand, 
                VerticalOptions = LayoutOptions.FillAndExpand
            });
            foreach(var item in TabBarItems)
                _TabBar.Children.Add(item);
            
        }

        void _children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            LayoutChildren();

    }

}


