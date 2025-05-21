using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskBGRC
{
    /// <summary>
    /// 폴더 내에서 뉴스 데이터를 검색하는 클래스
    /// </summary>
    public class DirectoryScanner
    {
        /// <summary>
        /// 지정된 경로 내의 모든 하위 뉴스 폴더를 검색하여 리스트로 반환
        /// </summary>
        public static List<string> GetNewsFolders(string basePath) => Directory.GetDirectories(basePath).ToList();

        /// <summary>
        /// 지정된 뉴스 폴더에서 필요한 파일 경로를 검색하여 반환
        /// </summary>
        public static (string mainImage, string titlePath, string contentPath, string[] contentImages) GetNewsFiles(string folderPath)
        {
            string mainImage = Directory.GetFiles(folderPath, "메인 사진.*")
                                        .FirstOrDefault(); // 파일이 있으면 첫 번째 파일을 선택

            string titlePath = Path.Combine(folderPath, "제목.txt");
            string contentPath = Path.Combine(folderPath, "내용.txt");

            // "내용 사진" 폴더 내의 모든 이미지 파일을 배열로 반환
            string contentDir = Path.Combine(folderPath, "내용 사진");
            string[] validExtensions = { ".png", ".jpg", ".jpeg", ".bmp", ".gif", ".tiff", ".webp" };

            string[] contentImages = Directory.Exists(contentDir)
                ? Directory.GetFiles(contentDir)
                          .Where(file => validExtensions.Contains(Path.GetExtension(file).ToLower()))
                          .OrderBy(f => f) // 이름순 정렬
                          .ToArray()
                : new string[0];

            return (mainImage, titlePath, contentPath, contentImages);
        }

        /// <summary>
        /// 여러 폴더를 받아 각 폴더에 대해 GetNewsFiles 호출
        /// </summary>
        public static List<(string mainImage, string titlePath, string contentPath, string[] contentImages)> GetNewsFilesMultiple(IEnumerable<string> folderPaths)
        {
            List<(string, string, string, string[])> results = new List<(string, string, string, string[])>();

            foreach (var folderPath in folderPaths)
            {
                var files = GetNewsFiles(folderPath);
                results.Add(files);
            }

            return results;
        }
    }

    /// <summary>
    /// 뉴스 파일을 읽어오는 클래스
    /// </summary>
    public class NewsParser
    {
        /// <summary>
        /// 지정된 텍스트 파일을 읽어 문자열로 반환
        /// </summary>
        public static string ReadTextFile(string filePath) => File.Exists(filePath) ? File.ReadAllText(filePath, Encoding.UTF8) : "파일 없음";
    }
}
