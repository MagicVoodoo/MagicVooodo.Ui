using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MagicVoodoo.Demo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            switch(sender){
                case Button self when self.Text =="TabbedLayout":
                        await Navigation.PushAsync(new TabbedLayoutTop { Title = "TabbedLayout" });
                    break;

                case Button self when self.Text == "CollapsingLayout":
                        await Navigation.PushAsync(new CollapsingLayoutPage { Title = "CollapsingLayout" });
                    break;
            }


        }
    }
}
