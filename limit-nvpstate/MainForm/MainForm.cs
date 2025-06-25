using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace limit_nvpstate {
    public partial class limitnvpstate : Form {
        private readonly Process inspector = new Process();

        public limitnvpstate() {
            InitializeComponent();
        }

        private void AddProcess_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                string fileName = Path.GetFileName(ofd.FileName).Replace(".exe", "");
                _ = processes.Items.Add(fileName);
            }
        }

        private void RemoveProcess_Click(object sender, EventArgs e) {
            processes.Items.RemoveAt(processes.SelectedIndex);
        }

        private void EventHandler(object sender, EventArrivedEventArgs e) {
            Process createdProcess = Process.GetProcessById((int)(uint)e.NewEvent.Properties["ProcessID"].Value);

            if (processes.Items.Contains(createdProcess.ProcessName)) {
                LimitPstate(false);
                createdProcess.WaitForExit();
                LimitPstate(true);
            }
        }

        private void LoadSettings() {
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate")) {

                if (config.GetValue("LimitPState") == null) {
                    config.SetValue("LimitPState", "1", RegistryValueKind.String);
                }

                if (config.GetValue("IndexOfGPU") == null) {
                    config.SetValue("IndexOfGPU", "0", RegistryValueKind.String);
                }

                if (config.GetValue("StartMinimized") == null) {
                    config.SetValue("StartMinimized", "False", RegistryValueKind.String);
                }

                if (config.GetValue("ProcessList") == null) {
                    config.SetValue("ProcessList", new string[] { }, RegistryValueKind.MultiString);
                }

                pstateLimit.SelectedIndex = Convert.ToInt32(config.GetValue("LimitPState"));
                gpuIndex.SelectedIndex = Convert.ToInt32(config.GetValue("IndexOfGPU"));
                startMinimizedToolStripMenuItem.Checked = Convert.ToBoolean(config.GetValue("StartMinimized"));

                processes.Items.Clear();
                foreach (string i in (string[])config.GetValue("ProcessList")) {
                    _ = processes.Items.Add(i);
                }
            }
        }

        private void LimitPstate(bool enabled) {
            if (limited && enabled) return;
            if (!limited && !enabled) return;
            inspector.StartInfo.Arguments = enabled
                //? $"-setPStateLimit:{gpuIndex.SelectedIndex},{pstateLimit.SelectedItem.ToString().Replace("P", "")}"
                ? $"-setPStateLimit:{gpuIndex.SelectedIndex},5"
                : $"-setPStateLimit:{gpuIndex.SelectedIndex},0";
            _ = inspector.Start();
        }

        private void ApplySettings_Click(object sender, EventArgs e) {
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate")) {
                config.SetValue("LimitPState", pstateLimit.SelectedIndex, RegistryValueKind.String);
                config.SetValue("IndexOfGPU", gpuIndex.SelectedIndex, RegistryValueKind.String);
                config.SetValue("ProcessList", processes.Items.OfType<string>().ToArray(), RegistryValueKind.MultiString);
            }
            LoadSettings();
        }

        private void Limitnvpstate_Load(object sender, EventArgs e) {


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
                if(pState == "P8")
                {
                    limited = true;
                } else
                {
                    limited = false;
                }
            }
            else
            {
                Console.WriteLine("Failed to get P-State.");
            }









            //return;
            // create a new instance of the ManagementObjectSearcher class
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

            // call the Get method of the ManagementObjectSearcher class to retrieve a collection of GPU objects
            ManagementObjectCollection gpuCollection = searcher.Get();

            // iterate through the collection of GPU objects
            foreach (ManagementObject gpu in gpuCollection.Cast<ManagementObject>()) {
                _ = gpuIndex.Items.Add($"{gpu["Name"]}");
            }

            if (!File.Exists("nvidiaInspector.exe")) {
                _ = MessageBox.Show("Inspector not found in current directory", "limit-nvpstate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FormClosing -= Limitnvpstate_FormClosing;
                Close();
            }

            // configure inspector launch settings
            inspector.StartInfo.FileName = "nvidiaInspector.exe";
            inspector.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // configure event handler
            //ManagementEventWatcher startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            //startWatch.EventArrived += new EventArrivedEventHandler(EventHandler);
            //startWatch.Start();


            LoadSettings(); // load settings when program starts
            //LimitPstate(true); // limit pstates when program starts

            if (startMinimizedToolStripMenuItem.Checked) {
                WindowState = FormWindowState.Minimized;
            }

            removeProcess.Enabled = false; // disable remove button by default, is re-enabled when item in list is selected
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void Limitnvpstate_SizeChanged(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized && Screen.GetWorkingArea(this).Contains(Cursor.Position)) {
                ShowInTaskbar = false;
                notifyIcon.Visible = true;
                Hide();
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                notifyIcon.Visible = false;
                Show();
            } else if (e.Button == MouseButtons.Right) {
                notifyIcon.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void StartMinimizedToolStripMenuItem_Click(object sender, EventArgs e) {
            using (RegistryKey config = Registry.CurrentUser.CreateSubKey("SOFTWARE\\limit-nvpstate")) {
                config.SetValue("StartMinimized", Convert.ToString(startMinimizedToolStripMenuItem.Checked), RegistryValueKind.String);
            }
        }

        private void Limitnvpstate_FormClosing(object sender, FormClosingEventArgs e) {
            LimitPstate(false);
        }

        private void ExitToolStripMenuItem1_Click(object sender, EventArgs e) {
            Close();
        }

        private void Processes_SelectedIndexChanged(object sender, EventArgs e) {
            // only enable the remove process button if a index is selected
            removeProcess.Enabled = processes.SelectedIndex > -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LimitPstate(true);
        }

        private void button2_Click(object sender, EventArgs e)
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

        Process process;
        bool limited = false;
        int steps_until_fast = 0;
        int steps_until_slow = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(process == null)
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
            } catch { }

            // Print the GPU usage
            Console.WriteLine("GPU Usage: " + gpuUsage + "%");
            Console.WriteLine("limited: " + limited);
            Console.WriteLine("steps: " + steps_until_slow);
            Console.WriteLine("title: " + title);

            if (title != null && (title.ToLower().Contains("rimworld") || title.ToLower().Contains("team fortress 2")))
            {
                LimitPstate(true);
                return;
            }


            if (limited)
            {
                if(gpuUsage > 90)
                {
                    steps_until_fast++;
                }
                else
                {
                    steps_until_fast = 0;
                }
                if(steps_until_fast > 3)
                {
                    LimitPstate(false);
                    limited = false;
                    Console.WriteLine("LimitPstate(false);");
                    steps_until_slow = 0;
                }
            } else
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
                    limited = true;
                    Console.WriteLine("LimitPstate(true);");
                    steps_until_fast = 0;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_GPUPerformanceCounters_GPUEngine");

            //foreach (ManagementObject queryObj in searcher.Get())
            //{
            //    var usage = queryObj["UtilizationPercentage"];
            //    if (int.Parse(usage + "") > 0)
            //        Console.WriteLine("GPU Usage: {0}%", usage);
            //}
            // Create a new process to execute the nvidia-smi command
            Process process = new Process();
            process.StartInfo.FileName = "nvidia-smi";
            process.StartInfo.Arguments = "--query-gpu=utilization.gpu --format=csv,noheader,nounits";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Start the process and read the output
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Parse the output to get the GPU usage
            int gpuUsage = int.Parse(output.Trim());

            // Print the GPU usage
            Console.WriteLine("GPU Usage: " + gpuUsage + "%");
        }
    }
}
