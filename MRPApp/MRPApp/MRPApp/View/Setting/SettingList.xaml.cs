using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

namespace MRPApp.View.Setting
{
    public partial class SettingList : Page
    {
        public SettingList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadGridData();
                InitErrorMessages();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
            }
        }

        private bool IsvalidInputs()
        {
            var isValid = true;
            InitErrorMessages();

            if (string.IsNullOrEmpty(txtBasicCode.Text))
            {
                lblBasicCode.Visibility = Visibility.Visible;
                lblBasicCode.Text = "코드를 입력하세요";
                isValid = false; 
            }
            else if (Logic.DataAccess.GetSettings().Where(s => s.BasicCode.Equals(txtBasicCode.Text)).Count() > 0)
            {
                lblBasicCode.Visibility = Visibility.Visible;
                lblBasicCode.Text = "중복 코드가 존재합니다";
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtCodeName.Text))
            {
                lblCodeName.Visibility = Visibility.Visible;
                lblCodeName.Text = "코드명을 입력하세요";
                isValid = false;
            }

            return isValid;
        }

        private void InitErrorMessages()
        {
            throw new NotImplementedException();
        }

        private void LoadGridData()
        {
            List<Model.Settings> settings = Logic.DataAccess.GetSettings();
            GrdData.ItemsSource = settings;
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //NavigationService.Navigate(new EditUser());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditUser_Click : {ex}");
                throw ex;
            }
        }

        private void BtnAddStore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // NavigationService.Navigate(new AddStore());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAddStore_Click : {ex}");
                throw ex;
            }
        }

        private void BtnEditStore_Click(object sender, RoutedEventArgs e)
        {
            /*if (GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("창고수정", "수정할 창고를 선택하세요");
                return;
            }*/

            try
            {
                
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditStore_Click : {ex}");
                throw ex;
            }
        }

        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            var setting = new Model.Settings();
            setting.BasicCode = txtBasicCode.Text;
            setting.CodeName = txtCodeName.Text;
            setting.CodeDesc = txtCodeDesc.Text;
            setting.RegDate = DateTime.Now;
            setting.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSettings(setting);
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 수정시 오류 발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정실패");
                }
                else
                {
                    Commons.LOGGER.Info("데이터 수정 성공");
                    ClearInputs();
                    LoadGridData();
                }
                


            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 : {ex}");
                throw;
            }
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var setting = GrdData.SelectedItem as Model.Settings;
            setting.CodeName = txtCodeName.Text;
            setting.CodeDesc = txtCodeDesc.Text;
            setting.ModDate = DateTime.Now;
            setting.ModID = "MRP";
            

            try
            {
                var result = Logic.DataAccess.SetSettings(setting);
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 수정시 오류 발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정실패");
                }
                else
                {
                    Commons.LOGGER.Info("데이터 수정 성공");
                    ClearInputs();
                    LoadGridData();
                }


            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 : {ex}");
                throw;
            }
        }

        private void ClearInputs()
        {
            txtBasicCode.IsReadOnly = false;
            txtBasicCode.Background = new SolidColorBrush(Colors.White);
            txtBasicCode.Text = txtCodeName.Text = txtCodeDesc.Text = string.Empty;

            txtBasicCode.Focus();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = txtSearch.Text.Trim();

            var settings = Logic.DataAccess.GetSettings().Where(s => s.CodeName.Contains(search)).ToList();
            GrdData.ItemsSource = settings;
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var setting = GrdData.SelectedItem as Model.Settings;
                txtBasicCode.Text = setting.BasicCode;
                txtCodeName.Text = setting.CodeName;
                txtCodeDesc.Text = setting.CodeDesc;

                txtBasicCode.IsReadOnly = true;
                txtBasicCode.Background = new SolidColorBrush(Colors.LightGray);
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 : {ex.Message}");
            }
        }

        private void txtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnSearch_Click(sender, e);



            }
        }
    }
}
