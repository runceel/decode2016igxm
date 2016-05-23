using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XF_SalesDashboard.Models
{
    public class SingletonSalesClass : INotifyPropertyChanged
    {
        static readonly string EndPoint = @"http://jxug.org/SalesData.json";

        /// <summary>
        /// たった一つのインスタンス
        /// </summary>
        public static SingletonSalesClass Instance { get; } = new SingletonSalesClass();

        private List<SalesModel> _salesData = new List<SalesModel>();

        public List<SalesModel> SalesData
        {
            get { return _salesData; }
            set
            {
                _salesData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SalesData)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// インスタンスが一つきりであることを保証するためコンストラクタは隠ぺいする
        /// </summary>
        private SingletonSalesClass()
        {

        }

        /// <summary>
        /// 初期化メソッド
        /// </summary>
        public async Task InitializeAsync()
        {
            System.Diagnostics.Debug.WriteLine("【SingletonSalesClass.Instance.Initialize()】");
            using (var client = new HttpClient())
            {
                using (var reader = new StreamReader(await client.GetStreamAsync(EndPoint)))
                {
                    var json = await reader.ReadToEndAsync();
                    this.SalesData = JsonConvert.DeserializeObject<List<SalesModel>>(json);
                }
            }
        }

        public List<SummaryModel> GetSales()
        {
            if (_salesData == null) { return new List<SummaryModel>(); }
            return this._salesData
                .GroupBy(x => x.StoreName)
                .Select(x => new SummaryModel
                {
                    Item = x.Key,
                    Value = x.Sum(y => y.Sales)
                }).ToList();
        }

        public List<SummaryModel> GetSalesbyCategories()
        {
            if (_salesData == null) { return new List<SummaryModel>(); }
            return this._salesData
                .ToArray()
                .GroupBy(x => x.Category)
                .Select(x => new SummaryModel
                {
                    Item = x.Key,
                    Value = x.Sum(y => y.Sales)
                }).ToList();
        }

        public List<SummaryModel> GetSalesbySegments()
        {
            if (_salesData == null) { return new List<SummaryModel>(); }
            return this._salesData
                .ToArray()
                .GroupBy(x => x.Segment.Contains("M") ? "男性" : "女性")
                .Select(x => new SummaryModel
                {
                    Item = x.Key,
                    Value = x.Sum(y => y.Sales)
                }).ToList();
        }
    }
}
