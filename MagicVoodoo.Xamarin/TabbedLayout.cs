using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;
using static Xamarin.Forms.Button;
using static Xamarin.Forms.Button.ButtonContentLayout;

namespace MagicVoodoo.Xamarin
{
    [ContentProperty("Children")]
    public class TabbedLayout : StackLayout
    {
        ContentView _Content = new ContentView
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.FillAndExpand,
        };
        public TabView Content
        {
            get => _Content.Content as TabView;
            protected set => _Content.Content = value;
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
            self.SelectTab(newValue as TabView);
        }

        public event EventHandler<TabView> SlectedTabChanged;

        new public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(TabBarOrientations), typeof(TabbedLayout), TabBarOrientations.Default);

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
            set => SetValue(SelectedColorProperty, value);
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

            switch (self?.Orientation)
            {
                case TabBarOrientations.Top:
                    self._Seperator.WidthRequest = (double)BoxView.WidthRequestProperty.DefaultValue;
                    self._Seperator.HeightRequest = (double)newValue;
                    self._Seperator.VerticalOptions = (LayoutOptions)BoxView.VerticalOptionsProperty.DefaultValue;
                    self._Seperator.HorizontalOptions = LayoutOptions.FillAndExpand;
                    break;

                case TabBarOrientations.Left:
                    self._Seperator.HeightRequest = (double)BoxView.HeightRequestProperty.DefaultValue;
                    self._Seperator.WidthRequest = (double)newValue;
                    self._Seperator.VerticalOptions = LayoutOptions.FillAndExpand;
                    self._Seperator.HorizontalOptions = (LayoutOptions)BoxView.HorizontalOptionsProperty.DefaultValue;
                    break;

                default:
                    goto case TabBarOrientations.Top;
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


        public TabbedLayout()
        {
            //CompressedLayout.SetIsHeadless(_Content, true);

            _children.CollectionChanged += _children_CollectionChanged;
            _tabBarItem.CollectionChanged += _tabBarItems_CollectionChanged;

            base.Spacing = 0;

            LayoutChildren();

        }

        public virtual void SelectTab(TabView tabView)
        {
            if (tabView == SelectedTab)
                return;


            SelectedTab = tabView;
            foreach (var child in Children)
                child.IsSelected = child == SelectedTab;
            
            Content = SelectedTab;
            SlectedTabChanged?.Invoke(this, SelectedTab);

            //_Content.ForceLayout();
        }

        void Tab_Clicked(object sender, EventArgs e) => SelectTab(sender as TabView);


        void LayoutChildren()
        {
            if (Children?.Count < 1 && TabBarItems?.Count < 1)
                return;

            base.Children.Clear();
            switch (Orientation)
            {
                case TabBarOrientations.Top:
                    base.Children.Add(_TabBar);
                    base.Children.Add(_Seperator);
                    base.Children.Add(_Content);
                    break;

                case TabBarOrientations.Left:
                    base.Children.Add(_TabBar);
                    base.Children.Add(_Seperator);
                    base.Children.Add(_Content);
                    break;

                default:
                    goto case TabBarOrientations.Top;
            }



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

                default:
                    goto case TabBarOrientations.Top;
            }

            _TabBar.Children.Clear();
            foreach (var child in Children)
            {
                if (SelectedTab == default(TabView))
                    SelectedTab = child;

                _TabBar.Children.Add(child.Tab);
            }

            //SpacerView
            _TabBar.Children.Add(new ContentView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            });

            foreach (var item in TabBarItems)
                _TabBar.Children.Add(item);

            Content = SelectedTab;
        }

        void _children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        var tab = item as TabView;
                        if (tab == null)
                            break;
                        tab.Clicked += Tab_Clicked;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        var tab = item as TabView;
                        if (tab == null)
                            break;
                        tab.Clicked -= Tab_Clicked;
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    //New tabs
                    foreach (var item in e.NewItems)
                    {
                        var tab = item as TabView;
                        if (tab == null)
                            break;
                        tab.Clicked += Tab_Clicked;
                    }

                    //OldTabs
                    foreach (var item in e.OldItems)
                    {
                        var tab = item as TabView;
                        if (tab == null)
                            break;
                        tab.Clicked -= Tab_Clicked;
                    }
                    break;
            }

            LayoutChildren();
        }

        void _tabBarItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            LayoutChildren();


    }

}


