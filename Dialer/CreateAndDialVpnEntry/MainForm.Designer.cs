namespace Dialer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.AllUsersPhoneBook = new DotRas.RasPhoneBook(this.components);
            this.CreateEntryButton = new System.Windows.Forms.Button();
            this.DialButton = new System.Windows.Forms.Button();
            this.Dialer = new DotRas.RasDialer(this.components);
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.gvEntries = new System.Windows.Forms.DataGridView();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vpnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemoveEntryButton = new System.Windows.Forms.Button();
            this.buttonFlushGV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gvEntries)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateEntryButton
            // 
            this.CreateEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateEntryButton.Location = new System.Drawing.Point(443, 220);
            this.CreateEntryButton.Margin = new System.Windows.Forms.Padding(4);
            this.CreateEntryButton.Name = "CreateEntryButton";
            this.CreateEntryButton.Size = new System.Drawing.Size(73, 26);
            this.CreateEntryButton.TabIndex = 0;
            this.CreateEntryButton.Text = "新建";
            this.CreateEntryButton.UseVisualStyleBackColor = true;
            this.CreateEntryButton.Click += new System.EventHandler(this.CreateEntryButton_Click);
            // 
            // DialButton
            // 
            this.DialButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DialButton.Enabled = false;
            this.DialButton.Location = new System.Drawing.Point(16, 220);
            this.DialButton.Margin = new System.Windows.Forms.Padding(4);
            this.DialButton.Name = "DialButton";
            this.DialButton.Size = new System.Drawing.Size(68, 26);
            this.DialButton.TabIndex = 1;
            this.DialButton.Text = "连接";
            this.DialButton.UseVisualStyleBackColor = true;
            this.DialButton.Click += new System.EventHandler(this.DialButton_Click);
            // 
            // Dialer
            // 
// TODO: “”的代码生成失败，原因是出现异常“无效的基元类型: System.IntPtr。请考虑使用 CodeObjectCreateExpression。”。
            this.Dialer.Credentials = null;
            this.Dialer.EapOptions = new DotRas.RasEapOptions(false, false, false);
            this.Dialer.HangUpPollingInterval = 0;
            this.Dialer.Options = new DotRas.RasDialOptions(false, false, false, false, false, false, false, false, false, false);
            this.Dialer.SynchronizingObject = this;
            this.Dialer.StateChanged += new System.EventHandler<DotRas.StateChangedEventArgs>(this.Dialer_StateChanged);
            this.Dialer.DialCompleted += new System.EventHandler<DotRas.DialCompletedEventArgs>(this.Dialer_DialCompleted);
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusTextBox.Location = new System.Drawing.Point(16, 254);
            this.StatusTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.StatusTextBox.Multiline = true;
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusTextBox.Size = new System.Drawing.Size(499, 110);
            this.StatusTextBox.TabIndex = 2;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(92, 220);
            this.DisconnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(72, 26);
            this.DisconnectButton.TabIndex = 3;
            this.DisconnectButton.Text = "断开";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // gvEntries
            // 
            this.gvEntries.AllowUserToAddRows = false;
            this.gvEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEntries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.State,
            this.vpnName,
            this.ipAddress,
            this.Info});
            this.gvEntries.Location = new System.Drawing.Point(16, 15);
            this.gvEntries.Margin = new System.Windows.Forms.Padding(4);
            this.gvEntries.MultiSelect = false;
            this.gvEntries.Name = "gvEntries";
            this.gvEntries.ReadOnly = true;
            this.gvEntries.RowTemplate.Height = 23;
            this.gvEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvEntries.Size = new System.Drawing.Size(500, 198);
            this.gvEntries.TabIndex = 4;
            this.gvEntries.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEntries_RowEnter);
            // 
            // State
            // 
            this.State.HeaderText = "状态";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Width = 30;
            // 
            // vpnName
            // 
            this.vpnName.DataPropertyName = "Name";
            this.vpnName.HeaderText = "名称";
            this.vpnName.Name = "vpnName";
            this.vpnName.ReadOnly = true;
            // 
            // ipAddress
            // 
            this.ipAddress.DataPropertyName = "IPAddress.ToString()";
            this.ipAddress.HeaderText = "IP地址";
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.ReadOnly = true;
            this.ipAddress.Width = 80;
            // 
            // Info
            // 
            this.Info.HeaderText = "消息";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.Width = 120;
            // 
            // RemoveEntryButton
            // 
            this.RemoveEntryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveEntryButton.Location = new System.Drawing.Point(365, 220);
            this.RemoveEntryButton.Margin = new System.Windows.Forms.Padding(4);
            this.RemoveEntryButton.Name = "RemoveEntryButton";
            this.RemoveEntryButton.Size = new System.Drawing.Size(69, 26);
            this.RemoveEntryButton.TabIndex = 5;
            this.RemoveEntryButton.Text = "删除";
            this.RemoveEntryButton.UseVisualStyleBackColor = true;
            this.RemoveEntryButton.Click += new System.EventHandler(this.RemoveEntryButton_Click);
            // 
            // buttonFlushGV
            // 
            this.buttonFlushGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFlushGV.Location = new System.Drawing.Point(212, 220);
            this.buttonFlushGV.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFlushGV.Name = "buttonFlushGV";
            this.buttonFlushGV.Size = new System.Drawing.Size(97, 26);
            this.buttonFlushGV.TabIndex = 6;
            this.buttonFlushGV.Text = "刷新列表";
            this.buttonFlushGV.UseVisualStyleBackColor = true;
            this.buttonFlushGV.Click += new System.EventHandler(this.buttonFlushGV_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 380);
            this.Controls.Add(this.buttonFlushGV);
            this.Controls.Add(this.RemoveEntryButton);
            this.Controls.Add(this.gvEntries);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.DialButton);
            this.Controls.Add(this.CreateEntryButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "拨号器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.gvEntries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DotRas.RasPhoneBook AllUsersPhoneBook;
        private System.Windows.Forms.Button CreateEntryButton;
        private System.Windows.Forms.Button DialButton;
        private DotRas.RasDialer Dialer;
        private System.Windows.Forms.TextBox StatusTextBox;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.DataGridView gvEntries;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn vpnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Info;
        private System.Windows.Forms.Button buttonFlushGV;
        private System.Windows.Forms.Button RemoveEntryButton;
    }
}

