using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MRPApp.View.Report
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReportView : Page
    {
        public ReportView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InitControls();

                // DisplayChart();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 ReportView_Page_Loaded : {ex}");
                throw ex;
            }
        }

        private void DisplayChart(List<Model.Report> list)
        {
            int[] schAmounts = list.Select(a => (int)a.SchAmount).ToArray();
            int[] prcOkAmounts = list.Select(a => (int)a.PrcOkAmount).ToArray();
            int[] prcFailAmounts = list.Select(a => (int)a.PrcFailAmount).ToArray();
            

            var series1 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "계획수량",
                Values = new LiveCharts.ChartValues<int>(schAmounts)
            };
            var series2 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "성공수량",
                Values = new LiveCharts.ChartValues<int>(prcOkAmounts)
            };
            var series3 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "실패수량",
                Values = new LiveCharts.ChartValues<int>(prcFailAmounts)
            };

            chtReport.Series.Clear();
            chtReport.Series.Add(series1);
            chtReport.Series.Add(series2);
            chtReport.Series.Add(series3);
            chtReport.AxisX.First().Labels = list.Select(a => a.PrcDate.ToString("yyyy-MM-dd")).ToList();
        }

        private void InitControls()
        {
            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-7);
            dtpEndDate.SelectedDate = DateTime.Now;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs())
            {
                var startDate = ((DateTime)dtpStartDate.SelectedDate).ToString("yyyy-MM-dd");
                var endDate = ((DateTime)dtpEndDate.SelectedDate).ToString("yyyy-MM-dd");
                var searchResult = Logic.DataAccess.GetReportDatas(startDate, endDate, Commons.PlANTCODE);

                DisplayChart(searchResult);
                MessageBox.Show("검색 시작");
            }
        }

        private bool IsValidInputs()
        {
            var result = true;

            if (dtpStartDate.SelectedDate == null || dtpEndDate.SelectedDate == null)
            {
                Commons.ShowMessageAsync("검색", "검색할 일자를 선택하세요.");
                result = false;
            }

            if (dtpStartDate.SelectedDate > dtpEndDate.SelectedDate)
            {
                Commons.ShowMessageAsync("검색", "시작일자와 종료일자를 확인해주세요.");
                result = false;
            }

            

            return result;
        }
    }
}
