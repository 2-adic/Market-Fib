namespace MarketFib
{
    partial class FormChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chtStockDisplay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.cmbStockSymbol = new System.Windows.Forms.ComboBox();
            this.cmbPeriod = new System.Windows.Forms.ComboBox();
            this.lblStockSymbol = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.btnUpdateDateRange = new System.Windows.Forms.Button();
            this.cmbPattern = new System.Windows.Forms.ComboBox();
            this.lblPatterns = new System.Windows.Forms.Label();
            this.cmbWaveStart = new System.Windows.Forms.ComboBox();
            this.cmbWaveEnd = new System.Windows.Forms.ComboBox();
            this.lblWaveStart = new System.Windows.Forms.Label();
            this.lblWaveEnd = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.bsCandlestick = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chtStockDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCandlestick)).BeginInit();
            this.SuspendLayout();
            // 
            // chtStockDisplay
            // 
            this.chtStockDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AlignWithChartArea = "chaBeauty";
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.Title = "Date";
            chartArea1.AxisX2.IsMarksNextToAxis = false;
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.Title = "Stock Price";
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.Name = "chaCandlestick";
            chartArea2.AlignWithChartArea = "chaCandlestick";
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.Title = "Stock Price";
            chartArea2.AxisX2.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.Title = "Alignment Score";
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.Name = "chaBeauty";
            this.chtStockDisplay.ChartAreas.Add(chartArea1);
            this.chtStockDisplay.ChartAreas.Add(chartArea2);
            this.chtStockDisplay.DataSource = this.bsCandlestick;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chtStockDisplay.Legends.Add(legend1);
            this.chtStockDisplay.Location = new System.Drawing.Point(-1, 60);
            this.chtStockDisplay.Name = "chtStockDisplay";
            series1.ChartArea = "chaCandlestick";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.Black;
            series1.CustomProperties = "PriceUpColor=80\\, 210\\, 80, PriceDownColor=240\\, 80\\, 80";
            series1.IsVisibleInLegend = false;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "serCandlestick";
            series1.XValueMember = "Date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "High, Low, Open, Close";
            series1.YValuesPerPoint = 4;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BorderWidth = 3;
            series2.ChartArea = "chaBeauty";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsVisibleInLegend = false;
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "serBeauty";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chtStockDisplay.Series.Add(series1);
            this.chtStockDisplay.Series.Add(series2);
            this.chtStockDisplay.Size = new System.Drawing.Size(923, 561);
            this.chtStockDisplay.TabIndex = 16;
            this.chtStockDisplay.TabStop = false;
            this.chtStockDisplay.Text = "chart1";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "ddd │ MMM dd, yyyy │ HH:mm:ss";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(75, 7);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "ddd │ MMM dd, yyyy │ HH:mm:ss";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(76, 33);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 20);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.Value = new System.DateTime(2021, 12, 31, 23, 59, 59, 0);
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(12, 9);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(15, 35);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End Date:";
            // 
            // cmbStockSymbol
            // 
            this.cmbStockSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockSymbol.FormattingEnabled = true;
            this.cmbStockSymbol.Location = new System.Drawing.Point(790, 7);
            this.cmbStockSymbol.Name = "cmbStockSymbol";
            this.cmbStockSymbol.Size = new System.Drawing.Size(121, 21);
            this.cmbStockSymbol.TabIndex = 12;
            this.cmbStockSymbol.SelectedIndexChanged += new System.EventHandler(this.CmbStockSymbol_SelectedIndexChanged);
            // 
            // cmbPeriod
            // 
            this.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriod.FormattingEnabled = true;
            this.cmbPeriod.Location = new System.Drawing.Point(790, 33);
            this.cmbPeriod.Name = "cmbPeriod";
            this.cmbPeriod.Size = new System.Drawing.Size(121, 21);
            this.cmbPeriod.TabIndex = 14;
            this.cmbPeriod.SelectedIndexChanged += new System.EventHandler(this.CmbPeriod_SelectedIndexChanged);
            // 
            // lblStockSymbol
            // 
            this.lblStockSymbol.AutoSize = true;
            this.lblStockSymbol.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblStockSymbol.Location = new System.Drawing.Point(709, 9);
            this.lblStockSymbol.Name = "lblStockSymbol";
            this.lblStockSymbol.Size = new System.Drawing.Size(75, 13);
            this.lblStockSymbol.TabIndex = 11;
            this.lblStockSymbol.Text = "Stock Symbol:";
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblPeriod.Location = new System.Drawing.Point(744, 35);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(40, 13);
            this.lblPeriod.TabIndex = 13;
            this.lblPeriod.Text = "Period:";
            // 
            // btnUpdateDateRange
            // 
            this.btnUpdateDateRange.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnUpdateDateRange.Location = new System.Drawing.Point(300, 6);
            this.btnUpdateDateRange.Name = "btnUpdateDateRange";
            this.btnUpdateDateRange.Size = new System.Drawing.Size(175, 23);
            this.btnUpdateDateRange.TabIndex = 4;
            this.btnUpdateDateRange.Text = "Update Date Range";
            this.btnUpdateDateRange.UseVisualStyleBackColor = true;
            this.btnUpdateDateRange.Click += new System.EventHandler(this.BtnUpdateDateRange_Click);
            // 
            // cmbPattern
            // 
            this.cmbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPattern.FormattingEnabled = true;
            this.cmbPattern.Items.AddRange(new object[] {
            "None",
            "Peak",
            "Valley",
            "Bullish",
            "Bearish",
            "Neutral",
            "Marubozu",
            "Hammer",
            "Doji",
            "Dragonfly doji",
            "Gravestone doji"});
            this.cmbPattern.Location = new System.Drawing.Point(353, 33);
            this.cmbPattern.Name = "cmbPattern";
            this.cmbPattern.Size = new System.Drawing.Size(121, 21);
            this.cmbPattern.TabIndex = 6;
            this.cmbPattern.SelectedIndexChanged += new System.EventHandler(this.CmbPattern_SelectedIndexChanged);
            // 
            // lblPatterns
            // 
            this.lblPatterns.AutoSize = true;
            this.lblPatterns.Location = new System.Drawing.Point(298, 35);
            this.lblPatterns.Name = "lblPatterns";
            this.lblPatterns.Size = new System.Drawing.Size(49, 13);
            this.lblPatterns.TabIndex = 5;
            this.lblPatterns.Text = "Patterns:";
            // 
            // cmbWaveStart
            // 
            this.cmbWaveStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveStart.FormattingEnabled = true;
            this.cmbWaveStart.Location = new System.Drawing.Point(566, 7);
            this.cmbWaveStart.Name = "cmbWaveStart";
            this.cmbWaveStart.Size = new System.Drawing.Size(121, 21);
            this.cmbWaveStart.TabIndex = 8;
            this.cmbWaveStart.SelectedIndexChanged += new System.EventHandler(this.CmbWaveStart_SelectedIndexChanged);
            // 
            // cmbWaveEnd
            // 
            this.cmbWaveEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWaveEnd.FormattingEnabled = true;
            this.cmbWaveEnd.Location = new System.Drawing.Point(566, 33);
            this.cmbWaveEnd.Name = "cmbWaveEnd";
            this.cmbWaveEnd.Size = new System.Drawing.Size(121, 21);
            this.cmbWaveEnd.TabIndex = 10;
            this.cmbWaveEnd.SelectedIndexChanged += new System.EventHandler(this.CmbWaveEnd_SelectedIndexChanged);
            // 
            // lblWaveStart
            // 
            this.lblWaveStart.AutoSize = true;
            this.lblWaveStart.Location = new System.Drawing.Point(496, 9);
            this.lblWaveStart.Name = "lblWaveStart";
            this.lblWaveStart.Size = new System.Drawing.Size(64, 13);
            this.lblWaveStart.TabIndex = 7;
            this.lblWaveStart.Text = "Wave Start:";
            // 
            // lblWaveEnd
            // 
            this.lblWaveEnd.AutoSize = true;
            this.lblWaveEnd.Location = new System.Drawing.Point(499, 35);
            this.lblWaveEnd.Name = "lblWaveEnd";
            this.lblWaveEnd.Size = new System.Drawing.Size(61, 13);
            this.lblWaveEnd.TabIndex = 9;
            this.lblWaveEnd.Text = "Wave End:";
            // 
            // lblWarning
            // 
            this.lblWarning.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWarning.AutoSize = true;
            this.lblWarning.BackColor = System.Drawing.Color.White;
            this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lblWarning.Location = new System.Drawing.Point(251, 301);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(421, 20);
            this.lblWarning.TabIndex = 17;
            this.lblWarning.Text = "No candlestick data available for the current date range";
            this.lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWarning.Visible = false;
            // 
            // bsCandlestick
            // 
            this.bsCandlestick.DataSource = typeof(MarketFib.SmartCandlestick);
            // 
            // FormChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 621);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblWaveEnd);
            this.Controls.Add(this.lblWaveStart);
            this.Controls.Add(this.cmbWaveEnd);
            this.Controls.Add(this.cmbWaveStart);
            this.Controls.Add(this.lblPatterns);
            this.Controls.Add(this.cmbPattern);
            this.Controls.Add(this.btnUpdateDateRange);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.lblStockSymbol);
            this.Controls.Add(this.cmbPeriod);
            this.Controls.Add(this.cmbStockSymbol);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.chtStockDisplay);
            this.MinimumSize = new System.Drawing.Size(939, 500);
            this.Name = "FormChart";
            this.Text = "Multiple Stocks";
            ((System.ComponentModel.ISupportInitialize)(this.chtStockDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCandlestick)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chtStockDisplay;
        private System.Windows.Forms.BindingSource bsCandlestick;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.ComboBox cmbStockSymbol;
        private System.Windows.Forms.ComboBox cmbPeriod;
        private System.Windows.Forms.Label lblStockSymbol;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Button btnUpdateDateRange;
        private System.Windows.Forms.ComboBox cmbPattern;
        private System.Windows.Forms.Label lblPatterns;
        private System.Windows.Forms.ComboBox cmbWaveStart;
        private System.Windows.Forms.ComboBox cmbWaveEnd;
        private System.Windows.Forms.Label lblWaveStart;
        private System.Windows.Forms.Label lblWaveEnd;
        private System.Windows.Forms.Label lblWarning;
    }
}