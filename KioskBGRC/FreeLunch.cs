using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskBGRC
{
    public class FreeLunch
    {
        public static string userlist_path = "";
        public static string lunchlog_path = "";
        public static string user_name = "";
        public static string user_birth = "";
        public static string startchk_time = "", endchk_time = "";

        /// <summary>
        /// name, birth 인자값을 비교하여 관리자인지 아닌지 여부를 반환합니다.
        /// </summary>
        public bool IsAdmin(string name, string birth) => name == "관리자" && birth == "2024-03-04";
        /// <summary>
        /// name, birth 인자값을 비교하여 유효한 코드인지 아닌지 여부를 반환합니다.
        /// </summary>
        public bool IsInvalid(string name, string birth) => name == "Invalid QR" && birth == "Invalid QR";
        /// <summary>
        /// 현재 QR코드 체크 가능한 시간인지 아닌지 여부를 반환합니다.
        /// </summary>
        public bool IsChkTime()
        {
            KioskUI UI = new KioskUI();
            DateTime current_time = DateTime.Parse(UI.GetCurrentTime());
            DateTime startchk = DateTime.Parse(startchk_time);
            DateTime endchk = DateTime.Parse(endchk_time);
            return (current_time < startchk || current_time > endchk);
        }
        /// <summary>
        /// 명단에 등록된 이용자인지 아닌지 여부를 반환합니다.
        /// </summary>
        public bool IsUser(string name, string birth)
        {
            ExcelData Excel = new ExcelData();
            return (Excel.IsContainExcel(userlist_path, name) == true && Excel.IsContainExcel(userlist_path, birth) == true);
        }

        /// <summary>
        /// 파일이 생성된 오늘 날짜에 해당 요소가 기재되어 있는지 여부를 반환합니다. 
        /// 0은 중복이 아님, 1은 중복, -1은 파일이 존재하지 않음을 나타냅니다.
        /// </summary>
        public int IsContainText(string name, string birth)
        {
            if (String.IsNullOrEmpty(lunchlog_path)) throw new FileNotFoundException("무료급식 일일 식사내역 폴더의 경로가 설정되지 않았습니다.");
            DateTime date = DateTime.Now;
            string filePath = Path.Combine(lunchlog_path, $"무료급식 일일 식사내역_{date.ToString("yyyy.MM.dd")}.txt");
            try
            {
                if (!File.Exists(filePath)) return -1;

                string user_data = File.ReadAllText(filePath);
                if (user_data.Contains(name) && user_data.Contains(birth)) return 1;

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 입력받은 데이터를 자세한 날짜 및 정보를 추가 기재한 텍스트 파일로 생성합니다.
        /// </summary>
        public void CreateText(string name, string birth)
        {
            if (String.IsNullOrEmpty(lunchlog_path)) throw new FileNotFoundException("무료급식 일일 식사내역 폴더의 경로가 설정되지 않았습니다.");
            DateTime date = DateTime.Now;
            string data = "";
            string filePath = Path.Combine(lunchlog_path, $"무료급식 일일 식사내역_{date.ToString("yyyy.MM.dd")}.txt");
            try
            {
                if (!File.Exists(filePath)) using (FileStream fs = File.Create(filePath)) { }

                string[] read_data = File.ReadAllLines(filePath);
                if (read_data.Length == 0)
                {
                    data = ($"[출석 인원 : {read_data.Length + 1}]{Environment.NewLine}{name}/{birth}/{date.ToString("T")}");
                    File.WriteAllText(filePath, data);
                    return;
                }
                else
                {
                    string[] text = new string[read_data.Length - 1];
                    Array.Copy(read_data, 1, text, 0, read_data.Length - 1);

                    for (int i = 0; i <= text.Length - 1; i++)
                    {
                        data += text[i] + Environment.NewLine;
                    }
                    data = $"[출석 인원 : {text.Length + 1}]{Environment.NewLine}{data}{name}/{birth}/{date.ToString("T")}";
                    File.WriteAllText(filePath, data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
