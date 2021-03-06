﻿using System;
using System.Collections.Generic;
using MagicVoodoo.Xamarin;
using Xamarin.Forms;

namespace MagicVoodoo.Views
{
    public partial class TabbedLayoutTop : ContentPage
    {
        public TabbedLayoutTop()
        {
            InitializeComponent();
        }

        public void Handle_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                switch (sender)
                {
                    case ToolbarItem self when self.Text == "Default":
                        mainLayout.Orientation = TabBarOrientations.Default;
                        self.Text = "Left";
                        break;

                    case ToolbarItem self when self.Text == "Left":
                        mainLayout.Orientation = TabBarOrientations.Left;
                        self.Text = "Default";
                        break;

                    case TabBarItem self when self.Text == "Default":
                        mainLayout.BarBackgroundColor = (Color)TabbedLayout.BarBackgroundColorProperty.DefaultValue;
                        self.Text = "Silver";
                        break;

                    case TabBarItem self when self.Text == "Silver":
                        mainLayout.BarBackgroundColor = Color.Silver;
                        self.Text = "Default";
                        break;
                }
            });

        }

    }
}
