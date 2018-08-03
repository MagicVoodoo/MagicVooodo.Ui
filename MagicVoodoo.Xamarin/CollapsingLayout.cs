using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicVoodoo.Xamarin
{
    [ContentProperty("Content")]
    public class CollapsingLayout : StackLayout
    {
        //hide 
        protected new IList<View> Children => base.Children;
        protected new StackOrientation Orientation => base.Orientation;


        public static readonly BindableProperty HeadingProperty =
            BindableProperty.Create("Heading", typeof(View), typeof(CollapsingLayout), null, propertyChanged: HeadingPropertyChanged);

        public View Heading
        {
            get => (View)GetValue(HeadingProperty);
            set => SetValue(HeadingProperty, value);
        }

        static void HeadingPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as CollapsingLayout;
            var oldView = oldValue as View;
            var newView = newValue as View;

            if (newView != null)
            {
                var gesture = new TapGestureRecognizer();
                gesture.Tapped += self.Gesture_Tapped;

                newView.GestureRecognizers.Add(gesture);

                self._Heading = newView;
                self.Layout();
            }
        }

        public static readonly BindableProperty ContentProperty =
            BindableProperty.Create("Content", typeof(View), typeof(CollapsingLayout), null, propertyChanged: ContentPropertyChanged);

        public View Content
        {
            get => (View)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        static void ContentPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as CollapsingLayout;

            self.Layout();
        }


        public static readonly BindableProperty IsCollapsedProperty =
            BindableProperty.Create("IsCollapsed", typeof(bool), typeof(CollapsingLayout), true, propertyChanged: IsCollapsedPropertyChanged);

        public bool IsCollapsed
        {
            get => (bool)GetValue(IsCollapsedProperty);
            set => SetValue(IsCollapsedProperty, value);
        }

        static void IsCollapsedPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as CollapsingLayout;
            self.IsAnimating = true;

            self._Content.BackgroundColor = self._Content?.Content.BackgroundColor ?? self._Content.BackgroundColor;
                
            if (!(bool)newValue)
            {
                self._Content.HeightRequest = (double)ContentView.HeightRequestProperty.DefaultValue;
                self._Content.IsVisible = true;
                self._Content.Content.Opacity = 0;

                var size = self._Content.Measure(self.Width, self.Height).Request;
                var animate = new Animation(d => self._Content.HeightRequest = d, 0, size.Height, Easing.SpringOut);

                animate.Commit(self._Content, "open", length: 100, finished: (arg1, arg2) => self._Content.Content?.FadeTo(1, 50));

            }
            else
            {
                self._Content.Content?.FadeTo(0);
                var animate = new Animation(d => self._Content.HeightRequest = d, self._Content.Height, 0, Easing.SpringIn, () => self._Content.IsVisible = false);
                animate.Commit(self._Content, "collapse", length: 100);
            }
            self.IsAnimating = false;

            self.IsCollapsedChanged?.Invoke(self, (bool)newValue);
        }

        public static readonly BindableProperty IsAnimatingProperty =
            BindableProperty.Create("IsAnimating", typeof(bool), typeof(CollapsingLayout), true, propertyChanged: IsAnimatingPropertyChanged);

        public bool IsAnimating
        {
            get => (bool)GetValue(IsAnimatingProperty);
            set => SetValue(IsAnimatingProperty, value);
        }

        static void IsAnimatingPropertyChanged(BindableObject sender, object oldValue, object newValue)
        {
            var self = sender as CollapsingLayout;
            self?.IsAnimatingChanged?.Invoke(self, (bool)newValue); 
        }


        protected View _Heading =  new Frame();
        protected ContentView _Content = new ContentView();

        public event EventHandler<bool> IsCollapsedChanged;
        public event EventHandler<bool> IsAnimatingChanged;

        public CollapsingLayout()
        {
            Layout();
        }

        void Gesture_Tapped(object sender, EventArgs e)
        {
            IsCollapsed = !IsCollapsed;
        }

        void Layout(){
            base.Children.Clear();
            Children.Add(_Heading);

            _Content.IsVisible = !IsCollapsed;
            _Content.Content = Content;
            Children.Add(_Content);
        }

    }
}

