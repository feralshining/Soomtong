using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace KioskBGRC
{
    public partial class MainMenu : MetroFramework.Forms.MetroForm
    {
        private static QRdata QR = new QRdata();
        private static ExcelData EXCEL = new ExcelData();
        private static FreeLunch FREE = new FreeLunch();
        private static KioskUI UI = new KioskUI();
        public static string User_name
        {
            get => FreeLunch.user_name;
            set => FreeLunch.user_name = value;
        }
        public static string User_birth
        {
            get => FreeLunch.user_birth;
            set { FreeLunch.user_birth = value; SearchInExcel(); }
        }

        private string portName = "";

        private Panel bgPanel = null;
        private string bgPanel_img = "";

        private static Panel newsPanel = null, imgPanel = null;
        private static Label newsTitle, newsContent;

        private static ImageSlideShow slideShow;
        private static BlendPanel mainPanel = null;
        private static Bitmap[] blendImg = null;
        private PanelSlide slideUp, slideDown;

        private int currentNewsIndex = 0;
        private List<(string mainImage, string titlePath, string contentPath, string[] contentImages)> newsList;

        public MainMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void MainMenu_Load(object sender, EventArgs s)
        {
            this.Opacity = 0;
            Initialize();
            //InitializeTest();
        }
        //private async void InitializeTest()
        //{
        //    MessageMenu form_message = new MessageMenu();
        //    try
        //    {
        //        #region "[     Config.json 로드     ]"
        //        if (!NetworkInterface.GetIsNetworkAvailable()) throw new Exception("인터넷에 연결되어 있지 않습니다. 연결 상태를 확인해주세요.");
        //        string configPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC\\config.json";
        //        if (!File.Exists(configPath)) throw new FileNotFoundException("키오스크 설정 파일이 존재하지 않습니다.");
        //        try
        //        {
        //            using (StreamReader file = new StreamReader(configPath))
        //            {
        //                JObject data = (JObject)JToken.ReadFrom(new JsonTextReader(file));

        //                if (data.ContainsKey("RESOURCE_PATH")) UI.LoadResource(data["RESOURCE_PATH"].ToString());
        //                else throw new FileNotFoundException("리소스 폴더의 경로가 설정되지 않았습니다.");

        //                if (data.ContainsKey("PROGRAM_PATH")) UI.LoadProgram(data["PROGRAM_PATH"].ToString());
        //                else throw new FileNotFoundException("사업 종류 폴더 경로가 설정되지 않았습니다.");

        //                if (data.ContainsKey("BANNER_MAIN")) bgPanel_img = data["BANNER_MAIN"].ToString();
        //                else throw new FileNotFoundException("메인 배너가 설정되지 않았습니다.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            HandleException(ex);
        //            return;
        //        }
        //        #endregion

        //        #region "[     UI 설정 세팅     ]"
        //        this.WindowState = FormWindowState.Minimized;

        //        Thread t = new Thread((ThreadStart)delegate { UI.FormShow(form_message); Application.Run(); });
        //        t.SetApartmentState(ApartmentState.STA);
        //        t.Start();
        //        #endregion
        //        #region "[     QR 수신 준비, 및 엑셀, 화면 세팅 로드     ]"
        //        await Task.Run(() =>
        //        {
        //            if (form_message.InvokeRequired)
        //                form_message.Invoke(new MethodInvoker(() => form_message.Close()));
        //            else
        //                form_message.Close();
        //            SetScreen(); //화면 세팅
        //            if (this.InvokeRequired) this.Invoke(new MethodInvoker(() => InitializeNewsPanel()));
        //            else InitializeNewsPanel();
        //        });
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        form_message.Close();
        //        HandleException(ex);
        //        return;
        //    }
        //}
        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                QR.Close();
                DisposeBlending();
                UI.UnloadProgram(UI.PROGRAMPATH);
                Application.Exit();
            }
            catch (Exception)
            {
                DisposeBlending();
                UI.UnloadProgram(UI.PROGRAMPATH);
                Application.Exit();
            }
        }

        /// <summary>
        /// 초기 설정 메소드입니다.
        /// </summary>
        private async void Initialize()
        {
            MessageMenu form_message = new MessageMenu();
            try
            {
                #region "[     Config.json 로드     ]"
                if (!NetworkInterface.GetIsNetworkAvailable()) throw new Exception("인터넷에 연결되어 있지 않습니다. 연결 상태를 확인해주세요.");
                string configPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\KioskBGRC\\config.json";
                if (!File.Exists(configPath)) throw new FileNotFoundException("키오스크 설정 파일이 존재하지 않습니다.");
                try
                {
                    using (StreamReader file = new StreamReader(configPath))
                    {
                        JObject data = (JObject)JToken.ReadFrom(new JsonTextReader(file));

                        if (data.ContainsKey("RESOURCE_PATH")) UI.LoadResource(data["RESOURCE_PATH"].ToString());
                        else throw new FileNotFoundException("리소스 폴더의 경로가 설정되지 않았습니다.");

                        if (data.ContainsKey("PROGRAM_PATH")) UI.LoadProgram(data["PROGRAM_PATH"].ToString());
                        else throw new FileNotFoundException("뉴스레터 폴더 경로가 설정되지 않았습니다.");

                        if (data.ContainsKey("BANNER_MAIN")) bgPanel_img = data["BANNER_MAIN"].ToString();
                        else throw new FileNotFoundException("메인 배너가 설정되지 않았습니다.");

                        if (data.ContainsKey("SERIAL_PORT")) portName = data["SERIAL_PORT"].ToString();
                        else throw new FileNotFoundException("시리얼 포트가 설정되지 않았습니다.");
                        if (portName == null) throw new FileNotFoundException("시리얼 포트가 설정되지 않았습니다.");

                        if (data.ContainsKey("START_TIME")) FreeLunch.startchk_time = data["START_TIME"].ToString();
                        else throw new NullReferenceException("체크 시작 시간이 설정되지 않았습니다.");
                        if (data.ContainsKey("END_TIME")) FreeLunch.endchk_time = data["END_TIME"].ToString();
                        else throw new NullReferenceException("체크 마감 시간이 설정되지 않았습니다.");

                        if (data.ContainsKey("LIST_PATH")) FreeLunch.userlist_path = data.GetValue("LIST_PATH").ToString();
                        else throw new FileNotFoundException("명단 파일의 경로가 설정되지 않았습니다.");

                        if (data.ContainsKey("LUNCHLOG_DIR_PATH")) FreeLunch.lunchlog_path = data.GetValue("LUNCHLOG_DIR_PATH").ToString();
                        else throw new FileNotFoundException("일일 식사내역 폴더의 경로가 설정되지 않았습니다.");
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                    return;
                }
                #endregion
                #region "[     UI 설정 세팅     ]"
                this.WindowState = FormWindowState.Minimized;

                form_message.panel1.BackColor = Color.Gray;
                form_message.label_title.Font = UI.SetFont(0, 22, FontStyle.Regular);
                form_message.label_title.Text = "알림";
                form_message.Controls.Add(UI.SetMessage(0, 43, FontStyle.Regular, "초기 설정 중입니다", 90, 200));

                Thread t = new Thread((ThreadStart)delegate { UI.FormShow(form_message); Application.Run(); });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                #endregion
                #region "[     QR 수신 준비, 및 엑셀, 화면 세팅 로드     ]"
                await Task.Run(() =>
                {
                    EXCEL.Initialize(FreeLunch.userlist_path); //엑셀 메모리 로드

                    if (form_message.InvokeRequired)
                        form_message.Invoke(new MethodInvoker(() => form_message.Close()));
                    else
                        form_message.Close();

                    SetQRData(); // QR 수신 준비

                    SetScreen(); //화면 세팅
                    if (this.InvokeRequired) this.Invoke(new MethodInvoker(() => InitializeNewsPanel()));
                    else InitializeNewsPanel();
                });
                #endregion
            }
            catch (Exception ex)
            {
                form_message.Close();
                HandleException(ex);
                return;
            }
        }
        /// <summary>
        /// 예외 상황이 발생했을 경우, 로그를 생성하고 예외 관련 정보를 표시하는 메소드입니다.
        /// </summary>
        public static void HandleException(Exception ex)
        {
            if (ex is NullReferenceException || ex is FileNotFoundException || ex is InvalidOperationException || ex is ExternalException || ex is IOException)
            {
                MessageBox.Show(new MainMenu() { TopMost = true }, ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                UI.OpenSettingMenu();
                return;
            }
            else if (NetworkInterface.GetIsNetworkAvailable() == false)
            {
                UI.CreateLog($"{UI.GetCurrentTime()}{Environment.NewLine}{"인터넷에 연결되어 있지 않습니다. 연결 상태를 확인해주세요."}{Environment.NewLine}");
                MessageBox.Show(new MainMenu() { TopMost = true },ex.Message + Environment.NewLine + "관리자에게 문의하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UI.CreateLog($"{UI.GetCurrentTime()}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}{ex.Message}{Environment.NewLine}");
                MessageBox.Show(new MainMenu() { TopMost = true }, ex.Message + Environment.NewLine + "관리자에게 문의하세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }

        /// <summary>
        /// 블렌딩된 이미지의 리소스를 해제하는 메소드입니다.
        /// </summary>
        public static void DisposeBlending()
        {
            if (blendImg != null)
            {
                foreach (var img in blendImg)
                {
                    img?.Dispose();
                }
            }
        }

        #region "[     무료급식사업 코드     ]"
        /// <summary>
        /// 엑셀 파일 내에서 프로퍼티 값을 확인하고 결과를 도출하는 메소드입니다.
        /// </summary>
        private static void SearchInExcel()
        {
            try
            {
                if (FREE.IsAdmin(User_name, User_birth))
                {
                    UI.OpenSettingMenu();
                    return;
                }
                if (FREE.IsInvalid(User_name, User_birth))
                {
                    ShowErrorMessage(-1);
                    return;
                }
                if (FREE.IsChkTime())
                {
                    Form msgForm = new Form
                    {
                        FormBorderStyle = FormBorderStyle.None,
                        Opacity = 0.8,
                        Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                        BackgroundImage = UI.FastLoadIMG(Path.Combine(UI.RESOURCEPATH, "msgBanner.png")),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Location = new Point(0, 0),
                        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
                    };
                    Panel logoPanel = new Panel
                    {
                        Size = new Size(200, 150),
                        Location = new Point(440, 600),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        BackColor = Color.Transparent,
                        BackgroundImage = UI.ResizeImage(UI.FastLoadIMG(Path.Combine(UI.RESOURCEPATH, "logo.png")),200,150),
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    Label titleLabel1 = new Label
                    {
                        Text = "체크 가능 시간에",
                        AutoSize = true,
                        Location = new Point(340, 750),
                        Font = UI.SetFont(1, 40, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.Transparent
                    };
                    Label titleLabel2 = new Label
                    {
                        Text = "다시 시도해 주세요",
                        AutoSize = true,
                        Location = new Point(318, 820),
                        Font = UI.SetFont(1, 42, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.Transparent
                    };

                    Label chkTimeLbl = new Label
                    {
                        Text = FreeLunch.startchk_time,
                        AutoSize = true,
                        Location = new Point(100, 920),
                        Font = UI.SetFont(1, 60, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.Transparent
                    };

                    Label waveLbl = new Label
                    {
                        Text = "~",
                        AutoSize = true,
                        Location = new Point(500, 920),
                        Font = UI.SetFont(1, 60, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.Transparent
                    };

                    Label endTimeLbl = new Label
                    {
                        Text = FreeLunch.endchk_time,
                        AutoSize = true,
                        Location = new Point(600, 920),
                        Font = UI.SetFont(1, 60, FontStyle.Bold),
                        ForeColor = Color.White,
                        BackColor = Color.Transparent
                    };
                    msgForm.Controls.AddRange(new Control[] { logoPanel, titleLabel1, titleLabel2, chkTimeLbl, waveLbl, endTimeLbl });
                    ShowMessageForm(msgForm);
                    return;
                }
                if (FREE.IsUser(User_name, User_birth))
                {
                    ShowWelcomeMessage();
                    return;
                }
                else
                {
                    ShowErrorMessage(0);
                    return;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return;
            }
        }

        /// <summary>
        /// QR 데이터 값이 유효할 경우, 환영 메시지를 띄우는 메소드입니다.
        /// </summary>
        private static void ShowWelcomeMessage()
        {
            Form msgForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                Opacity = 0.8,
                Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                BackgroundImage = UI.FastLoadIMG(Path.Combine(UI.RESOURCEPATH, "msgBanner.png")),
                BackgroundImageLayout = ImageLayout.Stretch,
                Location = new Point(0, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            Panel logoPanel = new Panel
            {
                Size = new Size(200, 150),
                Location = new Point(440, 600),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BackgroundImage = UI.ResizeImage(UI.FastLoadIMG(Path.Combine(UI.RESOURCEPATH, "logo.png")), 200, 150),
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            Label titleLabel = new Label
            {
                Text = "안녕하세요",
                AutoSize = true,
                Location = new Point(200, 780),
                Font = UI.SetFont(1, 60, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            if (FREE.IsContainText(User_name, User_birth) == 1)
            {
                titleLabel.Text = "이미 처리된 카드입니다";
                titleLabel.Location = new Point(160, 780);
                Label subLabel = new Label
                {
                    Text = "식당으로 입장해 주세요",
                    AutoSize = true,
                    Location = new Point(280, 900),
                    Font = UI.SetFont(1, 40, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.Transparent
                };
                msgForm.Controls.AddRange(new Control[] { logoPanel, logoPanel, titleLabel, subLabel });
                ShowMessageForm(msgForm);
                return;
            }

            QR.Speak(User_name);

            Label nameLabel = new Label
            {
                Text = User_name + "님",
                AutoSize = true,
                Location = new Point(580, 780),
                Font = UI.SetFont(1, 60, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            Label descLabel = new Label
            {
                Text = "저희 복지관을 찾아주셔서 감사합니다",
                AutoSize = true,
                Location = new Point(130, 900),
                Font = UI.SetFont(1, 40, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            msgForm.Controls.AddRange(new Control[] { logoPanel, logoPanel, titleLabel, nameLabel, descLabel });
            ShowMessageForm(msgForm);
            FREE.CreateText(User_name, User_birth);
        }

        /// <summary>
        /// QR 데이터 값이 유효하지 않을 경우, 등록되지 않은 이용자를 표시하는 메소드입니다.
        /// </summary>
        private static void ShowErrorMessage(int num)
        {
            Form msgForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                Opacity = 0.85,
                Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                BackgroundImage = UI.FastLoadIMG(Path.Combine(UI.RESOURCEPATH, "msgBanner.png")),
                BackgroundImageLayout = ImageLayout.Stretch,
                Location = new Point(0, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            Panel logoPanel = new Panel
            {
                Size = new Size(200, 150),
                Location = new Point(440, 600),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                BackgroundImage = UI.ResizeImage(UI.FastLoadIMG(Path.Combine(UI.RESOURCEPATH, "logo.png")),200,150),
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            Label titleLabel1 = new Label
            {
                Text = "등록된 이용자가 아닙니다",
                AutoSize = true,
                Location = new Point(110, 780),
                Font = UI.SetFont(1, 60, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            Label titleLabel2 = new Label
            {
                Text = "유효한 데이터가 아닙니다",
                AutoSize = true,
                Location = new Point(110, 780),
                Font = UI.SetFont(1, 60, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            Label titleLabel3 = new Label
            {
                Text = "복지관 직원에게 문의해 주세요",
                AutoSize = true,
                Location = new Point(295, 880),
                Font = UI.SetFont(1, 30, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            if (num == 0) msgForm.Controls.AddRange(new Control[] { logoPanel, titleLabel1, titleLabel3 });
            if (num == -1) msgForm.Controls.AddRange(new Control[] { logoPanel, titleLabel2 });

            UI.PlayWarningSound(2);
            ShowMessageForm(msgForm);
        }

        /// <summary>
        /// 메시지 창을 호출하는 메소드입니다.
        /// </summary>
        private static void ShowMessageForm(Form newForm)
        {
            Thread t = new Thread((ThreadStart)delegate
            {
                UI.FormShow(newForm);
                Application.Run();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            Thread.Sleep(1500);

            if (newForm.InvokeRequired) newForm.Invoke(new Action(() => newForm.Close()));
            else newForm.Close();
        }
        #endregion
        #region "[     키오스크 메인 코드     ]"

        /// <summary>
        /// QR 데이터를 수신 받고 데이터를 제어하는 메소드입니다.
        /// </summary>
        private void SetQRData()
        {
            try
            {
                QR.Initialize(portName);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return;
            }
        }

        /// <summary>
        /// 초기 화면을 세팅하는 메소드입니다.
        /// </summary>
        private void SetScreen()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(SetScreen));
                return;
            }
            try
            {
                if (String.IsNullOrWhiteSpace(bgPanel_img)) throw new FileNotFoundException("메인 배너의 경로가 지정되지 않았습니다.");

                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Height = Screen.PrimaryScreen.Bounds.Height;
                this.Location = new Point(0, 0);

                #region "[     메인 화면 설정     ]"    
                //백그라운드 배경 설정
                bgPanel = new Panel
                {
                    Size = new Size(this.Width, this.Height),
                    Location = new Point(0, 0),
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackgroundImage = UI.FastLoadIMG(bgPanel_img) //UI.ResizeImage(UI.FastLoadIMG(bgPanel_img), this.Width, this.Height)
                };
                //VOL 연도 설정 (윈도우 기반 파싱)
                Label parsedYear = new Label
                {
                    Parent = bgPanel,
                    Text = DateTime.Now.Year.ToString(),
                    Location = new Point(145, 810),
                    AutoSize = true,
                    Font = UI.SetFont(2, 70, FontStyle.Bold),
                    ForeColor = Color.Black,
                    BackColor = Color.Transparent
                };
                //복지관 접속 QR 이미지 설정
                PictureBox bgrcImgQR = new PictureBox
                {
                    Parent = bgPanel,
                    Size = new Size(150, 150),
                    Location = new Point(120, 1040),
                    BackColor = Color.Transparent,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    BackgroundImage = UI.ResizeImage(UI.FastLoadIMG(UI.RESOURCEPATH + "\\" + "북구장복 사이트 접속 QR.png"),150, 150)
                };
                //메인 이미지 패널 설정
                mainPanel = new BlendPanel
                {
                    Parent = bgPanel,
                    Size = new Size(649, 1028),
                    Location = new Point(377, 584),
                    BackgroundImageLayout = ImageLayout.Stretch
                };
                #endregion

                this.Controls.AddRange(new Control[] { bgPanel });
                bgPanel.BringToFront();
                mainPanel.Click += (s, e) => mainPanel_Click(s, e);
                LoadNewsList();
                LoadMainImage();
                this.Opacity = 1;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return;
            }
        }
        
        /// <summary>
        /// 뉴스레터 폴더 내부에 존재하는 하위 폴더 및 내용을 전부 검색해서 리스트화 시킵니다.
        /// </summary>
        private void LoadNewsList()
        {
            string[] folderPaths = DirectoryScanner.GetNewsFolders(UI.PROGRAMPATH).ToArray();
            newsList = DirectoryScanner.GetNewsFilesMultiple(folderPaths);
        }

        /// <summary>
        /// 메인 이미지 클릭 시 띄워지는 뉴스레터 페이지를 초기에 설정하는 메소드입니다.
        /// </summary>
        private void InitializeNewsPanel()
        {
            newsPanel = new Panel
            {
                Size = new Size(this.Width, this.Height),
                Location = new Point(0, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = UI.ResizeImage(UI.FastLoadIMG(UI.RESOURCEPATH + "\\" + "newsBanner.png"),this.Width, this.Height),
                Visible = false, // 처음엔 숨김
            };

            Label subTitle = new Label
            {
                Parent = newsPanel,
                Text = "|  월간 뉴스레터 " + DateTime.Today.Month + "월호",
                Location = new Point(776, 535),
                AutoSize = true,
                Font = UI.SetFont(5, 18, FontStyle.Regular),
                ForeColor = Color.FromArgb(54, 33, 28),
                BackColor = Color.Transparent
            };

            newsTitle = new Label
            {
                Parent = newsPanel,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(981, 151),
                Location = new Point(50, 596),
                Font = UI.SetFont(3, 30, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };

            imgPanel = new Panel
            {
                Parent = newsPanel,
                Size = new Size(765, 429),
                Location = new Point(158, 811),
                BackgroundImageLayout = ImageLayout.Stretch,
            };

            newsContent = new Label
            {
                Parent = newsPanel,
                Size = new Size(861, 326),
                TextAlign = ContentAlignment.TopLeft,
                Location = new Point(107, 1288),
                Font = UI.SetFont(4, 20, FontStyle.Regular),
                ForeColor = Color.Black,
                BackColor = Color.Transparent
            };
            newsPanel.Click += (s, ev) => newsPanel_Click(s, ev);
            this.Controls.Add(newsPanel);

            slideUp = new PanelSlide(newsPanel, 1920, 0, true, true);
            slideDown = new PanelSlide(newsPanel, 0, 1920, false, false);
        }

        /// <summary>
        /// 사진 클릭 시 뉴스레터 페이지를 블렌딩 인덱스에 맞춰서 띄우는 메소드입니다.
        /// </summary>
        private void SetNewsPanel((string mainImage, string titlePath, string contentPath, string[] contentImages) newsData)
        {
            try
            {
                newsTitle.Text = NewsParser.ReadTextFile(newsData.titlePath);
                newsContent.Text = NewsParser.ReadTextFile(newsData.contentPath);
                imgPanel.BackgroundImage = newsData.contentImages.Length > 0 ? UI.ResizeImage(UI.FastLoadIMG(newsData.contentImages[0]),imgPanel.Width, imgPanel.Height) : null;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("월간 뉴스레터 폴더가 비어있거나 찾을 수 없습니다: " + ex.Message);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            newsPanel.Visible = true;
            newsPanel.Location = new Point(0, 1920);
            newsPanel.BringToFront();

            slideUp.Start();
        }

        /// <summary>
        /// 사진을 터치할 경우, 초기 설정 시에 메모리에 적재했던 페이지를 보여줍니다.
        /// </summary>
        private void mainPanel_Click(object sender, EventArgs e)
        {
            if (mainPanel.BackgroundImage == null) return;
            SetNewsPanel(newsList[currentNewsIndex]);
        }

        /// <summary>
        /// 메인 화면에서 사진 클릭 시 띄워지는 페이지 뷰를 클릭했을 경우 
        /// 숨김 처리 후에 리소스를 해제하는 메소드입니다.
        /// </summary>
        private void newsPanel_Click(object sender, EventArgs e)
        {
            if (!newsPanel.Visible) return;
            newsPanel.Location = new Point(0, 0); // 초기 위치 설정
            slideDown.Start();
        }

        /// <summary>
        /// 홍보 폴더 내에 있는 모든 이미지 파일을 읽고
        /// 그 중 "메인 사진" 라는 파일명을 가진 이미지만 블렌딩 처리하여 표시하는 메소드입니다.
        /// </summary>
        private void LoadMainImage()
        {
            if (newsList == null || !newsList.Any()) throw new FileNotFoundException("뉴스 데이터가 비어 있습니다.");
            
            var imageFiles = newsList
                             .Where(news => !string.IsNullOrEmpty(news.mainImage) && File.Exists(news.mainImage))
                             .Select(news => news.mainImage)
                             .ToList();
            if (imageFiles.Count == 0) throw new FileNotFoundException("메인 이미지를 찾을 수 없습니다.");

            try
            {
                BlendingImage(imageFiles.ToArray(), mainPanel, 4000);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// 이미지를 bitmap 변수에 복사하여 panel로 띄우고, blend 효과를 줍니다.
        /// </summary>
        /// <param name="fileName">이미지 파일 경로가 담겨있는 문자열 배열 변수입니다.</param>
        /// <param name="panel">블렌딩을 적용할 패널입니다.</param>
        /// <param name="interval">슬라이드를 넘길 시간입니다.</param>
        private void BlendingImage(string[] fileName, BlendPanel panel, int interval)
        {
            try
            {
                blendImg = new Bitmap[fileName.Length];
                for (int i = 0; i < fileName.Length; i++)
                {
                    if (!File.Exists(fileName[i])) throw new FileNotFoundException("이미지 파일을 찾을 수 없습니다: " + fileName[i]);

                    // 이미지를 로드하고 회전 처리 (FastLoadIMG에서 회전 처리)
                    var image = UI.FastLoadIMG(fileName[i]);
                    blendImg[i] = new Bitmap(image);  // 회전된 이미지를 Bitmap으로 저장
                }

                slideShow = new ImageSlideShow(blendImg, panel, interval);

                slideShow.OnImageChanged += (index) =>
                {
                    currentNewsIndex = index % newsList.Count;

                    // 뉴스 이미지 로드 및 크기 조정 후 회전 적용 (FastLoadIMG에서 처리)
                    var image = UI.FastLoadIMG(newsList[currentNewsIndex].mainImage);
                    mainPanel.BackgroundImage = UI.ResizeImage(image, mainPanel.Width, mainPanel.Height);
                };

                slideShow.Start();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }


        #endregion
    }
}