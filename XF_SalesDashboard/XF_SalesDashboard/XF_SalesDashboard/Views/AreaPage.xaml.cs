using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_SalesDashboard.Views
{
    public partial class AreaPage : ContentPage
    {
        public AreaPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.AreaPageViewModel();
        }
    }
}
