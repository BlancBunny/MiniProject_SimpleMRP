using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
                    lblSchLoadTime.Content = $"{currSchedule.PrcLoadTime} 초";
                    lblSchAmount.Content = $"{currSchedule.SchAmount} 개";
                    btnStartProcess.IsEnabled = true;

                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 MyAccount Loaded : {ex}");
                
            }
        }

        private void btnStartProcess_Click(object sender, RoutedEventArgs e)
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
    }
}
