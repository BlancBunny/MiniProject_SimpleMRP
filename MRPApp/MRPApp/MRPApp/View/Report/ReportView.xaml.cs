using System;
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
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 ReportView_Page_Loaded : {ex}");
                throw ex;
            }
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
                MessageBox.Show("검색 시작");
            }
        }

        private bool IsValidInputs()
        {
            var result = true;
            return result;
        }
    }
}
