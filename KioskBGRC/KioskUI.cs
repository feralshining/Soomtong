using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Media;
using Timer = System.Windows.Forms.Timer;

namespace KioskBGRC
{
    #region "[     키오스크 UI 클래스    ]"
    public class KioskUI
    {
        // 캐시를 위한 딕셔너리 선언 (경로 -> 이미지)
        private static Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
        public string RESOURCEPATH = "", PROGRAMPATH = "";
        readonly List<PrivateFontCollection> fonts = new List<PrivateFontCollection>
        {
            new PrivateFontCollection(), //여기어때 잘난체
            new PrivateFontCollection(), //PretendardVariable
            new PrivateFontCollection(), //RondalBold
            new PrivateFontCollection(), //원스토어 모바일 제목
            new PrivateFontCollection(), //원스토어 모바일 본문
            new PrivateFontCollection()  //학교안심 출석부
        };
        /// <summary>
        /// 리소스 폴더의 경로를 로컬 경로로 복사하여 복사된 폴더를 사용합니다.
        /// </summary>
        public void LoadResource(string filePath)
        {
            string dirName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            string srcDir = filePath;
            string destDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC\\{dirName}";

            DeleteDirectory(destDir); //기존 폴더가 있다면 제거
            CopyDirectory(srcDir, destDir);
            RESOURCEPATH = filePath;
        }
        
        /// <summary>
        /// 프로그램 홍보 관련 폴더를 로컬 경로로 복사하여 복사된 폴더를 사용합니다.
        /// </summary>
        public void LoadProgram(string filePath)
        {
            string dirName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
            string srcDir = filePath;
            string destDir = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC\\{dirName}";

            DeleteDirectory(destDir); //기존 폴더가 있다면 제거
            CopyDirectory(srcDir, destDir);
            PROGRAMPATH = destDir;
        }
        /// <summary>
        /// 로컬 경로로 복사한 프로그램 홍보 관련 폴더를 제거합니다.
        /// </summary>
        public void UnloadProgram(string filePath) => DeleteDirectory(filePath);
   
        /// <summary>
        /// 폴더를 복사하는 메소드입니다.
        /// </summary>
        public static void CopyDirectory(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            string[] folders = Directory.GetDirectories(sourceFolder);

            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }

            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyDirectory(folder, dest);
            }
        }
        /// <summary>
        /// 인자값으로 받은 특정 폴더의 경로에서 해당 폴더를 제거합니다.
        /// </summary>
        public void DeleteDirectory(string filePath)
        {
            if (!Directory.Exists(filePath)) return;
            DirectoryInfo dir = new DirectoryInfo(filePath);
            dir.Attributes = FileAttributes.Normal;
            dir.Delete(true);
        }
        public Font SetFont(int fontIndex, int size, FontStyle style)
        {
            string fontName = "";
            try
            {
                switch (fontIndex)
                {
                    case 0:
                        fontName = "Jalnan2TTF.ttf";
                        break;
                    case 1:
                        fontName = "PretendardVariable.ttf";
                        break;
                    case 2:
                        fontName = "RondalBold.otf";
                        break;
                    case 3:
                        fontName = "ONE Mobile Title.ttf";
                        break;
                    case 4:
                        fontName = "ONE Mobile Regular.ttf";
                        break;
                    case 5:
                        fontName = "Hakgyoansim Chulseokbu TTF L.ttf";
                        break;
                }
                fonts[fontIndex].AddFontFile($"{RESOURCEPATH}\\{fontName}");
                return new Font(fonts[fontIndex].Families[0], size, style);
            }
            catch
            {
                throw new FileNotFoundException($"{fontName} 폰트 파일을 찾을 수 없습니다.");
            }
        }
        /// <summary>
        /// 설정한 라벨 값을 반환합니다.
        /// </summary>
        public Label SetMessage(int fontIndex, int size, FontStyle style, string text, int x, int y)
        {
            Label msg = new Label();
            msg.Font = SetFont(fontIndex, size, style);
            msg.Text = text;
            msg.AutoSize = true;
            msg.Location = new Point(x, y);

            return msg;
        }

        /// <summary>
        /// 입력받은 Form을 업데이트하고 창 우선 순위에서 최상위로 설정해서 보여줍니다.
        /// </summary>
        public void FormShow(Form form)
        {
            form.TopMost = true;
            form.WindowState = FormWindowState.Normal;
            form.Show();
            form.Update();
            form.BringToFront();
            form.Activate();
        }

        /// <summary>
        /// 경고음을 호출하는 메소드입니다.
        /// </summary>
        public async void PlayWarningSound(int count)
        {
            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < count; i++)
                    {
                        SoundPlayer player = new SoundPlayer(RESOURCEPATH + "\\" + "warning.wav");
                        player.Play();
                        Thread.Sleep(1000);
                        player.Stop();
                        player.Dispose();
                    }
                });
            }
            catch
            {
            }
        }

        /// <summary>
        /// 경로에 존재하는 이미지 파일을 읽어서 exif 정보를 확인 후
        /// 회전 정보가 존재한다면 정방향으로 되돌린 image 객체로 반환합니다.
        /// </summary>
        /// <param name="path">Stream으로 읽을 이미지 파일의 경로</param>
        /// <returns></returns>
        public Image AutoOrientation(string path)
        {
            const int EXIF_ORIENTATION = 0x0112; // 274

            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (Image img = Image.FromStream(stream, true, false)) // EXIF 데이터 유지
                {
                    // EXIF Orientation 정보 확인
                    if (img.PropertyIdList.Contains(EXIF_ORIENTATION))
                    {
                        var orientationProperty = img.GetPropertyItem(EXIF_ORIENTATION);
                        int orientation = orientationProperty.Value[0];

                        // Orientation에 따라 이미지 회전
                        switch (orientation)
                        {
                            case 2: img.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                            case 3: img.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                            case 4: img.RotateFlip(RotateFlipType.Rotate180FlipX); break;
                            case 5: img.RotateFlip(RotateFlipType.Rotate90FlipX); break;
                            case 6: img.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                            case 7: img.RotateFlip(RotateFlipType.Rotate270FlipX); break;
                            case 8: img.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                        }
                    }

                    // 회전 적용된 이미지를 새 Bitmap으로 변환하여 반환
                    return new Bitmap(img);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public Image FastLoadIMG(string path)
        {
            // 캐시에 이미지가 있으면 캐시된 이미지를 반환
            if (imageCache.ContainsKey(path)) return imageCache[path];

            Image image = AutoOrientation(path); // EXIF 회전 적용된 이미지 반환

            // 캐시에 저장할 때 기존 이미지가 있다면 해제
            if (imageCache.TryGetValue(path, out Image existingImage))
            {
                existingImage.Dispose();
                imageCache.Remove(path);
            }

            imageCache[path] = image;  // 새로운 이미지 캐싱
            return image;
        }



        /// <summary>
        /// 지정된 크기에 맞게 이미지를 자동으로 리사이징해서 반환합니다. (보간 모드 => 고품질로 설정)
        /// </summary>
        /// <param name="image">원본 이미지</param>
        /// <param name="width">리사이징할 너비</param>
        /// <param name="height">리사이징할 높이</param>
        /// <param name="keepAspectRatio">비율 유지 여부</param>
        /// <returns>리사이징된 이미지</returns>
        public Image ResizeImage(Image image, int width, int height, bool keepAspectRatio = true)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));

            // 이미지가 정상적으로 로드되었는지 확인
            if (image.Width == 0 || image.Height == 0)
            {
                throw new InvalidOperationException("Invalid image dimensions.");
            }

            int targetWidth = width;
            int targetHeight = height;
            if (keepAspectRatio)
            {
                double ratio = Math.Min((double)width / image.Width, (double)height / image.Height);
                targetWidth = (int)(image.Width * ratio);
                targetHeight = (int)(image.Height * ratio);
            }

            // 비트맵으로 크기 조정
            Bitmap resizedImage = new Bitmap(targetWidth, targetHeight);

            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic; // 이미지 크기 조정 시 부드럽게 보정
                //g.CompositingQuality = CompositingQuality.HighQuality; // 이미지 합성 품질을 높여 계단 현상 줄이기
                g.SmoothingMode = SmoothingMode.AntiAlias; // 곡선 및 경계선을 부드럽게 처리
                g.DrawImage(image, 0, 0, targetWidth, targetHeight);
            }

            return resizedImage;
        }


        /// <summary>
        /// 현재 시간을 반환합니다.
        /// </summary>
        public string GetCurrentTime() => DateTime.Now.ToString("G");

        /// <summary>
        /// 로그 파일을 생성합니다. 파일이 이미 존재한다면 줄을 추가합니다.
        /// </summary>
        public void CreateLog(string log)
        {
            string directory_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC";
            if (!Directory.Exists(directory_path)) Directory.CreateDirectory(directory_path);

            try
            {
                string savepath = $"{directory_path}\\Error_log_{DateTime.Now.ToString("d")}.txt";
                File.AppendAllText(savepath, log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 설정 창을 여는 메소드입니다.
        /// </summary>
        public void OpenSettingMenu()
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is SettingMenu)
                {
                    if (openForm.InvokeRequired)
                    {
                        openForm.Invoke(new MethodInvoker(() =>
                        {
                            openForm.Show();
                            openForm.BringToFront();
                        }));
                    }
                    else
                    {
                        openForm.Show();
                        openForm.BringToFront();
                    }
                    return;
                }
            }
            SettingMenu form_setting = new SettingMenu();
            try
            {
                form_setting.label_title.Font = SetFont(0, 22, FontStyle.Regular);

                #region "[     키오스크 설정     ]"
                form_setting.groupBox7.Font = SetFont(0, 9, FontStyle.Regular);
                //리소스 폴더 설정
                form_setting.groupBox1.Font = SetFont(0, 9, FontStyle.Regular);
                form_setting.txt_resource_path.Font = SetFont(0, 8, FontStyle.Regular);
                form_setting.btn_resrc.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_resrc.IconSize = 30;

                //메인 배너 설정
                form_setting.groupBox9.Font = SetFont(0, 9, FontStyle.Regular);
                form_setting.txt_bannerMain.Font = SetFont(0, 8, FontStyle.Regular);
                form_setting.btn_bannerMain.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_bannerMain.IconSize = 30;

                //프로그램 홍보 폴더 설정
                form_setting.groupBox2.Font = SetFont(0, 9, FontStyle.Regular);
                form_setting.txt_programPath.Font = SetFont(0, 8, FontStyle.Regular);
                form_setting.btn_program.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_program.IconSize = 30;

                //시리얼 포트 설정
                form_setting.groupBox8.Font = SetFont(0, 9, FontStyle.Regular);
                form_setting.label_qr_1.Font = SetFont(0, 8, FontStyle.Regular);
                form_setting.serialportCB.Font = SetFont(0, 10, FontStyle.Regular);
                #endregion
                #region "[     무료급식사업 설정     ]"
                form_setting.groupBox6.Font = SetFont(0, 9, FontStyle.Regular);
                //명단 위치 설정
                form_setting.groupBox4.Font = SetFont(0, 9, FontStyle.Regular);
                form_setting.txt_listfile_path.Font = SetFont(0, 8, FontStyle.Regular);
                form_setting.btn_file.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_file.IconSize = 30;

                //체크 시간 설정
                form_setting.groupBox5.Font = SetFont(0, 9, FontStyle.Regular);
                form_setting.label_chk_1.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.label_chk_2.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.timePicker1.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.timePicker2.Font = SetFont(0, 10, FontStyle.Regular);

                //일일 식사내역 저장 경로 설정
                form_setting.groupBox3.Font = SetFont(0, 9, FontStyle.Regular);
                form_setting.txt_dailylogPath.Font = SetFont(0, 8, FontStyle.Regular);
                form_setting.btn_savedailydir.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_savedailydir.IconSize = 30;
                #endregion
                #region "[     기본 설정     ]"
                //에러 디버그 폴더 확인
                form_setting.btn_debug.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_debug.IconSize = 30;

                //인터넷 연결 상태 확인
                form_setting.btn_network.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_network.IconSize = 30;

                //프로그램 종료
                form_setting.btn_exit.Font = SetFont(0, 10, FontStyle.Regular);
                form_setting.btn_exit.IconSize = 30;
                #endregion
                form_setting.Update();

                Thread th = new Thread((ThreadStart)delegate { FormShow(form_setting); Application.Run(); });
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            catch
            {
                //폰트 못 찾아서 예외 발생하면 그냥 실행시키셈.
                form_setting.Update();

                Thread th = new Thread((ThreadStart)delegate { FormShow(form_setting); Application.Run(); });
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
        }
    }
    #endregion
    #region "[     사각형 모서리 둥글게 깎는 클래스     ]"
    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 20;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            using (GraphicsPath path = RoundedRect(rect, CornerRadius))
            {
                this.Region = new Region(path);
                if (this.BackgroundImage != null)
                {
                    using (Image processedImage = CreateNonIndexedImageFromBitmap(this.BackgroundImage as Bitmap))
                    {
                        using (TextureBrush brush = new TextureBrush(processedImage))
                        {
                            g.FillPath(brush, path);
                        }
                    }
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        g.FillPath(brush, path);
                    }
                }
            }
        }

        private Image CreateNonIndexedImageFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                return null;

            Bitmap nonIndexedImage = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(nonIndexedImage))
            {
                g.DrawImage(bitmap, 0, 0, this.Width, this.Height);
            }

            return nonIndexedImage;
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            float r = radius;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, r, r, 180, 90);
            path.AddArc(bounds.Right - r, bounds.Top, r, r, 270, 90);
            path.AddArc(bounds.Right - r, bounds.Bottom - r, r, r, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
    public class RoundedTopPanel : Panel
    {
        public int CornerRadius { get; set; } = 20;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            using (GraphicsPath path = RoundedRectTop(rect, CornerRadius))
            {
                this.Region = new Region(path);

                // Clip to the rounded path
                g.SetClip(path);

                // Draw the background image
                if (this.BackgroundImage != null)
                {
                    DrawImageStretched(g, this.BackgroundImage, rect);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        g.FillPath(brush, path);
                    }
                }

                // Reset clipping
                g.ResetClip();
            }
        }

        private void DrawImageStretched(Graphics g, Image image, Rectangle bounds)
        {
            // Calculate the aspect ratio of the image
            float aspectRatio = (float)image.Width / image.Height;

            // Calculate the bounds to maintain aspect ratio and fit within the panel
            int width = bounds.Width;
            int height = (int)(width / aspectRatio);

            if (height > bounds.Height)
            {
                height = bounds.Height;
                width = (int)(height * aspectRatio);
            }

            int x = (bounds.Width - width) / 2;
            int y = (bounds.Height - height) / 2;

            g.DrawImage(image, new Rectangle(x, y, width, height));
        }

        private GraphicsPath RoundedRectTop(Rectangle bounds, int radius)
        {
            float r = radius;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, r, r, 180, 90); // 좌측 상단
            path.AddArc(bounds.Right - r, bounds.Top, r, r, 270, 90); // 우측 상단
            path.AddLine(bounds.Right, bounds.Top + r, bounds.Right, bounds.Bottom); // 우측 세로
            path.AddLine(bounds.Right, bounds.Bottom, bounds.Left, bounds.Bottom); // 하단 직선
            path.AddLine(bounds.Left, bounds.Bottom, bounds.Left, bounds.Top + r); // 좌측 세로
            path.CloseFigure();
            return path;
        }
    }
    public class RoundedBottomPanel : Panel
    {
        public int CornerRadius { get; set; } = 20;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            using (GraphicsPath path = RoundedRectBottom(rect, CornerRadius))
            {
                this.Region = new Region(path);
                if (this.BackgroundImage != null)
                {
                    using (Image processedImage = CreateNonIndexedImageFromBitmap(this.BackgroundImage as Bitmap))
                    {
                        using (TextureBrush brush = new TextureBrush(processedImage))
                        {
                            g.FillPath(brush, path);
                        }
                    }
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        g.FillPath(brush, path);
                    }
                }
            }
        }

        private Image CreateNonIndexedImageFromBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                return null;

            Bitmap nonIndexedImage = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(nonIndexedImage))
            {
                g.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            }

            return nonIndexedImage;
        }
        private GraphicsPath RoundedRectBottom(Rectangle bounds, int radius)
        {
            float r = radius;
            GraphicsPath path = new GraphicsPath();
            path.AddLine(bounds.Left, bounds.Top, bounds.Right, bounds.Top); // 상단 직선
            path.AddLine(bounds.Right, bounds.Top, bounds.Right, bounds.Bottom - r); // 우측 세로
            path.AddArc(bounds.Right - r, bounds.Bottom - r, r, r, 0, 90); // 우측 하단
            path.AddArc(bounds.Left, bounds.Bottom - r, r, r, 90, 90); // 좌측 하단
            path.CloseFigure();
            return path;
        }
    }
    #endregion
    #region "[     이미지 블렌딩 관련 클래스     ]"
    /// <summary>
    /// 두 개의 이미지를 블렌딩하여 부드럽게 전환하는 패널 클래스입니다.
    /// </summary>
    public class BlendPanel : Panel
    {
        private Image image1;
        private Image image2;
        private float blendFactor;

        /// <summary>
        /// 첫 번째 이미지 (배경 역할)
        /// </summary>
        public Image Image1
        {
            get => image1;
            set { image1 = value; Invalidate(); }
        }

        /// <summary>
        /// 두 번째 이미지 (블렌딩 효과 적용 대상)
        /// </summary>
        public Image Image2
        {
            get => image2;
            set { image2 = value; Invalidate(); }
        }

        /// <summary>
        /// 블렌딩 강도 (0 = 첫 번째 이미지만 보임, 1 = 두 번째 이미지만 보임)
        /// </summary>
        public float Blend
        {
            get => blendFactor;
            set { blendFactor = Math.Max(0, Math.Min(1, value)); Invalidate(); }
        }

        /// <summary>
        /// 블렌딩 효과를 적용하는 패널 생성자 (더블 버퍼링 활성화)
        /// </summary>
        public BlendPanel()
        {
            DoubleBuffered = true;
        }

        /// <summary>
        /// 패널을 다시 그릴 때 블렌딩된 이미지를 적용합니다.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (image1 == null || image2 == null) return;

            using (ImageAttributes attributes = new ImageAttributes())
            {
                // 첫 번째 이미지(배경) 그리기
                e.Graphics.DrawImage(image1, new Rectangle(0, 0, Width, Height));

                // 두 번째 이미지(블렌딩 효과 적용)
                ColorMatrix cm = new ColorMatrix { Matrix33 = blendFactor }; // Alpha 블렌딩
                attributes.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                e.Graphics.DrawImage(image2, new Rectangle(0, 0, Width, Height),
                                     0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, attributes);
            }
        }
    }

    /// <summary>
    /// 여러 이미지를 슬라이드쇼 형태로 자동 전환하는 클래스입니다.
    /// </summary>
    public class ImageSlideShow
    {
        private int count = 0;
        private readonly Bitmap[] pictures;
        private readonly Timer timer;
        private readonly BlendPanel blendPanel;

        public event Action<int> OnImageChanged; // 현재 이미지 인덱스 변경 이벤트

        /// <summary>
        /// 이미지 슬라이드쇼를 초기화합니다.
        /// </summary>
        /// <param name="images">전환할 이미지 배열</param>
        /// <param name="panel">이미지를 표시할 BlendPanel</param>
        /// <param name="interval">이미지 전환 간격(ms)</param>
        public ImageSlideShow(Bitmap[] images, BlendPanel panel, int interval)
        {
            pictures = images;
            blendPanel = panel;

            timer = new Timer { Interval = interval };
            timer.Tick += UpdateImage;
        }

        /// <summary>
        /// 슬라이드쇼를 시작합니다.
        /// </summary>
        public void Start()
        {
            if (pictures.Length < 2) return;

            count = 0;
            blendPanel.Image1 = pictures[count];
            blendPanel.Image2 = pictures[++count];
            blendPanel.Blend = 1.0f;  // 즉시 변경
            timer.Start();
        }

        /// <summary>
        /// 이미지 전환을 수행하며 블렌딩 효과를 적용합니다.
        /// </summary>
        private void UpdateImage(object sender, EventArgs e)
        {
            count = (count + 1) % pictures.Length;
            blendPanel.Image1 = blendPanel.Image2;
            blendPanel.Image2 = pictures[count];
            blendPanel.Blend = 0.0f;

            // 🔹 현재 블렌딩된 이미지 인덱스 이벤트 호출
            OnImageChanged?.Invoke(count);

            // 블렌딩 효과를 부드럽게 적용하는 보조 타이머 실행
            Timer blendTimer = new Timer { Interval = 50 }; // 블렌딩 속도 조절
            blendTimer.Tick += (s, args) =>
            {
                if (blendPanel.Blend < 1.0f)
                {
                    blendPanel.Blend += 0.06f; // 블렌딩 증가율
                    blendPanel.Invalidate();
                }
                else
                {
                    blendTimer.Stop();
                    blendTimer.Dispose();
                }
            };
            blendPanel.BackgroundImage = blendPanel.Image2;
            blendTimer.Start();
        }

        /// <summary>
        /// 슬라이드쇼를 중지하고 리소스를 해제합니다.
        /// </summary>
        public void DisposeImage()
        {
            timer.Dispose();
            blendPanel.Dispose();
        }
    }


    #endregion
    #region "[     패널 슬라이드 클래스     ]"
    /// <summary>
    /// 패널이 화면 아래에서 위로 올라오거나, 위에서 아래로 내려가는 애니메이션을 적용하는 클래스입니다.
    /// PPT의 "올라오기" 효과처럼 처음에는 빠르게 올라오고 점점 감속합니다.
    /// </summary>
    public class PanelSlide
    {
        private Panel panel;
        private Timer slideTimer;
        private int targetY;
        private int speed;
        private float easingFactor = 0.2f; // 감속 비율 (값이 낮을수록 더 부드럽게 멈춤)
        private int startY;
        private bool isSlidingUp, isVisible;

        /// <summary>
        /// PanelSlide 인스턴스를 생성합니다.
        /// </summary>
        /// <param name="panel">애니메이션을 적용할 패널</param>
        /// <param name="startY">애니메이션 시작 Y좌표</param>
        /// <param name="targetY">애니메이션 종료 Y좌표</param>
        /// <param name="isSlidingUp">위로 슬라이드(true) 또는 아래로 슬라이드(false) 여부</param>
        /// <param name="isVisible">슬라이드 완료 후 표시할 패널의 visible 값</param>
        public PanelSlide(Panel panel, int startY, int targetY, bool isSlidingUp, bool isVisible)
        {
            this.panel = panel;
            this.startY = startY;
            this.targetY = targetY;
            this.isSlidingUp = isSlidingUp;
            this.isVisible = isVisible;
            if(isSlidingUp)
                this.speed = Math.Abs(startY - targetY) / 10; // 초기 속도 설정
            else
                this.speed = Math.Abs(startY - targetY) / 10; // 초기 속도 설정

            panel.Location = new Point(panel.Left, startY); // 패널을 시작 위치에 배치

            slideTimer = new Timer { Interval = 10 };
            slideTimer.Tick += Slide;
        }

        /// <summary>
        /// 애니메이션을 시작합니다.
        /// </summary>
        public void Start()
        {
            slideTimer.Start();
        }

        /// <summary>
        /// 패널이 점점 감속하면서 올라오거나 내려가는 애니메이션 효과를 적용합니다.
        /// </summary>
        private void Slide(object sender, EventArgs e)
        {
            int distance = 0;
            if (isSlidingUp)
            {
                distance = panel.Top - targetY; // 남은 거리 계산
                if (Math.Abs(distance) <= 2)
                {
                    panel.Top = targetY;
                    slideTimer.Stop();
                    panel.Visible = isVisible;
                    return;
                }
            }
            else
            {
                distance = panel.Top;
                if (Math.Abs(distance) >= targetY)
                {
                    panel.Top = targetY;
                    slideTimer.Stop();
                    panel.Visible = isVisible;
                    return;
                }
            }
            // 남은 거리 비율에 따라 속도 조절
            if(isSlidingUp) speed = (int)(Math.Abs(distance) * easingFactor);
            else speed =  (int)(Math.Abs(distance) * easingFactor * 4);      
            speed = Math.Max(speed, 1); // 최소 이동 값 보장

            if (isSlidingUp) panel.Top -= speed; // 위로 슬라이드
            else panel.Top += speed; // 아래로 슬라이드          
        }
    }


    #endregion
}