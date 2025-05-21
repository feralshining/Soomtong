using System;
using System.Linq;
using System.IO;
using OfficeOpenXml;

namespace KioskBGRC
{
    public class ExcelData
    {
        /// <summary>
        /// "명단" 시트에서 지정한 요소를 찾고 해당 위치를 "A1 참조 스타일"로 반환합니다.
        /// </summary>
        public string GetAddress(string filepath, string element)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (String.IsNullOrEmpty(filepath)) throw new FileNotFoundException("파일을 찾을 수 없습니다.");
            if (!File.Exists(filepath)) throw new FileNotFoundException("파일을 찾을 수 없습니다.");
            using (var pkg = new ExcelPackage(new FileInfo(filepath)))
            {
                return pkg.Workbook.Worksheets[0].Cells["A:Z"].Where(cell => cell.Text?.ToString() == element).ToArray()[0].Address;
                #region "[    Old Code     ]"
                //var query = ws.Cells["A:Z"].Where(cell => cell.Text?.ToString() == element)
                //foreach (var cell in query)
                //{
                //    address = cell.Address;
                //}
                //return address;
                #endregion
            } 
        }

        /// <summary>
        /// "명단" 시트에서 지정한 요소가 있는지 여부를 반환합니다.
        /// </summary>
        public bool IsContainExcel(string filepath, string element)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (String.IsNullOrEmpty(filepath)) throw new FileNotFoundException("파일을 찾을 수 없습니다.");
            if (!File.Exists(filepath)) throw new FileNotFoundException("파일을 찾을 수 없습니다.");
            using (var pkg = new ExcelPackage(new FileInfo(filepath)))
            {
                return pkg.Workbook.Worksheets[0].Cells["A:Z"].Where(cell => cell.Text?.ToString() == element).Any();
                #region "[     Old Code     ]"
                //var query = from cell in ws.Cells["A:Z"]
                //            where cell.Text?.ToString() == element
                //            select cell;
                #endregion
            }
        }
        /// <summary>
        /// 처음 메모리에 엑셀 파일을 로드해서 공간을 확보하기 위한 메소드입니다.
        /// </summary>
        public void Initialize(string filepath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (String.IsNullOrEmpty(filepath)) throw new FileNotFoundException("파일을 찾을 수 없습니다.");
            if (!File.Exists(filepath)) throw new FileNotFoundException("파일을 찾을 수 없습니다.");
            using (var pkg = new ExcelPackage(new FileInfo(filepath)))
            {
                // 메모리 공간 할당을 위해 파일을 열고 닫기만 하므로 추가적인 작업이 필요하지 않습니다.
            }
        }


        /// <summary>
        /// 파일이 생성된 오늘 날짜에 해당 요소가 기재되어 있는지 여부를 반환합니다. 
        /// 0은 중복이 아님, 1은 중복, -1은 파일이 존재하지 않음을 나타냅니다.
        /// </summary>
        public int IsContainText(string name, string birth)
        {
            DateTime date = DateTime.Now;
            string file_path = $"\\\\192.168.0.53\\1층 공유폴더\\무료급식사업\\무료급식 일일 식사내역\\무료급식 일일 식사내역_{date.ToString("yyyy.MM.dd")}.txt";
            try
            {
                if (!File.Exists(file_path)) return -1;

                string user_data = File.ReadAllText(file_path);
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
            DateTime date = DateTime.Now;
            string file_path = $"\\\\192.168.0.53\\1층 공유폴더\\무료급식사업\\무료급식 일일 식사내역\\무료급식 일일 식사내역_{date.ToString("yyyy.MM.dd")}.txt";
            string data = "";
            try
            {
                if (!File.Exists(file_path)) using (FileStream fs = File.Create(file_path)) { }

                string[] read_data = File.ReadAllLines(file_path);
                if (read_data.Length == 0)
                {
                    data = ($"[출석 인원 : {read_data.Length + 1}]{Environment.NewLine}{name}/{birth}/{date.ToString("T")}");
                    File.WriteAllText(file_path, data);
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
                    File.WriteAllText(file_path, data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
