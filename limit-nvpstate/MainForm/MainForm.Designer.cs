namespace limit_nvpstate {
    partial class limitnvpstate {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.applySettings = new System.Windows.Forms.Button();
            this.processes = new System.Windows.Forms.ListBox();
            this.addProcess = new System.Windows.Forms.Button();
            this.removeProcess = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pstateLimit = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gpuIndex = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMinimizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // applySettings
            // 
            this.applySettings.Location = new System.Drawing.Point(288, 292);
            this.applySettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.applySettings.Name = "applySettings";
            this.applySettings.Size = new System.Drawing.Size(297, 35);
            this.applySettings.TabIndex = 2;
            this.applySettings.Text = "Apply";
            this.applySettings.UseVisualStyleBackColor = true;
            this.applySettings.Click += new System.EventHandler(this.ApplySettings_Click);
            // 
            // processes
            // 
            this.processes.FormattingEnabled = true;
            this.processes.ItemHeight = 20;
            this.processes.Location = new System.Drawing.Point(9, 29);
            this.processes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.processes.Name = "processes";
            this.processes.Size = new System.Drawing.Size(241, 244);
            this.processes.TabIndex = 0;
            this.processes.SelectedIndexChanged += new System.EventHandler(this.Processes_SelectedIndexChanged);
            // 
            // addProcess
            // 
            this.addProcess.Location = new System.Drawing.Point(10, 29);
            this.addProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addProcess.Name = "addProcess";
            this.addProcess.Size = new System.Drawing.Size(136, 35);
            this.addProcess.TabIndex = 3;
            this.addProcess.Text = "Add";
            this.addProcess.UseVisualStyleBackColor = true;
            this.addProcess.Click += new System.EventHandler(this.AddProcess_Click);
            // 
            // removeProcess
            // 
            this.removeProcess.Location = new System.Drawing.Point(156, 29);
            this.removeProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.removeProcess.Name = "removeProcess";
            this.removeProcess.Size = new System.Drawing.Size(130, 35);
            this.removeProcess.TabIndex = 4;
            this.removeProcess.Text = "Remove";
            this.removeProcess.UseVisualStyleBackColor = true;
            this.removeProcess.Click += new System.EventHandler(this.RemoveProcess_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.processes);
            this.groupBox1.Location = new System.Drawing.Point(18, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(261, 286);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Processes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pstateLimit);
            this.groupBox2.Location = new System.Drawing.Point(288, 42);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(297, 72);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Limit P-State";
            // 
            // pstateLimit
            // 
            this.pstateLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pstateLimit.FormattingEnabled = true;
            this.pstateLimit.Items.AddRange(new object[] {
            "P2",
            "P8",
            "P10",
            "P12"});
            this.pstateLimit.Location = new System.Drawing.Point(9, 29);
            this.pstateLimit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pstateLimit.Name = "pstateLimit";
            this.pstateLimit.Size = new System.Drawing.Size(277, 28);
            this.pstateLimit.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gpuIndex);
            this.groupBox3.Location = new System.Drawing.Point(288, 123);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(297, 72);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "GPU";
            // 
            // gpuIndex
            // 
            this.gpuIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpuIndex.FormattingEnabled = true;
            this.gpuIndex.Location = new System.Drawing.Point(9, 29);
            this.gpuIndex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gpuIndex.Name = "gpuIndex";
            this.gpuIndex.Size = new System.Drawing.Size(277, 28);
            this.gpuIndex.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.addProcess);
            this.groupBox4.Controls.Add(this.removeProcess);
            this.groupBox4.Location = new System.Drawing.Point(286, 205);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(300, 78);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configure Processes";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(112, 36);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(111, 32);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.ExitToolStripMenuItem1_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startMinimizedToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.CheckOnClick = true;
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            this.startMinimizedToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.startMinimizedToolStripMenuItem.Text = "Start Minimized";
            this.startMinimizedToolStripMenuItem.Click += new System.EventHandler(this.StartMinimizedToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(164, 34);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(774, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 62);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(774, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 104);
            this.button2.TabIndex = 10;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(638, 152);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 104);
            this.button3.TabIndex = 11;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // limitnvpstate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 346);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.applySettings);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "limitnvpstate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "limit-nvpstate v0.2.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Limitnvpstate_FormClosing);
            this.Load += new System.EventHandler(this.Limitnvpstate_Load);
            this.SizeChanged += new System.EventHandler(this.Limitnvpstate_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button applySettings;
        private System.Windows.Forms.ListBox processes;
        private System.Windows.Forms.Button removeProcess;
        private System.Windows.Forms.Button addProcess;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox pstateLimit;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox gpuIndex;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startMinimizedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
    }
}

