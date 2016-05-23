using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XF_SalesDashboard.Models;

namespace XF_SalesDashboard.ViewModels
{
    public class AreaPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<SummaryModel> _salesData;

        public List<SummaryModel> SalesData
        {
            get { return _salesData; }
            set
            {
                _salesData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SalesData)));
            }
        }

        List<SummaryModel> _categoryData;

        public List<SummaryModel> CategoryData
        {
            get { return _categoryData; }
            set
            {
                _categoryData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoryData)));
            }
        }

        private List<SummaryModel> _segmentData;
        public List<SummaryModel> SegmentData
        {
            get { return _segmentData; }
            set
            {
                _segmentData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SegmentData)));
            }
        }



        public AreaPageViewModel()
        {
            Init();
        }

        private void Init()
        {
            SingletonSalesClass.Instance.PropertyChanged += Instance_PropertyChanged;
            this.SalesData = SingletonSalesClass.Instance.GetSales();
            this.CategoryData = SingletonSalesClass.Instance.GetSalesbyCategories();
            this.SegmentData = SingletonSalesClass.Instance.GetSalesbySegments();
        }

        private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SingletonSalesClass.SalesData))
            {
                this.SalesData = SingletonSalesClass.Instance.GetSales();
                this.CategoryData = SingletonSalesClass.Instance.GetSalesbyCategories();
                this.SegmentData = SingletonSalesClass.Instance.GetSalesbySegments();
            }
        }
    }
}
