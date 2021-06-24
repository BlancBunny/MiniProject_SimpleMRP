using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DeviceSubApp
{
    public partial class FrmMain : Form
    {
        MqttClient client;
        string connectionString; // DB connection string | MQTT Broker address 
        ulong lineCount; // 
        delegate void UpdateTextCallback(string message);

        Stopwatch sw = new Stopwatch();

        public FrmMain()
        {
            InitializeComponent();
            InitializeAllData();
        }

        private void InitializeAllData()
        {
            connectionString = "Data Source=" + txtConnString.Text + ";Initial Catalog=MRP;" +
                "User ID=sa;password=mssql_p@ssw0rd!";
            lineCount = 0;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            IPAddress brokerAddress;

            try
            {
                brokerAddress = IPAddress.Parse(txtConnString.Text);
                client = new MqttClient(brokerAddress);
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            timer.Enabled = true;
            timer.Interval = 1000; // 1000ms
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblResult.Text = sw.Elapsed.Seconds.ToString();
            //if (sw.Elapsed.Seconds >= 3)
            //{
            //    sw.Stop();
            //    sw.Reset();
            //    // 처리
            //    UpdateText("progress");
                
            //}
        }

        private void PrcCorrectDataToDB()
        {
            if (iotData.Count > 0)
            {
                var correctData = iotData[iotData.Count - 1];
                // DB에 입력
                //UpdateText("DB처리");
                using (var conn = new SqlConnection(connectionString))
                {
                    var result = (correctData["PRC_MSG"] == "OK" ? 1 : 0);
                    string strUpQry = $@"UPDATE [Process_DEV]
                                            SET [PrcEndTime] = '{ DateTime.Now.ToString("HH:mm:ss") }' 
                                            ,[PrcResult] = '{ result }'
                                            ,[ModDate] = '{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }'
                                            ,[ModID] = '{ "SYS" }'
                                        WHERE PrcIdx = (SELECT TOP 1 PrcIdx FROM Process_DEV ORDER BY PrcIdx DESC)";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(strUpQry, conn);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            UpdateText(">>>> [DB] Update Complete");
                        }
                        else UpdateText(">>>> [DB] Update Failure");
                    }
                    catch (Exception ex)
                    {
                        UpdateText($">>>> DataToDB() Error Occured : {ex.Message}");
                    }
                }
            }

            

            iotData.Clear(); // 데이터 모두 삭제
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Message);
                UpdateText($">>>> Received Message : {message}\n");
                // message(json) -> C#

                var currentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
                PrcInputDataToList(currentData);

                sw.Stop();
                sw.Reset();
                sw.Start();
            }
            catch (Exception ex)
            {
                UpdateText($">>>> ERROR!! : {ex.Message}");
            }
        }


        List<Dictionary<string, string>> iotData = new List<Dictionary<string, string>>();
        /// <summary>
        /// 라즈베리에서 들어온 메시지를 전역리스트에 입력하는 메소드
        /// </summary>
        /// <param name="currentData">직렬화된 json 데이터</param>
        private void PrcInputDataToList(Dictionary<string, string> currentData)
        {
            if (currentData["PRC_MSG"] == "OK" || currentData["PRC_MSG"] == "FAIL")
            {
                iotData.Add(currentData);
            }
            PrcCorrectDataToDB();

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client.Connect(txtClntID.Text); // SUBCRIPT01
            UpdateText(">>>> Client Connected");
            client.Subscribe(new string[] { txtSubTopic.Text },
                new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });
            UpdateText(">>>> Subscribing to : " + txtSubTopic.Text);
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;

        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            UpdateText(">>>> Client Disconnected");
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        private void UpdateText(string message)
        {
            if (rtbSubscription.InvokeRequired)
            {
                UpdateTextCallback callback = new UpdateTextCallback(UpdateText);
                this.Invoke(callback, new object[] { message });
            }
            else
            {
                lineCount++;
                rtbSubscription.AppendText($"{lineCount} : {message}\n");
                rtbSubscription.ScrollToCaret();
            }
        }
    }
}
