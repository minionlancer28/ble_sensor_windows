namespace BleCommunication
{
    partial class FrmBLE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBLE));
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblDeviceName = new System.Windows.Forms.Label();
            this.lblMacAddress = new System.Windows.Forms.Label();
            this.cbxService = new System.Windows.Forms.ComboBox();
            this.cbxCharacteristic = new System.Windows.Forms.ComboBox();
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.tbxHz = new System.Windows.Forms.TextBox();
            this.gbxMode = new System.Windows.Forms.GroupBox();
            this.lblTemp = new System.Windows.Forms.Label();
            this.tbxTimes = new System.Windows.Forms.TextBox();
            this.lblHz = new System.Windows.Forms.Label();
            this.cbxTemp = new System.Windows.Forms.CheckBox();
            this.cbxCompass = new System.Windows.Forms.CheckBox();
            this.cbxGyro = new System.Windows.Forms.CheckBox();
            this.cbxAccell = new System.Windows.Forms.CheckBox();
            this.cbxPrint = new System.Windows.Forms.CheckBox();
            this.gbxMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(375, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(82, 28);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(375, 65);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(82, 28);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(484, 65);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(82, 28);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(484, 21);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(82, 28);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblDeviceName
            // 
            this.lblDeviceName.AutoSize = true;
            this.lblDeviceName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDeviceName.Location = new System.Drawing.Point(28, 20);
            this.lblDeviceName.Name = "lblDeviceName";
            this.lblDeviceName.Size = new System.Drawing.Size(71, 12);
            this.lblDeviceName.TabIndex = 4;
            this.lblDeviceName.Text = "Device Name";
            // 
            // lblMacAddress
            // 
            this.lblMacAddress.AutoSize = true;
            this.lblMacAddress.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblMacAddress.Location = new System.Drawing.Point(28, 42);
            this.lblMacAddress.Name = "lblMacAddress";
            this.lblMacAddress.Size = new System.Drawing.Size(23, 12);
            this.lblMacAddress.TabIndex = 7;
            this.lblMacAddress.Text = "MAC";
            // 
            // cbxService
            // 
            this.cbxService.FormattingEnabled = true;
            this.cbxService.Location = new System.Drawing.Point(30, 78);
            this.cbxService.Name = "cbxService";
            this.cbxService.Size = new System.Drawing.Size(156, 20);
            this.cbxService.TabIndex = 8;
            this.cbxService.SelectedIndexChanged += new System.EventHandler(this.cbxService_SelectedIndexChanged);
            // 
            // cbxCharacteristic
            // 
            this.cbxCharacteristic.FormattingEnabled = true;
            this.cbxCharacteristic.Location = new System.Drawing.Point(192, 78);
            this.cbxCharacteristic.Name = "cbxCharacteristic";
            this.cbxCharacteristic.Size = new System.Drawing.Size(156, 20);
            this.cbxCharacteristic.TabIndex = 9;
            this.cbxCharacteristic.SelectedIndexChanged += new System.EventHandler(this.cbxCharacteristic_SelectedIndexChanged);
            // 
            // lbxLog
            // 
            this.lbxLog.BackColor = System.Drawing.Color.Black;
            this.lbxLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbxLog.ForeColor = System.Drawing.Color.White;
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.ItemHeight = 12;
            this.lbxLog.Location = new System.Drawing.Point(0, 192);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(591, 496);
            this.lbxLog.TabIndex = 10;
            // 
            // tbxHz
            // 
            this.tbxHz.Location = new System.Drawing.Point(360, 23);
            this.tbxHz.Name = "tbxHz";
            this.tbxHz.Size = new System.Drawing.Size(28, 19);
            this.tbxHz.TabIndex = 11;
            this.tbxHz.Text = "20";
            // 
            // gbxMode
            // 
            this.gbxMode.Controls.Add(this.lblTemp);
            this.gbxMode.Controls.Add(this.tbxTimes);
            this.gbxMode.Controls.Add(this.lblHz);
            this.gbxMode.Controls.Add(this.cbxTemp);
            this.gbxMode.Controls.Add(this.tbxHz);
            this.gbxMode.Controls.Add(this.cbxCompass);
            this.gbxMode.Controls.Add(this.cbxGyro);
            this.gbxMode.Controls.Add(this.cbxAccell);
            this.gbxMode.Location = new System.Drawing.Point(30, 110);
            this.gbxMode.Name = "gbxMode";
            this.gbxMode.Size = new System.Drawing.Size(536, 52);
            this.gbxMode.TabIndex = 12;
            this.gbxMode.TabStop = false;
            this.gbxMode.Text = "Mode";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Location = new System.Drawing.Point(486, 26);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(36, 12);
            this.lblTemp.TabIndex = 15;
            this.lblTemp.Text = "Times";
            // 
            // tbxTimes
            // 
            this.tbxTimes.Location = new System.Drawing.Point(452, 23);
            this.tbxTimes.Name = "tbxTimes";
            this.tbxTimes.Size = new System.Drawing.Size(28, 19);
            this.tbxTimes.TabIndex = 14;
            this.tbxTimes.Text = "200";
            // 
            // lblHz
            // 
            this.lblHz.AutoSize = true;
            this.lblHz.Location = new System.Drawing.Point(394, 26);
            this.lblHz.Name = "lblHz";
            this.lblHz.Size = new System.Drawing.Size(18, 12);
            this.lblHz.TabIndex = 13;
            this.lblHz.Text = "Hz";
            // 
            // cbxTemp
            // 
            this.cbxTemp.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbxTemp.AutoSize = true;
            this.cbxTemp.Checked = true;
            this.cbxTemp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxTemp.Location = new System.Drawing.Point(223, 21);
            this.cbxTemp.Name = "cbxTemp";
            this.cbxTemp.Size = new System.Drawing.Size(79, 22);
            this.cbxTemp.TabIndex = 3;
            this.cbxTemp.Text = "Temperature";
            this.cbxTemp.UseVisualStyleBackColor = true;
            // 
            // cbxCompass
            // 
            this.cbxCompass.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbxCompass.AutoSize = true;
            this.cbxCompass.Location = new System.Drawing.Point(144, 21);
            this.cbxCompass.Name = "cbxCompass";
            this.cbxCompass.Size = new System.Drawing.Size(62, 22);
            this.cbxCompass.TabIndex = 2;
            this.cbxCompass.Text = "Compass";
            this.cbxCompass.UseVisualStyleBackColor = true;
            // 
            // cbxGyro
            // 
            this.cbxGyro.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbxGyro.AutoSize = true;
            this.cbxGyro.Location = new System.Drawing.Point(85, 21);
            this.cbxGyro.Name = "cbxGyro";
            this.cbxGyro.Size = new System.Drawing.Size(39, 22);
            this.cbxGyro.TabIndex = 1;
            this.cbxGyro.Text = "Gyro";
            this.cbxGyro.UseVisualStyleBackColor = true;
            // 
            // cbxAccell
            // 
            this.cbxAccell.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbxAccell.AutoSize = true;
            this.cbxAccell.Checked = true;
            this.cbxAccell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAccell.Location = new System.Drawing.Point(18, 21);
            this.cbxAccell.Name = "cbxAccell";
            this.cbxAccell.Size = new System.Drawing.Size(47, 22);
            this.cbxAccell.TabIndex = 0;
            this.cbxAccell.Text = "Accell";
            this.cbxAccell.UseVisualStyleBackColor = true;
            // 
            // cbxPrint
            // 
            this.cbxPrint.AutoSize = true;
            this.cbxPrint.Location = new System.Drawing.Point(280, 56);
            this.cbxPrint.Name = "cbxPrint";
            this.cbxPrint.Size = new System.Drawing.Size(68, 16);
            this.cbxPrint.TabIndex = 13;
            this.cbxPrint.Text = "file save";
            this.cbxPrint.UseVisualStyleBackColor = true;
            // 
            // FrmBLE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 688);
            this.Controls.Add(this.cbxPrint);
            this.Controls.Add(this.gbxMode);
            this.Controls.Add(this.lbxLog);
            this.Controls.Add(this.cbxCharacteristic);
            this.Controls.Add(this.cbxService);
            this.Controls.Add(this.lblMacAddress);
            this.Controls.Add(this.lblDeviceName);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBLE";
            this.Text = "HomeDeviceDummy";
            this.gbxMode.ResumeLayout(false);
            this.gbxMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lblDeviceName;
        private System.Windows.Forms.Label lblMacAddress;
        private System.Windows.Forms.ComboBox cbxService;
        private System.Windows.Forms.ComboBox cbxCharacteristic;
        private System.Windows.Forms.ListBox lbxLog;
        private System.Windows.Forms.TextBox tbxHz;
        private System.Windows.Forms.GroupBox gbxMode;
        private System.Windows.Forms.CheckBox cbxTemp;
        private System.Windows.Forms.CheckBox cbxCompass;
        private System.Windows.Forms.CheckBox cbxGyro;
        private System.Windows.Forms.CheckBox cbxAccell;
        private System.Windows.Forms.Label lblHz;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.TextBox tbxTimes;
        private System.Windows.Forms.CheckBox cbxPrint;
    }
}