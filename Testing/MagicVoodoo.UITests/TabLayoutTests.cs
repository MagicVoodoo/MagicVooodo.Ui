using NUnit.Framework;
using System;
using Xamarin.UITest;

namespace MagicVoodoo.XamarinTests
{

    public class TabLayoutTests : TestBase
    {
        public TabLayoutTests(Platform platform) : base(platform)
        {
        }



        [Test()]
        public void TestCase()
        {
            app.WaitForElement("TabedLayoutButton");
            app.Tap("TabedLayoutButton");

            app.Repl();
        }
    }
}
