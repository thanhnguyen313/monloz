namespace TourApp
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            panel1 = new Panel();
            sfindBtn = new Button();
            screateBtn = new Button();
            LogOutBtn = new Button();
            label1 = new Label();
            Account = new ContextMenuStrip(components);
            myAccountToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            logOutToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            createBtn = new Button();
            findBtn = new Button();
            flowPanelCards = new FlowLayoutPanel();
            pnlEmptyState = new Panel();
            label3 = new Label();
            label2 = new Label();
            addBtn = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteBtn = new ToolStripMenuItem();
            toolStripTextBox1 = new ToolStripMenuItem();
            panel1.SuspendLayout();
            Account.SuspendLayout();
            pnlEmptyState.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            panel1.Controls.Add(sfindBtn);
            panel1.Controls.Add(screateBtn);
            panel1.Controls.Add(LogOutBtn);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1278, 72);
            panel1.TabIndex = 0;
            // 
            // sfindBtn
            // 
            sfindBtn.FlatAppearance.BorderSize = 0;
            sfindBtn.FlatStyle = FlatStyle.Flat;
            sfindBtn.ForeColor = Color.Transparent;
            sfindBtn.Image = (Image)resources.GetObject("sfindBtn.Image");
            sfindBtn.Location = new Point(1072, 22);
            sfindBtn.Name = "sfindBtn";
            sfindBtn.Size = new Size(30, 30);
            sfindBtn.TabIndex = 5;
            sfindBtn.UseVisualStyleBackColor = true;
            // 
            // screateBtn
            // 
            screateBtn.BackColor = Color.FromArgb(30, 30, 30);
            screateBtn.FlatAppearance.BorderSize = 0;
            screateBtn.FlatStyle = FlatStyle.Flat;
            screateBtn.ForeColor = Color.Transparent;
            screateBtn.Image = (Image)resources.GetObject("screateBtn.Image");
            screateBtn.Location = new Point(1147, 22);
            screateBtn.Name = "screateBtn";
            screateBtn.Size = new Size(30, 30);
            screateBtn.TabIndex = 4;
            screateBtn.UseVisualStyleBackColor = false;
            screateBtn.Click += screateBtn_Click;
            // 
            // LogOutBtn
            // 
            LogOutBtn.BackColor = Color.Transparent;
            LogOutBtn.FlatAppearance.BorderSize = 0;
            LogOutBtn.FlatAppearance.MouseDownBackColor = Color.Gray;
            LogOutBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 55, 55);
            LogOutBtn.FlatStyle = FlatStyle.Flat;
            LogOutBtn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LogOutBtn.ForeColor = SystemColors.ActiveCaptionText;
            LogOutBtn.Image = (Image)resources.GetObject("LogOutBtn.Image");
            LogOutBtn.Location = new Point(1221, 22);
            LogOutBtn.Name = "LogOutBtn";
            LogOutBtn.Size = new Size(30, 30);
            LogOutBtn.TabIndex = 3;
            LogOutBtn.UseVisualStyleBackColor = false;
            LogOutBtn.Click += LogOutBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(52, 178, 51);
            label1.Location = new Point(25, 9);
            label1.Name = "label1";
            label1.Size = new Size(298, 45);
            label1.TabIndex = 2;
            label1.Text = "🏆 TOURNAMENT";
            // 
            // Account
            // 
            Account.ImageScalingSize = new Size(24, 24);
            Account.Items.AddRange(new ToolStripItem[] { myAccountToolStripMenuItem, settingsToolStripMenuItem, logOutToolStripMenuItem, exitToolStripMenuItem });
            Account.Name = "Account";
            Account.Size = new Size(177, 132);
            // 
            // myAccountToolStripMenuItem
            // 
            myAccountToolStripMenuItem.Name = "myAccountToolStripMenuItem";
            myAccountToolStripMenuItem.Size = new Size(176, 32);
            myAccountToolStripMenuItem.Text = "My account";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(176, 32);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // logOutToolStripMenuItem
            // 
            logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            logOutToolStripMenuItem.Size = new Size(176, 32);
            logOutToolStripMenuItem.Text = "Log out";
            logOutToolStripMenuItem.Click += logOutToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(176, 32);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // createBtn
            // 
            createBtn.BackColor = Color.FromArgb(52, 178, 51);
            createBtn.FlatAppearance.BorderSize = 0;
            createBtn.FlatStyle = FlatStyle.Flat;
            createBtn.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            createBtn.ForeColor = Color.White;
            createBtn.Location = new Point(45, 130);
            createBtn.Name = "createBtn";
            createBtn.Size = new Size(397, 104);
            createBtn.TabIndex = 2;
            createBtn.Text = "CREATE TOURNAMENT";
            createBtn.UseVisualStyleBackColor = false;
            createBtn.Click += createBtn_Click;
            // 
            // findBtn
            // 
            findBtn.BackColor = Color.FromArgb(52, 178, 51);
            findBtn.FlatAppearance.BorderSize = 0;
            findBtn.FlatStyle = FlatStyle.Flat;
            findBtn.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            findBtn.ForeColor = Color.White;
            findBtn.Location = new Point(45, 275);
            findBtn.Name = "findBtn";
            findBtn.Size = new Size(397, 104);
            findBtn.TabIndex = 3;
            findBtn.Text = "FIND TOURNAMENTS";
            findBtn.UseVisualStyleBackColor = false;
            // 
            // flowPanelCards
            // 
            flowPanelCards.AutoScroll = true;
            flowPanelCards.BackColor = Color.Transparent;
            flowPanelCards.Dock = DockStyle.Bottom;
            flowPanelCards.Location = new Point(0, 452);
            flowPanelCards.Name = "flowPanelCards";
            flowPanelCards.Padding = new Padding(20, 20, 0, 0);
            flowPanelCards.Size = new Size(1278, 312);
            flowPanelCards.TabIndex = 4;
            flowPanelCards.Paint += flowPanelCards_Paint;
            // 
            // pnlEmptyState
            // 
            pnlEmptyState.Controls.Add(label3);
            pnlEmptyState.Controls.Add(label2);
            pnlEmptyState.Controls.Add(addBtn);
            pnlEmptyState.Location = new Point(0, 452);
            pnlEmptyState.Name = "pnlEmptyState";
            pnlEmptyState.Size = new Size(1243, 282);
            pnlEmptyState.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(582, 80);
            label3.Name = "label3";
            label3.Size = new Size(135, 38);
            label3.TabIndex = 2;
            label3.Text = "Add now";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(475, 18);
            label2.Name = "label2";
            label2.Size = new Size(371, 48);
            label2.TabIndex = 1;
            label2.Text = "No tournaments yet!";
            // 
            // addBtn
            // 
            addBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            addBtn.FlatAppearance.BorderSize = 0;
            addBtn.FlatStyle = FlatStyle.Flat;
            addBtn.Image = (Image)resources.GetObject("addBtn.Image");
            addBtn.Location = new Point(597, 144);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(100, 100);
            addBtn.TabIndex = 0;
            addBtn.UseVisualStyleBackColor = true;
            addBtn.Click += addBtn_Click_1;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteBtn, toolStripTextBox1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(214, 68);
            // 
            // deleteBtn
            // 
            deleteBtn.BackColor = Color.White;
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(213, 32);
            deleteBtn.Text = "✏️ Chỉnh sửa";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(213, 32);
            toolStripTextBox1.Text = "🗑️ Xóa giải đấu";
            toolStripTextBox1.Click += toolStripTextBox1_Click;
            // 
            // Home
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(32, 34, 37);
            ClientSize = new Size(1278, 764);
            Controls.Add(pnlEmptyState);
            Controls.Add(flowPanelCards);
            Controls.Add(findBtn);
            Controls.Add(createBtn);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlDarkDark;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Home";
            StartPosition = FormStartPosition.CenterScreen;
            Text = " ";
            FormClosed += Home_FormClosed;
            Load += Home_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            Account.ResumeLayout(false);
            pnlEmptyState.ResumeLayout(false);
            pnlEmptyState.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button LogOutBtn;
        private Label label1;
        private ContextMenuStrip Account;
        private ToolStripMenuItem myAccountToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem logOutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button createBtn;
        private Button findBtn;
        private Button Screate;
        private Button button2;
        private Button sfindBtn;
        private Button screateBtn;
        private FlowLayoutPanel flowPanelCards;
        private Panel pnlEmptyState;
        private Button addBtn;
        private Label label3;
        private Label label2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem deleteBtn;
        private ToolStripMenuItem toolStripTextBox1;
    }
}