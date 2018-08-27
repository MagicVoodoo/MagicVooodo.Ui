# MagicVooodo.Ui



## Tabbed Layout

~~~ xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
     xmlns:ui="clr-namespace:MagicVoodoo.Xamarin;assembly=MagicVoodoo.Xamarin" x:Class="MagicVoodoo.Views.TabbedLayoutTop">
    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Left" Order="Primary" Priority="0" Clicked="Handle_Clicked" />
    </ContentPage.ToolbarItems>
    <ContentPage.Content>
        <ui:TabbedLayout x:Name="mainLayout">
            <ui:TabbedLayout.TabBarItems>
                <ui:TabBarItem Text="Silver" Clicked="Handle_Clicked" />
            </ui:TabbedLayout.TabBarItems>
            <ui:TabView Title="Tab 1" Icon="Icon.png">
                <StackLayout>
                    <Label Text="This is tab 1" />
                </StackLayout>
            </ui:TabView>
            <ui:TabView Title="Tab 2" Icon="Icon.png">
                <StackLayout>
                    <Label Text="This is tab 2" />
                </StackLayout>
            </ui:TabView>
            <ui:TabView Title="Tab 3" Icon="Icon.png">
                <StackLayout>
                    <Label Text="This is tab 3" />
                </StackLayout>
            </ui:TabView>
        </ui:TabbedLayout>
    </ContentPage.Content>
</ContentPage>

~~~

## Colapsing Layout

~~~ xaml
<?xml version="1.0" encoding="UTF-8"?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:ui="clr-namespace:MagicVoodoo.Xamarin;assembly=MagicVoodoo.Xamarin"
     x:Class="MagicVoodoo.Views.CollapsingLayoutPage">
    <ContentPage.Content>
        <ui:CollapsingLayout>
            <ui:CollapsingLayout.Heading>
                <Frame BackgroundColor="Silver">
                    <Label Text="Header" HorizontalOptions="CenterAndExpand"/>
                </Frame>
            </ui:CollapsingLayout.Heading>
            <StackLayout BackgroundColor="SeaShell">
                <Label Text="Donec id ultricies est. Maecenas rutrum placerat varius. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vestibulum finibus metus sit amet convallis porta. Etiam porttitor leo at ligula porta, ac egestas risus mollis. Fusce sed mi vel mauris sodales tristique sed eget velit. Nulla nisl libero, semper id rhoncus in, auctor et quam. Mauris turpis tellus, condimentum ut nisi sit amet, cursus placerat nisi. Sed rutrum metus tortor, dignissim ultricies lectus imperdiet sed. In cursus, nisi non porttitor ornare, nibh leo vestibulum quam, eu imperdiet neque quam ut sapien." HorizontalOptions="CenterAndExpand" VerticalOptions="CenterAndExpand"/>
            </StackLayout>
        </ui:CollapsingLayout>
    </ContentPage.Content>
</ContentPage>
~~~