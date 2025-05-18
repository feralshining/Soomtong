using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;

namespace KioskBGRC
{
    public class QRdata
    {
        private static SerialPort sp = new SerialPort();
        private static string User_name { set => MainMenu.User_name = value; }
        private static string User_birth { set => MainMenu.User_birth = value; }
        private Dictionary<string, DateTime> received_data = new Dictionary<string, DateTime>();

        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes);
        }

        /// <summary>
        /// 시리얼 포트를 설정하여 키오스크 QR 데이터를 수신할 수 있게 설정합니다.
        /// </summary>
        public void Initialize(string portName)
        {
            sp = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            if (sp.IsOpen == false)
            {
                sp.DataReceived += new SerialDataReceivedEventHandler(GetQRData);
                try
                {
                    sp.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception(portName+"포트에 연결할 수 없습니다.");
            }
        }

        /// <summary>
        /// QR코드의 유효성을 검사합니다.
        /// </summary>
        public bool IsValid(string data)
        {
            //홍길동/1900-01-01/북구장애인종합복지관
            //name <= 홍길동
            //birth <= 1900-01-01
            //valid <= 북구장애인종합복지관
            if (!data.Contains("/")) return false;

            string[] matches = data.Split('/');
            return (matches.Length >= 3) && (matches[2] == "북구장애인종합복지관");
        }

        /// <summary>
        /// QR코드 수신 속도를 조절하는 기능입니다. 매개변수 second 이내에 데이터를 받을 경우 true를 반환합니다.
        /// </summary>
        public bool IsScanFast(string data, int second)
        {
            if (received_data.TryGetValue(data, out DateTime last_received_time))
            {
                if ((DateTime.Now - last_received_time).TotalSeconds < second) return true;
                received_data[data] = DateTime.Now;
            }
            else
            {
                received_data.Add(data, DateTime.Now);
            }
            return false;
        }

        /// <summary>
        /// 중복 및 불필요 데이터를 제거합니다.
        /// </summary>
        private string Purge(string data)
        {
            string[] realdata = new string[2953];
            MatchCollection matches = Regex.Matches(data, Environment.NewLine);

            if (matches.Count > 1)
            {
                realdata = data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                data = realdata[0];
                return data;
            }
            matches = Regex.Matches(data, "북구장애인종합복지관");
            if (matches.Count > 1)
            {
                realdata = data.Split(new string[] { "북구장애인종합복지관" }, StringSplitOptions.RemoveEmptyEntries);
                data = realdata[0];
                return data;
            }
            if (data.Contains(Environment.NewLine)) data = data.Replace(Environment.NewLine, "");

            return data;
        }

        /// <summary>
        /// 자동으로 수신된 데이터의 유효성을 검사하고 유효한 데이터일 경우 User 프로퍼티에 값을 추가합니다.
        /// </summary>
        private void GetQRData(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int size = sp.BytesToRead;
                byte[] buffer = new byte[size];
                sp.Read(buffer, 0, size);

                string qrdata = Encoding.UTF8.GetString(buffer);
                //qrdata = FromHexString(qrdata); //가상 시리얼 통신 테스트용 코드. 키오스크 직접 테스트 시 비활성화 필요.

                if (IsScanFast(qrdata, 3)) return; //데이터 처리 속도 조절
                qrdata = Purge(qrdata); //중복 및 불필요 데이터 제거
                if (!IsValid(qrdata)) //유효 데이터 검증
                {
                    User_name = "Invalid QR";
                    User_birth = "Invalid QR";
                    return;
                }
                User_name = qrdata.Split('/')[0];
                User_birth = qrdata.Split('/')[1];
            }
            catch (Exception ex)
            {
                MainMenu.HandleException(ex);
                return;
            }
        }

        /// <summary>
        /// 입력받은 data를 비동기로 재생합니다.
        /// </summary>
        public void Speak(string data)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.SetOutputToDefaultAudioDevice();
            ss.SelectVoice("Microsoft Heami Desktop");
            ss.Rate = -4;
            ss.SpeakAsync(data);
        }

        /// <summary>
        /// 시리얼 포트를 모두 닫습니다.
        /// </summary>
        public void Close()
        {
            sp.Close();
            sp.Dispose();
        }
    }
}
