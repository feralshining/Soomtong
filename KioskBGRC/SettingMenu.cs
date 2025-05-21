using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO.Ports;

namespace KioskBGRC
{
    public partial class SettingMenu : MetroFramework.Forms.MetroForm
    {
        public SettingMenu()
        {
            InitializeComponent();
        }
        #region "[     키오스크 설정     ]"
        #region "[     리소스 설정     ]"
        private void btn_resrc_Click(object sender, EventArgs e)
        {
            IntPtr handle = this.Handle;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => btn_resrc_Click(sender, e)));
                return;
            }

            using (CommonOpenFileDialog dlg = new CommonOpenFileDialog())
            {
                dlg.Title = "리소스 폴더 경로를 설정해주세요.";
                dlg.IsFolderPicker = true;

                // 비동기로 대화 상자 열기
                CommonFileDialogResult result = dlg.ShowDialog(handle);

                if (result == CommonFileDialogResult.Ok)
                {
                    string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
                    if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

                    string file_path = $"{directory_path}//Config.json";
                    if (!File.Exists(file_path)) return;
                    JObject data = new JObject();
                    try
                    {
                        using (StreamReader file = new StreamReader(file_path))
                        {
                            using (JsonTextReader reader = new JsonTextReader(file))
                            {
                                data = (JObject)JToken.ReadFrom(reader);
                                if (data.ContainsKey("RESOURCE_PATH")) data["RESOURCE_PATH"].Replace(dlg.FileName);
                                else data.Add("RESOURCE_PATH", dlg.FileName);
                            }
                        }
                    }
                    catch
                    {
                        data.Add("RESOURCE_PATH", dlg.FileName);
                    }
                    File.WriteAllText(file_path, data.ToString());
                    BeginInvoke(new Action(() => txt_resource_path.Text = dlg.FileName));
                }
            }
        }

        #endregion
        #region "[     배너 설정     ]"
        private void btn_bannerMain_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "PNG파일 (*.png) |*.png|JPG파일 (*.jpg) |*.jpg|JPEG파일 (*.jpeg) |*jpeg|BMP파일 (*.bmp) |*.bmp|모든 파일 (*.*)|*.*";
                ofd.Title = "메인 배너에 등록할 이미지를 불러와주세요.";
                ofd.ShowHelp = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
                    if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

                    string file_path = $"{directory_path}//Config.json";
                    if (!File.Exists(file_path)) return;
                    JObject data = new JObject();
                    try
                    {
                        using (StreamReader file = new StreamReader(file_path))
                        {
                            using (JsonTextReader reader = new JsonTextReader(file))
                            {
                                data = (JObject)JToken.ReadFrom(reader);
                                if (data.ContainsKey("BANNER_MAIN")) data["BANNER_MAIN"].Replace(ofd.FileName);
                                else data.Add("BANNER_MAIN", ofd.FileName);
                            }
                        }
                    }
                    catch
                    {
                        data.Add("BANNER_MAIN", ofd.FileName);
                    }
                    File.WriteAllText(file_path, data.ToString());
                    txt_bannerMain.Text = ofd.FileName;
                }
            }
        }
        #endregion
        #region "[     프로그램 홍보 폴더 설정     ]"
        private void btn_program_Click(object sender, EventArgs e)
        {
            IntPtr handle = this.Handle;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => btn_program_Click(sender, e)));
                return;
            }

            using (CommonOpenFileDialog dlg = new CommonOpenFileDialog())
            {
                dlg.Title = "프로그램 홍보 폴더 경로를 설정해주세요.";
                dlg.IsFolderPicker = true;

                CommonFileDialogResult result = dlg.ShowDialog(handle);

                if (result == CommonFileDialogResult.Ok)
                {
                    string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
                    if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

                    string file_path = $"{directory_path}//Config.json";
                    if (!File.Exists(file_path)) return;
                    JObject data = new JObject();
                    try
                    {
                        using (StreamReader file = new StreamReader(file_path))
                        {
                            using (JsonTextReader reader = new JsonTextReader(file))
                            {
                                data = (JObject)JToken.ReadFrom(reader);
                                if (data.ContainsKey("PROGRAM_PATH")) data["PROGRAM_PATH"].Replace(dlg.FileName);
                                else data.Add("PROGRAM_PATH", dlg.FileName);
                            }
                        }
                    }
                    catch
                    {
                        data.Add("PROGRAM_PATH", dlg.FileName);
                    }
                    File.WriteAllText(file_path, data.ToString());

                    BeginInvoke(new Action(() => txt_programPath.Text = dlg.FileName));
                }
            }
        }
        #endregion
        #region "[     시리얼 포트 설정     ]"
        private void serialportCB_SelectedValueChanged(object sender, EventArgs e)
        {
            string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
            if (!Directory.Exists(directory_path)) return;

            string file_path = $"{directory_path}//Config.json";
            JObject data = new JObject();
            try
            {
                using (StreamReader file = new StreamReader(file_path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        data = (JObject)JToken.ReadFrom(reader);
                        if (serialportCB.SelectedValue == null) return;
                        if (data.ContainsKey("SERIAL_PORT")) data["SERIAL_PORT"].Replace(serialportCB.SelectedValue.ToString());
                        else data.Add("SERIAL_PORT", serialportCB.SelectedItem.ToString());
                    }
                }
            }
            catch
            {
                data.Add("SERIAL_PORT", serialportCB.SelectedItem.ToString());
            }
            File.WriteAllText(file_path, data.ToString());
        }
        #endregion
        #endregion

        #region "[     무료급식사업 설정     ]"
        #region "[     명단 설정     ]"
        private void btn_file_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "엑셀 파일 (*.xlsx) |*.xlsx|엑셀 (매크로) 파일 (*.xlsm) |*.xlsm|모든 파일 (*.*)|*.*";
                ofd.Title = "명단으로 사용할 파일의 위치를 설정해주세요.";
                ofd.ShowHelp = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
                    if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

                    string file_path = $"{directory_path}//Config.json";
                    JObject data = new JObject();
                    if (!File.Exists(file_path)) return;
                    try
                    {
                        using (StreamReader file = new StreamReader(file_path))
                        {
                            using (JsonTextReader reader = new JsonTextReader(file))
                            {
                                data = (JObject)JToken.ReadFrom(reader);
                                if (data.ContainsKey("LIST_PATH")) data["LIST_PATH"].Replace(ofd.FileName);
                                else data.Add("LIST_PATH", ofd.FileName);
                            }
                        }
                    }
                    catch
                    {
                        data.Add("LIST_PATH", ofd.FileName);
                    }
                    File.WriteAllText(file_path, data.ToString());
                    txt_listfile_path.Text = ofd.FileName;
                }
            }
        }
        #endregion
        #region "[     체크 시간 설정     ]"
        private void timePicker1_ValueChanged(object sender, EventArgs e)
        {
            string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
            if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

            string file_path = $"{directory_path}//Config.json";
            JObject data = new JObject();
            if (!File.Exists(file_path)) return;
            try
            {
                using (StreamReader file = new StreamReader(file_path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        data = (JObject)JToken.ReadFrom(reader);
                        if (data.ContainsKey("START_TIME"))data["START_TIME"].Replace(timePicker1.Value.ToString("t"));
                        else data.Add("START_TIME", timePicker1.Value.ToString("t"));
                    }
                }
            }
            catch
            {
                data.Add("START_TIME", timePicker1.Value.ToString("t"));
            }
            FreeLunch.startchk_time = timePicker1.Value.ToString("t");
            File.WriteAllText(file_path, data.ToString());
        }

        private void timePicker2_ValueChanged(object sender, EventArgs e)
        {
            string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
            if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

            string file_path = $"{directory_path}//Config.json";
            JObject data = new JObject();
            if (!File.Exists(file_path)) return;
            try
            {
                using (StreamReader file = new StreamReader(file_path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        data = (JObject)JToken.ReadFrom(reader);
                        if (data.ContainsKey("END_TIME")) data["END_TIME"].Replace(timePicker2.Value.ToString("t"));
                        else data.Add("END_TIME", timePicker2.Value.ToString("t"));
                    }
                }
            }
            catch
            {
                data.Add("END_TIME", timePicker2.Value.ToString("t"));
            }
            FreeLunch.endchk_time = timePicker2.Value.ToString("t");
            File.WriteAllText(file_path, data.ToString());
        }
        #endregion
        #region "[     일일 식사내역 저장 폴더 설정     ]"
        private void btn_savedailydir_Click(object sender, EventArgs e)
        {
            IntPtr handle = this.Handle;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => btn_savedailydir_Click(sender, e)));
                return;
            }

            using (CommonOpenFileDialog dlg = new CommonOpenFileDialog())
            {
                dlg.Title = "일일 식사내역이 저장될 폴더 경로를 설정해주세요.";
                dlg.IsFolderPicker = true;

                CommonFileDialogResult result = dlg.ShowDialog(handle);

                if (result == CommonFileDialogResult.Ok)
                {
                    string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
                    if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

                    string file_path = $"{directory_path}//Config.json";
                    if (!File.Exists(file_path)) return;
                    JObject data = new JObject();
                    try
                    {
                        using (StreamReader file = new StreamReader(file_path))
                        {
                            using (JsonTextReader reader = new JsonTextReader(file))
                            {
                                data = (JObject)JToken.ReadFrom(reader);
                                if (data.ContainsKey("LUNCHLOG_DIR_PATH")) data["LUNCHLOG_DIR_PATH"].Replace(dlg.FileName);
                                else data.Add("LUNCHLOG_DIR_PATH", dlg.FileName);
                            }
                        }
                    }
                    catch
                    {
                        data.Add("LUNCHLOG_DIR_PATH", dlg.FileName);
                    }
                    File.WriteAllText(file_path, data.ToString());

                    BeginInvoke(new Action(() => txt_dailylogPath.Text = dlg.FileName));
                }
            }
        }
            #endregion
        #endregion

         #region "[     기본 설정     ]"
        #region "[     에러 디버그 확인     ]"
            private void btn_debug_Click(object sender, EventArgs e)
        {
            string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
            if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);
            System.Diagnostics.Process.Start(directory_path);
        }
        #endregion
        #region "[     인터넷 연결 상태 확인     ]"
        private void network_btn_Click(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable()) btn_network.Text = "인터넷 연결 상태 : True";
            else btn_network.Text = "인터넷 연결 상태 : False";
        }
        #endregion
        #region "[     프로그램 종료     ]"
        private void btn_exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("정말로 종료하시겠습니까?", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MainMenu.DisposeBlending();
                    this.Dispose();
                    this.Close();
                    Application.Exit();
                }
            }
            catch (Exception)
            {
                Application.Exit();
            } 
        }
        #endregion
        #endregion
        private void SettingMenu_Load(object sender, EventArgs e)
        {
            string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
            if (!Directory.Exists(directory_path)) return;

            string file_path = $"{directory_path}//Config.json";

            if (!File.Exists(file_path))
            {
                JObject data = new JObject();
                data.Add("RESOURCE_PATH", "null");
                data.Add("BANNER_MAIN", "null");
                data.Add("PROGRAM_PATH", "null");
                data.Add("SERIAL_PORT", "null");
                data.Add("LIST_PATH", "null");
                data.Add("LUNCHLOG_DIR_PATH", "null");
                data.Add("START_TIME", "11:00");
                data.Add("END_TIME", "12:30");
                File.WriteAllText(file_path, data.ToString());
                return;
            }
            try
            {
                JObject data = new JObject();
                using (StreamReader file = new StreamReader(file_path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        data = (JObject)JToken.ReadFrom(reader);
                    }
                }
                serialportCB.DataSource = SerialPort.GetPortNames();

                if (data.ContainsKey("RESOURCE_PATH")) txt_resource_path.Text = data["RESOURCE_PATH"].ToString();
                if (data.ContainsKey("PROGRAM_PATH")) txt_programPath.Text = data["PROGRAM_PATH"].ToString();
                if (data.ContainsKey("BANNER_MAIN")) txt_bannerMain.Text = data["BANNER_MAIN"].ToString();
                if (data.ContainsKey("SERIAL_PORT"))
                {
                    for (int i = 0; i < serialportCB.Items.Count; i++)
                    {
                        if (serialportCB.Items[i].ToString().Equals(data["SERIAL_PORT"].ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            serialportCB.SelectedIndex = i;
                            break;
                        }
                    }
                }
                if (data.ContainsKey("LIST_PATH")) txt_listfile_path.Text = data["LIST_PATH"].ToString();
                if (data.ContainsKey("LUNCHLOG_DIR_PATH")) txt_dailylogPath.Text = data["LUNCHLOG_DIR_PATH"].ToString();
                if (data.ContainsKey("START_TIME")) timePicker1.Value = DateTime.Parse(data["START_TIME"].ToString());
                if (data.ContainsKey("END_TIME")) timePicker2.Value = DateTime.Parse(data["END_TIME"].ToString());

                if (NetworkInterface.GetIsNetworkAvailable())
                    btn_network.Text = "인터넷 연결 상태 : True";
                else
                    btn_network.Text = "인터넷 연결 상태 : False";
            }
            catch (Exception ex)
            {
                MainMenu.HandleException(ex);
                return;
            }

        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

#region "[     TCP COMMUNICATION CODE     ]"
//private void communication_btn_Click(object sender, EventArgs e)
//{
//    if (is_connected == false)
//    {
//        Thread thread1 = new Thread(ConnectTCP);
//        thread1.IsBackground = true;
//        thread1.Start();
//    }
//    else
//    {
//        reader.Close();
//        writer.Close();
//        communication_btn.Text = "TCP 통신 연결";
//        is_connected = false;
//    }
//}
//private void ConnectTCP()
//{
//    try
//    {
//        TcpListener listener = new TcpListener(IPAddress.Parse(IP_ADDRESS), PORT);
//        listener.Start();
//        WriteText("연결 대기 중");
//        using (TcpClient client = listener.AcceptTcpClient())
//        {
//            WriteText("TCP 통신 연결 해제");

//            reader = new StreamReader(client.GetStream());
//            is_connected = true;
//            ProcessStartInfo cmd = new ProcessStartInfo();
//            cmd.FileName = @"cmd";
//            cmd.WindowStyle = ProcessWindowStyle.Hidden;
//            cmd.CreateNoWindow = true;
//            cmd.UseShellExecute = false;
//            cmd.RedirectStandardOutput = true;
//            cmd.RedirectStandardInput = true;
//            cmd.RedirectStandardError = true;

//            Process ps = new Process();
//            ps.EnableRaisingEvents = false;
//            ps.StartInfo = cmd;
//            ps.Start();

//            while (client.Connected)
//            {
//                ps.StandardInput.Write(reader.ReadToEnd() + Environment.NewLine);
//                ps.StandardInput.Close();
//            }
//        }
//    }
//    catch (Exception ex)
//    {

//    }
//    finally
//    {
//        reader.Close();
//        writer.Close();
//        communication_btn.Invoke((MethodInvoker)(() => communication_btn.Text = "TCP 통신 연결"));
//        is_connected = false;
//    }
//}
//private void WriteText(string data)
//{
//    communication_btn.Invoke((MethodInvoker)(() => communication_btn.Text = data));
//}
#endregion

