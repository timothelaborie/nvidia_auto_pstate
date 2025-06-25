using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace limit_nvpstate
{
    public partial class limitnvpstate : Form
    {
        private readonly Process inspector = new Process();
        private bool automaticSwitchingEnabled = true;

        public limitnvpstate()
        {
            InitializeComponent();
        }


        private void LoadSettings()
        {
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate"))
            {


                if (config.GetValue("IndexOfGPU") == null)
                {
                    config.SetValue("IndexOfGPU", "0", RegistryValueKind.String);
                }

                if (config.GetValue("StartMinimized") == null)
                {
                    config.SetValue("StartMinimized", "False", RegistryValueKind.String);
                }

                if (config.GetValue("AutomaticSwitching") == null)
                {
                    config.SetValue("AutomaticSwitching", "True", RegistryValueKind.String);
                }

                gpuIndex.SelectedIndex = Convert.ToInt32(config.GetValue("IndexOfGPU"));
                startMinimizedToolStripMenuItem.Checked = Convert.ToBoolean(config.GetValue("StartMinimized"));

                // Set checkbox state and variable
                automaticSwitchingEnabled = Convert.ToBoolean(config.GetValue("AutomaticSwitching"));
                checkBox1.Checked = automaticSwitchingEnabled;

                // Load the always limit list into the text box
                alwayslimitlist.Text = config.GetValue("AlwaysLimitList").ToString();
            }
        }

        private void SaveAlwaysLimitList()
        {
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate"))
            {
                config.SetValue("AlwaysLimitList", alwayslimitlist.Text, RegistryValueKind.String);
            }
        }

        private void LimitPstate(bool enabled)
        {
            if (limited && enabled) return;
            if (!limited && !enabled) return;
            inspector.StartInfo.Arguments = enabled
                ? $"-setPStateLimit:{gpuIndex.SelectedIndex},5"
                : $"-setPStateLimit:{gpuIndex.SelectedIndex},0";
            _ = inspector.Start();
            limited = enabled;
        }


        private void Limitnvpstate_Load(object sender, EventArgs e)
        {


            string nvidiaSmiPath = "nvidia-smi"; // Path to nvidia-smi executable

            // Create a new process to execute nvidia-smi
            Process process = new Process();
            process.StartInfo.FileName = nvidiaSmiPath;
            process.StartInfo.Arguments = "--query-gpu=pstate --format=csv,noheader";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Start the process and read the output
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Parse the output to get the current P-State
            string[] lines = output.Trim().Split('\n');
            if (lines.Length > 0)
            {
                string pState = lines[0].Trim();
                Console.WriteLine("Current P-State: " + pState);
                if (pState == "P8")
                {
                    limited = true;
                }
                else
                {
                    limited = false;
                }
            }
            else
            {
                Console.WriteLine("Failed to get P-State.");
            }





            // create a new instance of the ManagementObjectSearcher class
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            // call the Get method of the ManagementObjectSearcher class to retrieve a collection of GPU objects
            ManagementObjectCollection gpuCollection = searcher.Get();

            // iterate through the collection of GPU objects
            foreach (ManagementObject gpu in gpuCollection.Cast<ManagementObject>())
            {
                _ = gpuIndex.Items.Add($"{gpu["Name"]}");
            }

            if (!File.Exists("nvidiaInspector.exe"))
            {
                _ = MessageBox.Show("Inspector not found in current directory", "limit-nvpstate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormClosing -= Limitnvpstate_FormClosing;
                Close();
            }

            // configure inspector launch settings
            inspector.StartInfo.FileName = "nvidiaInspector.exe";
            inspector.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;


            LoadSettings();

            if (startMinimizedToolStripMenuItem.Checked)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Limitnvpstate_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && Screen.GetWorkingArea(this).Contains(Cursor.Position))
            {
                ShowInTaskbar = false;
                notifyIcon.Visible = true;
                Hide();
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                notifyIcon.Visible = false;
                Show();
            }
            else if (e.Button == MouseButtons.Right)
            {
                notifyIcon.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void StartMinimizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate"))
            {
                config.SetValue("StartMinimized", Convert.ToString(startMinimizedToolStripMenuItem.Checked), RegistryValueKind.String);
            }
        }

        private void Limitnvpstate_FormClosing(object sender, FormClosingEventArgs e)
        {
            LimitPstate(false);
            SaveAlwaysLimitList(); // Save the list when closing
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void slow_click(object sender, EventArgs e)
        {
            LimitPstate(true);
        }

        private void fast_click(object sender, EventArgs e)
        {
            LimitPstate(false);
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private bool ShouldAlwaysLimit(string windowTitle)
        {
            if (string.IsNullOrEmpty(windowTitle) || alwayslimitlist.Lines == null)
                return false;

            string lowerTitle = windowTitle.ToLower();

            foreach (string gameName in alwayslimitlist.Lines)
            {
                if (!string.IsNullOrWhiteSpace(gameName) && lowerTitle.Contains(gameName.Trim().ToLower()))
                {
                    return true;
                }
            }

            return false;
        }

        Process process;
        bool limited = false;
        int steps_until_fast = 0;
        int steps_until_slow = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (process == null)
            {
                process = new Process();
                process.StartInfo.FileName = "nvidia-smi";
                process.StartInfo.Arguments = "--query-gpu=utilization.gpu --format=csv,noheader,nounits";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                return;
            }

            // Start the process and read the output
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Parse the output to get the GPU usage
            int gpuUsage = 0;
            try
            {
                gpuUsage = int.Parse(output.Trim());
            }
            catch { }
            string title = "";
            try
            {
                title = GetActiveWindowTitle();
            }
            catch { }

            // Print the GPU usage
            Console.WriteLine("GPU Usage: " + gpuUsage + "%");
            Console.WriteLine("limited: " + limited);
            Console.WriteLine("steps: " + steps_until_slow);
            Console.WriteLine("title: " + title);

            // Only do automatic switching if enabled
            if (!automaticSwitchingEnabled)
            {
                Console.WriteLine("Automatic switching is disabled");
                return;
            }

            // Check if current window should always be limited based on the list
            if (ShouldAlwaysLimit(title))
            {
                LimitPstate(true);
                return;
            }

            if (limited)
            {
                if (gpuUsage > 90)
                {
                    steps_until_fast++;
                }
                else
                {
                    steps_until_fast = 0;
                }
                if (steps_until_fast > 3)
                {
                    LimitPstate(false);
                    Console.WriteLine("LimitPstate(false);");
                    steps_until_slow = 0;
                }
            }
            else
            {
                if (gpuUsage < 10)
                {
                    steps_until_slow++;
                }
                else
                {
                    steps_until_slow = 0;
                }
                if (steps_until_slow > 5)
                {
                    LimitPstate(true);
                    Console.WriteLine("LimitPstate(true);");
                    steps_until_fast = 0;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        // auto mode on or off
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            automaticSwitchingEnabled = checkBox1.Checked;

            // Save the setting to registry
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate"))
            {
                config.SetValue("AutomaticSwitching", automaticSwitchingEnabled.ToString(), RegistryValueKind.String);
            }

            Console.WriteLine("Automatic switching: " + (automaticSwitchingEnabled ? "Enabled" : "Disabled"));
        }

        private void gpuIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate"))
            {
                config.SetValue("IndexOfGPU", gpuIndex.SelectedIndex, RegistryValueKind.String);
            }
            LoadSettings();
        }

        private void alwayslimitlist_TextChanged(object sender, EventArgs e)
        {
            // Save the always limit list when it changes
            SaveAlwaysLimitList();
        }
    }
}