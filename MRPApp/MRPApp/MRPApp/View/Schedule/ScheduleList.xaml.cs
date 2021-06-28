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

namespace MRPApp.View.Schedule
{

    public partial class ScheduleList : Page
    {
        public ScheduleList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadControlData(); // 콤보박스 데이터 로딩 메소드 
                LoadGridData();
                InitErrorMessages();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }

        private void LoadControlData()
        {
            var plantCodes = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("PC01")).ToList();
            cboPlantCode.ItemsSource = plantCodes;
            CboGridPlantCode.ItemsSource = plantCodes;

            var facilityIDs = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("FAC1")).ToList();
            cboSchFacilityID.ItemsSource = facilityIDs;
        }

        private bool IsvalidInputs()
        {
            var isValid = true;
            InitErrorMessages();

            if (cboPlantCode.SelectedValue == null)
            {
                lblPlantCode.Visibility = Visibility.Visible;
                lblPlantCode.Text = "공장을 선택하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(dtpSchDate.Text)) {
                lblSchDate.Visibility = Visibility.Visible;
                lblSchDate.Text = "공정일을 입력하세요";
                isValid = false;
            }

            else if (Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(cboPlantCode.SelectedValue.ToString()))
                .Where(d => d.SchDate.Equals(DateTime.Parse(dtpSchDate.Text))).Count() > 0)
            {
                lblSchDate.Visibility = Visibility.Visible;
                lblSchDate.Text = "해당 공장의 공정일 계획이 존재합니다";
                isValid = false;
            }

            if (string.IsNullOrEmpty(txtSchLoadTime.Text))
            {
                lblSchLoadTime.Visibility = Visibility.Visible;
                lblSchLoadTime.Text = "로드타임을 입력하세요";
                isValid = false;
            }

            if (cboSchFacilityID.SelectedValue == null)
            {
                lblSchFacilityID.Visibility = Visibility.Visible;
                lblSchFacilityID.Text = "공정설비를 선택하세요";
                isValid = false;
            }

            if (nudSchAmount.Value <= 0)
            {
                lblSchAmount.Visibility = Visibility.Visible;
                lblSchAmount.Text = "계획수량을 입력하세요";
                isValid = false;
            }


            //if (string.IsNullOrEmpty(txtBasicCode.Text))
            //{
            //    lblBasicCode.Visibility = Visibility.Visible;
            //    lblBasicCode.Text = "코드를 입력하세요";
            //    isValid = false; 
            //}
            //else if (Logic.DataAccess.GetSettings().Where(s => s.BasicCode.Equals(txtBasicCode.Text)).Count() > 0)
            //{
            //    lblBasicCode.Visibility = Visibility.Visible;
            //    lblBasicCode.Text = "중복 코드가 존재합니다";
            //    isValid = false;
            //}

            //if (string.IsNullOrEmpty(txtCodeName.Text))
            //{
            //    lblCodeName.Visibility = Visibility.Visible;
            //    lblCodeName.Text = "코드명을 입력하세요";
            //    isValid = false;
            //}

            return isValid;
        }

        private bool IsvalidUpdates()
        {
            var isValid = true;
            InitErrorMessages();

            if (cboPlantCode.SelectedValue == null)
            {
                lblPlantCode.Visibility = Visibility.Visible;
                lblPlantCode.Text = "공장을 선택하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(dtpSchDate.Text))
            {
                lblSchDate.Visibility = Visibility.Visible;
                lblSchDate.Text = "공정일을 입력하세요";
                isValid = false;
            }

            /*else if (Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(cboPlantCode.SelectedValue.ToString()))
                .Where(d => d.SchDate.Equals(DateTime.Parse(dtpSchDate.Text))).Count() > 0)
            {
                lblSchDate.Visibility = Visibility.Visible;
                lblSchDate.Text = "해당 공장의 공정일 계획이 존재합니다";
                isValid = false;
            }*/

            if (string.IsNullOrEmpty(txtSchLoadTime.Text))
            {
                lblSchLoadTime.Visibility = Visibility.Visible;
                lblSchLoadTime.Text = "로드타임을 입력하세요";
                isValid = false;
            }

            if (cboSchFacilityID.SelectedValue == null)
            {
                lblSchFacilityID.Visibility = Visibility.Visible;
                lblSchFacilityID.Text = "공정설비를 선택하세요";
                isValid = false;
            }

            if (nudSchAmount.Value <= 0)
            {
                lblSchAmount.Visibility = Visibility.Visible;
                lblSchAmount.Text = "계획수량을 입력하세요";
                isValid = false;
            }


            //if (string.IsNullOrEmpty(txtBasicCode.Text))
            //{
            //    lblBasicCode.Visibility = Visibility.Visible;
            //    lblBasicCode.Text = "코드를 입력하세요";
            //    isValid = false; 
            //}
            //else if (Logic.DataAccess.GetSettings().Where(s => s.BasicCode.Equals(txtBasicCode.Text)).Count() > 0)
            //{
            //    lblBasicCode.Visibility = Visibility.Visible;
            //    lblBasicCode.Text = "중복 코드가 존재합니다";
            //    isValid = false;
            //}

            //if (string.IsNullOrEmpty(txtCodeName.Text))
            //{
            //    lblCodeName.Visibility = Visibility.Visible;
            //    lblCodeName.Text = "코드명을 입력하세요";
            //    isValid = false;
            //}

            return isValid;
        }

        private void InitErrorMessages()
        {
            lblPlantCode.Visibility = lblSchDate.Visibility = lblSchLoadTime.Visibility = lblSchStartTime.Visibility
                = lblSchEndTime.Visibility = lblSchAmount.Visibility = lblSchFacilityID.Visibility = Visibility.Hidden;
        }

        private void LoadGridData()
        {
            List<Model.Schedules> schedules = Logic.DataAccess.GetSchedules();
            GrdData.ItemsSource = schedules;
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

            if (IsvalidInputs() != true) return;

            var item = new Model.Schedules();
            
            item.PlantCode = cboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(dtpSchDate.Text);
            item.PrcLoadTime = int.Parse(txtSchLoadTime.Text);
            if (tmpSchStartTime.SelectedDateTime != null)
                item.SchStartTime = tmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            if (tmpSchEndTime.SelectedDateTime != null)
                item.SchEndTime = tmpSchEndTime.SelectedDateTime.Value.TimeOfDay;
            item.SchFacilityID = cboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)nudSchAmount.Value;

            item.RegDate = DateTime.Now;
            item.RegID = "MRP";





            try
            {
                var result = Logic.DataAccess.SetSchedules(item);
                if (result == 0)
                {
                    Commons.LOGGER.Error("Schedules Insert Error");
                    await Commons.ShowMessageAsync("오류", "데이터 수정실패");
                }
                else
                {
                    Commons.LOGGER.Info("Schedules Insert Success");
                    ClearInputs();
                    LoadGridData();
                }


            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 : {ex}");
            }
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsvalidUpdates() != true) return;

            var schedules = GrdData.SelectedItem as Model.Schedules;
            //setting.CodeName = txtCodeName.Text;
            //setting.CodeDesc = txtCodeDesc.Text;
            //setting.ModDate = DateTime.Now;
            //setting.ModID = "MRP";

            var item = GrdData.SelectedItem as Model.Schedules;
            item.PlantCode = cboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(dtpSchDate.Text);
            item.PrcLoadTime = int.Parse(txtSchLoadTime.Text);
            item.SchStartTime = tmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            item.SchEndTime = tmpSchEndTime.SelectedDateTime.Value.TimeOfDay;
            item.SchFacilityID = cboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)nudSchAmount.Value;

            item.ModDate = DateTime.Now;
            item.ModID = "MRP";





            try
            {
                var result = Logic.DataAccess.SetSchedules(schedules);
                if (result == 0)
                {
                    Commons.LOGGER.Error("Schedules Update Error");
                    await Commons.ShowMessageAsync("오류", "데이터 수정실패");
                }
                else
                {
                    Commons.LOGGER.Info("Schedules Update Success");
                    ClearInputs();
                    LoadGridData();
                }


            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 : {ex}");
            }
        }

        private void ClearInputs()
        {
            txtSchIdx.Text = "";
            cboPlantCode.SelectedItem = null;
            dtpSchDate.Text = "";
            txtSchLoadTime.Text = "";
            tmpSchStartTime.SelectedDateTime = null;
            tmpSchEndTime.SelectedDateTime = null;
            cboSchFacilityID.SelectedItem = null;
            nudSchAmount.Value = 0;

            cboPlantCode.Focus();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = dtpSearchDate.Text;
            if (string.IsNullOrEmpty(search))
            {
                LoadGridData();
                return;
            }
                
            var dataList = Logic.DataAccess.GetSchedules().Where(s => s.SchDate.Equals(DateTime.Parse(search))).ToList();
            GrdData.ItemsSource = dataList;
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var item = GrdData.SelectedItem as Model.Schedules;
                txtSchIdx.Text = item.SchIdx.ToString();
                cboPlantCode.SelectedValue = item.PlantCode;
                dtpSchDate.Text = item.SchDate.ToString();
                txtSchLoadTime.Text = item.PrcLoadTime.ToString();
                if (item.SchStartTime != null)
                    tmpSchStartTime.SelectedDateTime = new DateTime(item.SchDate.Ticks + item.SchStartTime.Value.Ticks);
                else tmpSchStartTime.SelectedDateTime = null;

                if (item.SchEndTime != null)
                    tmpSchEndTime.SelectedDateTime = new DateTime(item.SchDate.Ticks + item.SchEndTime.Value.Ticks);
                else tmpSchEndTime.SelectedDateTime = null;
                cboSchFacilityID.SelectedValue = item.SchFacilityID;
                nudSchAmount.Value = item.SchAmount;
                
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
