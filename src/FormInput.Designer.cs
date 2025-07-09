namespace MarketFib
{
    partial class FormInput
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
            this.btnSingleWindow = new System.Windows.Forms.Button();
            this.ofdSingleWindowLoad = new System.Windows.Forms.OpenFileDialog();
            this.lblSelectionInfo = new System.Windows.Forms.Label();
            this.btnMultiWindow = new System.Windows.Forms.Button();
            this.ofdMultiWindowLoad = new System.Windows.Forms.OpenFileDialog();
            this.dtpDefaultStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDefaultEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateTitle = new System.Windows.Forms.Label();
            this.lblDateInfo = new System.Windows.Forms.Label();
            this.lblSelectionTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSingleWindow
            // 
            this.btnSingleWindow.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSingleWindow.Location = new System.Drawing.Point(12, 28);
            this.btnSingleWindow.Name = "btnSingleWindow";
            this.btnSingleWindow.Size = new System.Drawing.Size(100, 30);
            this.btnSingleWindow.TabIndex = 0;
            this.btnSingleWindow.Text = "Single Window";
            this.btnSingleWindow.UseVisualStyleBackColor = true;
            this.btnSingleWindow.Click += new System.EventHandler(this.BtnSingleWindow_Click);
            // 
            // ofdSingleWindowLoad
            // 
            this.ofdSingleWindowLoad.Filter = "All Files|*.csv";
            this.ofdSingleWindowLoad.Multiselect = true;
            this.ofdSingleWindowLoad.FileOk += new System.ComponentModel.CancelEventHandler(this.OfdSingleWindowLoad_FileOk);
            // 
            // lblSelectionInfo
            // 
            this.lblSelectionInfo.AutoSize = true;
            this.lblSelectionInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblSelectionInfo.Location = new System.Drawing.Point(29, 61);
            this.lblSelectionInfo.Name = "lblSelectionInfo";
            this.lblSelectionInfo.Size = new System.Drawing.Size(173, 26);
            this.lblSelectionInfo.TabIndex = 1;
            this.lblSelectionInfo.Text = "*All filenames must be in the format:\r\n\"Stock-Period.csv\"";
            this.lblSelectionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMultiWindow
            // 
            this.btnMultiWindow.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMultiWindow.Location = new System.Drawing.Point(118, 28);
            this.btnMultiWindow.Name = "btnMultiWindow";
            this.btnMultiWindow.Size = new System.Drawing.Size(100, 30);
            this.btnMultiWindow.TabIndex = 1;
            this.btnMultiWindow.Text = "Multiple Windows";
            this.btnMultiWindow.UseVisualStyleBackColor = true;
            this.btnMultiWindow.Click += new System.EventHandler(this.BtnMultiWindow_Click);
            // 
            // ofdMultiWindowLoad
            // 
            this.ofdMultiWindowLoad.Filter = "All Files|*.csv";
            this.ofdMultiWindowLoad.Multiselect = true;
            this.ofdMultiWindowLoad.FileOk += new System.ComponentModel.CancelEventHandler(this.OfdMultiWindowLoad_FileOk);
            // 
            // dtpDefaultStartDate
            // 
            this.dtpDefaultStartDate.CustomFormat = "ddd │ MMM dd, yyyy │ HH:mm:ss";
            this.dtpDefaultStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDefaultStartDate.Location = new System.Drawing.Point(15, 147);
            this.dtpDefaultStartDate.Name = "dtpDefaultStartDate";
            this.dtpDefaultStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpDefaultStartDate.TabIndex = 2;
            this.dtpDefaultStartDate.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // dtpDefaultEndDate
            // 
            this.dtpDefaultEndDate.CustomFormat = "ddd │ MMM dd, yyyy │ HH:mm:ss";
            this.dtpDefaultEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDefaultEndDate.Location = new System.Drawing.Point(15, 173);
            this.dtpDefaultEndDate.Name = "dtpDefaultEndDate";
            this.dtpDefaultEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpDefaultEndDate.TabIndex = 3;
            this.dtpDefaultEndDate.Value = new System.DateTime(2021, 12, 31, 23, 59, 59, 0);
            // 
            // lblDateTitle
            // 
            this.lblDateTitle.AutoSize = true;
            this.lblDateTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblDateTitle.Location = new System.Drawing.Point(74, 128);
            this.lblDateTitle.Name = "lblDateTitle";
            this.lblDateTitle.Size = new System.Drawing.Size(83, 16);
            this.lblDateTitle.TabIndex = 4;
            this.lblDateTitle.Text = "Date Range:";
            this.lblDateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateInfo
            // 
            this.lblDateInfo.AutoSize = true;
            this.lblDateInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDateInfo.Location = new System.Drawing.Point(44, 196);
            this.lblDateInfo.Name = "lblDateInfo";
            this.lblDateInfo.Size = new System.Drawing.Size(143, 26);
            this.lblDateInfo.TabIndex = 5;
            this.lblDateInfo.Text = "*Dates can be changed later\r\n for individual windows";
            this.lblDateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSelectionTitle
            // 
            this.lblSelectionTitle.AutoSize = true;
            this.lblSelectionTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.lblSelectionTitle.Location = new System.Drawing.Point(70, 9);
            this.lblSelectionTitle.Name = "lblSelectionTitle";
            this.lblSelectionTitle.Size = new System.Drawing.Size(91, 16);
            this.lblSelectionTitle.TabIndex = 6;
            this.lblSelectionTitle.Text = "File Selection:";
            this.lblSelectionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 231);
            this.Controls.Add(this.lblSelectionTitle);
            this.Controls.Add(this.lblDateInfo);
            this.Controls.Add(this.lblDateTitle);
            this.Controls.Add(this.dtpDefaultEndDate);
            this.Controls.Add(this.dtpDefaultStartDate);
            this.Controls.Add(this.btnMultiWindow);
            this.Controls.Add(this.lblSelectionInfo);
            this.Controls.Add(this.btnSingleWindow);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(246, 270);
            this.MinimumSize = new System.Drawing.Size(246, 270);
            this.Name = "FormInput";
            this.Text = "Selector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSingleWindow;
        private System.Windows.Forms.OpenFileDialog ofdSingleWindowLoad;
        private System.Windows.Forms.Label lblSelectionInfo;
        private System.Windows.Forms.Button btnMultiWindow;
        private System.Windows.Forms.OpenFileDialog ofdMultiWindowLoad;
        private System.Windows.Forms.DateTimePicker dtpDefaultStartDate;
        private System.Windows.Forms.DateTimePicker dtpDefaultEndDate;
        private System.Windows.Forms.Label lblDateTitle;
        private System.Windows.Forms.Label lblDateInfo;
        private System.Windows.Forms.Label lblSelectionTitle;
    }
}

