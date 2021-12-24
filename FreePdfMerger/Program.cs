using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace PdfMergeSplitTool
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        const int ATTACH_PARENT_PROCESS = -1;
        const int ERROR_ACCESS_DENIED = 5;

        [STAThread]
        static void Main(string[] args)
        {
            Module.args = args;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //1remove
            // frmLanguage.SetLanguages();
            //  SetLanguage();

            if (args.Length > 0 && args[0].StartsWith("/uninstall"))
            {
                Module.DeleteApplicationSettingsFile();

                System.Diagnostics.Process.Start("https://www.4dots-software.com/support/bugfeature.php?uninstall=true&app=" + System.Web.HttpUtility.UrlEncode(Module.ShortApplicationTitle));

                //frmUninstallQuestionnaire fq = new frmUninstallQuestionnaire();
                //fq.ShowDialog();
                return;
                Environment.Exit(0);
            }
            ExceptionHandlersHelper.AddUnhandledExceptionHandlers();

            if (args.Length > 0)
            {
                //System.Threading.Thread.Sleep(2000);
            }

            if (ArgsHelper.IsCommandLine())
            {
                if (!AttachConsole(ATTACH_PARENT_PROCESS) && Marshal.GetLastWin32Error() == ERROR_ACCESS_DENIED)
                {
                    AllocConsole();
                }

                ArgsHelper.ExamineArgs();

                ArgsHelper.ExecuteCommandLine();

                Environment.Exit(0);
            }
            else
            {


                bool createdNew = true;
                using (Mutex mutex = new Mutex(true, "FreePDFSplitterMerger", out createdNew))
                {
                    if (!createdNew && args.Length > 0)
                    {
                        #region Send Arguments to Main Application
                        //args = new string[] { "-ImportImages C:\\a.txt C:\\b.txt" };

                        bool window_handle_not_zero = false;
                        while (!window_handle_not_zero)
                        {
                            Process[] procs = System.Diagnostics.Process.GetProcessesByName("FreePDFSplitterMerger");
                            for (int f = 0; f < procs.Length; f++)
                            {
                                if (System.Diagnostics.Process.GetCurrentProcess().Id != procs[f].Id)
                                {
                                    MessageHelper msg = new MessageHelper();
                                    int result = 0;
                                    IntPtr hWnd = procs[f].MainWindowHandle;

                                    if (hWnd != IntPtr.Zero && procs[f].Responding)
                                    {
                                        string arguments = "";
                                        for (int n = 0; n <= args.GetUpperBound(0); n++)
                                        {
                                            arguments += args[n] + "|||";
                                        }

                                        if (arguments != String.Empty)
                                        {
                                            result = msg.sendWindowsStringMessage(hWnd, IntPtr.Zero, arguments);
                                            MessageHelper.SetForegroundWindow(hWnd);
                                        }
                                        else
                                        {
                                            MessageHelper.SetForegroundWindow(hWnd);
                                        }

                                        window_handle_not_zero = true;

                                    }
                                }
                            }
                            if (!window_handle_not_zero)
                            {
                                MessageHelper.PostMessage((IntPtr)MessageHelper.HWND_BROADCAST, (UInt32)MessageHelper.WM_ACTIVATEAPP, IntPtr.Zero, IntPtr.Zero);

                                System.Threading.Thread.Sleep(300);
                                Application.DoEvents();
                            }
                        }

                        Environment.Exit(0);
                        return;


                        #endregion
                    }
                    else
                    {
                        if (frmMain.Instance == null)
                        {
                            Application.Run(new frmMain());
                        }
                        else
                        {
                            if (!frmMain.Instance.IsDisposed)
                            {
                                Application.Run(frmMain.Instance);
                            }
                        }
                    }
                }
            }
            Environment.Exit(0);
        }

        public static void SetLanguage()
        {
            RegistryKey key = Registry.CurrentUser;
            RegistryKey key2 = Registry.CurrentUser;
            string lang = "";

            try
            {
                key = key.OpenSubKey("Software\\4dots Software", true);

                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\4dots Software");
                }

                key2 = key.OpenSubKey("FreePDFSplitterMerger", true);

                if (key2 == null)
                {
                    key2 = key.CreateSubKey("FreePDFSplitterMerger");
                }

                key = key2;

                bool setlang = false;

                if (key.GetValue("Language") == null)
                {
                    frmLanguage fl = new frmLanguage();
                    fl.ShowDialog();

                    key.SetValue("Language", frmLanguage.SelectedLanguageCode);
                    setlang = true;

                }

                if (key != null && key.GetValue("Language") != null)
                {
                    lang = key.GetValue("Language").ToString();
                    Module.SelectedLanguage = lang;
                    if (lang == "en-US")
                    {
                        System.Threading.Thread.CurrentThread.CurrentUICulture =
                            System.Globalization.CultureInfo.InvariantCulture;

                        Application.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

                    }
                    else
                    {

                        try
                        {
                            System.Threading.Thread.CurrentThread.CurrentUICulture = new
                            System.Globalization.CultureInfo(lang);
                            try
                            {
                                Application.CurrentCulture = new System.Globalization.CultureInfo(lang);
                            }
                            catch { }
                        }
                        catch (Exception ex)
                        {
                            Module.ShowError(ex);
                        }
                    }
                }
                /*
                if (setlang)
                {
                    key.SetValue("Menu Item Caption", TranslateHelper.Translate("Remove PDF Password"));
                }*/

            }
            catch (Exception exr)
            {
                Module.ShowError(exr);
            }
            finally
            {
                key.Close();
                key2.Close();
            }


        }

    }
}