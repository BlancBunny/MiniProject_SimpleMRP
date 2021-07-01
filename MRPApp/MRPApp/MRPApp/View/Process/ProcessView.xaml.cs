using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MRPApp.View.Process
{
    /// <summary>
    /// 1. 공정계획에서 오늘의 생산계획 불러옴
    /// 2. 없으면 에러표시, 시작버튼을 클릭하지 못하게 비활성화
    /// 3. 있으면 오늘의 날짜 표시, 시작 버튼 활성화
    /// 4. 시작버튼 클릭시 새 공정을 생성, DB에 입력 
    /// 5. 공정처리 애니메이션 시작
    /// 6. 로드타임 후 애니메이션 중지
    /// 7. 센서링값 리턴될때까지 대기
    /// 8. 센서링 결과값에 따라 생산품 색상 변경
    /// 9. 현재 공정의 DB값 업데이트
    /// 10. 결과 레이블 값 수정/표시
    /// </summary>
    public partial class ProcessView : Page
    {
        private Model.Schedules currSchedule;
        public ProcessView()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                currSchedule = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(Commons.PlANTCODE))
                    .Where(s => s.SchDate.Equals(DateTime.Parse(today))).FirstOrDefault();
                if (currSchedule == null)
                {
                    await Commons.ShowMessageAsync("공정", "공정계획이 없습니다. 계획일정을 먼저 입력하세요.");
                    // TODO : 시작 버튼 DISABLE
                    lblProcessDate.Content = string.Empty;
                    lblSchLoadTime.Content = "none";
                    lblSchAmount.Content = "none";
                    btnStartProcess.IsEnabled = false;
                    return;
                }

                else
                {
                    // 공정계획 표시
                    MessageBox.Show($"{today} 공정 시작");
                    lblProcessDate.Content = currSchedule.SchDate.ToString("yyyy년 MM월 dd일");
                    lblSchAmount.Content = $"{currSchedule.SchAmount} 개";
                    btnStartProcess.IsEnabled = true;
                    UpdateData();

                    InitConnectMqttBroker();

                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                
            }
        }

        MqttClient client;
        Timer timer = new Timer();
        Stopwatch sw = new Stopwatch();
        bool prcResult;

        /// <summary>
        /// 공정 시작 시 MQTT Broker에 연결
        /// </summary>
        private void InitConnectMqttBroker()
        {
            var brokerAddress = IPAddress.Parse("210.119.12.96");
            client = new MqttClient(brokerAddress);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.Connect("Mornitor");
            client.Subscribe(new string[] { "factory1/machine1/data/" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE});


            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sw.Elapsed.Seconds >= 2) // 2초 대기후 일처리
            {
                sw.Stop();
                sw.Reset();
                
                
            }
        }

        private void UpdateData()
        {
            var prcSuccessAmount = Logic.DataAccess.GetProcess().Where(p => p.SchIdx.Equals(currSchedule.SchIdx))
                .Where(p => p.PrcResult.Equals(true)).Count();
            var prcFailAmount = Logic.DataAccess.GetProcess().Where(p => p.SchIdx.Equals(currSchedule.SchIdx))
                .Where(p => p.PrcResult.Equals(false)).Count();
            var prcSuccessRatio = ((double)prcSuccessAmount / (double)currSchedule.SchAmount) * 100;
            var prcFailRatio = ((double)prcFailAmount / (double)currSchedule.SchAmount) * 100;

            lblSuccessAmount.Content = $"{prcSuccessAmount} 개";
            lblFailAmount.Content = $"{prcFailAmount} 개";
            lblSuccessRatio.Content = $"{prcSuccessRatio:#.##} %";
            lblFailRatio.Content = $"{prcFailRatio:#.##} %";
        }

        Dictionary<string, string> currentData = new Dictionary<string, string>();

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Message);
                currentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

                if (currentData["PRC_MSG"] == "OK" || currentData["PRC_MSG"] == "FAIL")
                {
                    if (currentData["PRC_MSG"] == "OK")
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            prcResult = true;
                            product.Fill = new SolidColorBrush(Colors.Green);

                        }));
                    }
                    else if (currentData["PRC_MSG"] == "FAIL")
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            prcResult = false;
                            product.Fill = new SolidColorBrush(Colors.Red);
                        }));
                    }
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        UpdateData();
                    }));

                    sw.Stop();
                    sw.Reset();
                    sw.Start();

                    StartSensorAnimation();
                }

                
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 : {ex.Message}");
            }
        }

        private void StartSensorAnimation()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                DoubleAnimation ba = new DoubleAnimation();
                ba.From = 1; // 이미지 보임
                ba.To = 0;   // 이미지 안보임
                ba.Duration = TimeSpan.FromSeconds(1);
                ba.AutoReverse = true;
                // ba.RepeatBehavior = RepeatBehavior.Forever; 무한 반복
                sensor.BeginAnimation(OpacityProperty, ba);
            }));
            
            

            
        }

        private void btnStartProcess_Click(object sender, RoutedEventArgs e)
        {
            if (InsertProcessData())
                StartAnimation();
        }

        private bool InsertProcessData()
        {
            var item = new Model.Process();
            item.SchIdx = currSchedule.SchIdx;
            item.PrcCode = GetProcessCodeFromDB();
            item.PrcDate = DateTime.Now;
            item.PrcLoadTime = currSchedule.PrcLoadTime;
            item.PrcStartTime = currSchedule.SchStartTime;
            item.PrcEndTime = currSchedule.SchEndTime;
            item.PrcFacilityID = Commons.FACILITYID;
            item.PrcResult = prcResult; // 공정 성공 여부
            item.RegDate = DateTime.Now.ToString("yyyy-MM-dd");
            item.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetProcess(item);
                if (result == 0)
                {
                    Commons.LOGGER.Error("공정데이터 입력 실패!");
                    return false;
                }
                else
                {
                    Commons.LOGGER.Info("공정데이터 입력!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 ProcessView.cs : {ex.Message}");
                Commons.ShowMessageAsync("오류", "공정시작 오류발생, 관리자 문의");
                return false;
            }

        }

        private string GetProcessCodeFromDB()
        {
            var prefix = "PRC";
            var prePrcCode = prefix + DateTime.Now.ToString("yyyyMMdd");
            var resultCode = string.Empty;

            var maxPrc = Logic.DataAccess.GetProcess().Where(p => p.PrcCode.Contains(prePrcCode))
                .OrderByDescending(p => p.PrcCode).FirstOrDefault();
            
            if (maxPrc == null)
            {
                resultCode = prePrcCode + 1.ToString("000");    
            }
            else
            {
                var maxPrcCode = maxPrc.PrcCode;
                var maxVal = int.Parse(maxPrcCode.Substring(11)) + 1;

                resultCode = prePrcCode + maxVal.ToString("000");
            }
            return resultCode;
        }

        /// <summary>
        /// 애니메이션 처리
        /// </summary>
        private void StartAnimation()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = TimeSpan.FromSeconds(currSchedule.PrcLoadTime.Value);


            // gear lotation animation
            RotateTransform rt = new RotateTransform();
            gear1.RenderTransform = rt;
            gear1.RenderTransformOrigin = new Point(0.5, 0.5);
            gear2.RenderTransform = rt;
            gear2.RenderTransformOrigin = new Point(0.5, 0.5);

            rt.BeginAnimation(RotateTransform.AngleProperty, da);

            // product move animation
            DoubleAnimation ma = new DoubleAnimation();
            ma.From = 175;
            ma.To = 565; // 옮겨지는 x값의 최대값
            ma.Duration = TimeSpan.FromSeconds(currSchedule.PrcLoadTime.Value);
            // ma.AccelerationRatio = 0.5;

            // ma.AutoReverse = true;
            product.BeginAnimation(Canvas.LeftProperty, ma);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (client.IsConnected)
                client.Disconnect();
            timer.Dispose();
        }
    }
}
