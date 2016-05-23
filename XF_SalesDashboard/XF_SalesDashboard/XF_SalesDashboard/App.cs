using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Xamarin.Forms;

namespace XF_SalesDashboard
{
    public class App : Application
    {
        public App()
        {
            var nav = new NavigationPage(new Views.AreaPage());
            nav.BarBackgroundColor = Color.FromHex("3498DB");
            nav.BarTextColor = Color.White;
            MainPage = nav;
        }

        protected override async void OnStart()
        {
            System.Diagnostics.Debug.WriteLine("【OnStart】");
            await Models.SingletonSalesClass.Instance.InitializeAsync();
            // Handle when your app starts
            //await Models.SingletonSalesClass.Instance.Initialize();
        }

        protected override void OnSleep()
        {
            System.Diagnostics.Debug.WriteLine("【OnSleep】");
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            System.Diagnostics.Debug.WriteLine("【OnResume】");
            // Handle when your app resumes
        }
    }
}
