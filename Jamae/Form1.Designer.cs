namespace Jamae
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dgbTradeInfo = new System.Windows.Forms.DataGridView();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.high_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.high_volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Low_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Low_Volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bVolume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtbBPrice = new System.Windows.Forms.RichTextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rtbWarning = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgbTradeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            //
            // dgbTradeInfo
            //
            this.dgbTradeInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbTradeInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Price,
            this.Volume,
            this.high_price,
            this.high_volume,
            this.Low_Price,
            this.Low_Volume,
            this.bPrice,
            this.bVolume});
            this.dgbTradeInfo.Location = new System.Drawing.Point(12, 12);
            this.dgbTradeInfo.Name = "dgbTradeInfo";
            this.dgbTradeInfo.Size = new System.Drawing.Size(852, 225);
            this.dgbTradeInfo.TabIndex = 0;
            //
            // Price
            //
            this.Price.HeaderText = "clPrice";
            this.Price.Name = "Price";
            this.Price.Width = 70;
            //
            // Volume
            //
            this.Volume.HeaderText = "clVolume";
            this.Volume.Name = "Volume";
            this.Volume.Width = 70;
            //
            // high_price
            //
            this.high_price.HeaderText = "clHighPrice";
            this.high_price.Name = "high_price";
            this.high_price.Width = 70;
            //
            // high_volume
            //
            this.high_volume.HeaderText = "clHighVolume";
            this.high_volume.Name = "high_volume";
            this.high_volume.Width = 70;
            //
            // Low_Price
            //
            this.Low_Price.HeaderText = "clLowPrice";
            this.Low_Price.Name = "Low_Price";
            this.Low_Price.Width = 70;
            //
            // Low_Volume
            //
            this.Low_Volume.HeaderText = "clLowVolume";
            this.Low_Volume.Name = "Low_Volume";
            this.Low_Volume.Width = 70;
            //
            // bPrice
            //
            this.bPrice.HeaderText = "clBPrice";
            this.bPrice.Name = "bPrice";
            this.bPrice.Width = 70;
            //
            // bVolume
            //
            this.bVolume.HeaderText = "clBVolume";
            this.bVolume.Name = "bVolume";
            this.bVolume.Width = 70;
            //
            // rtbBPrice
            //
            this.rtbBPrice.Font = new System.Drawing.Font("휴먼모음T", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rtbBPrice.Location = new System.Drawing.Point(65, 639);
            this.rtbBPrice.Name = "rtbBPrice";
            this.rtbBPrice.Size = new System.Drawing.Size(209, 65);
            this.rtbBPrice.TabIndex = 1;
            this.rtbBPrice.Text = "";
            //
            // chart1
            //
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MaximumAutoSize = 20F;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "caCandle";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 20F;
            chartArea1.Position.Width = 96F;
            chartArea1.Position.X = 4F;
            chartArea1.Position.Y = 4F;
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            chartArea2.AlignWithChartArea = "caCandle";
            chartArea2.Name = "caVolume";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 20F;
            chartArea2.Position.Width = 96F;
            chartArea2.Position.X = 4F;
            chartArea2.Position.Y = 24F;
            chartArea3.AlignWithChartArea = "caCandle";
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.BackColor = System.Drawing.Color.White;
            chartArea3.BackSecondaryColor = System.Drawing.Color.White;
            chartArea3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.Name = "caRSI";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 20F;
            chartArea3.Position.Width = 80F;
            chartArea3.Position.X = 4F;
            chartArea3.Position.Y = 44F;
            chartArea4.AlignWithChartArea = "caCandle";
            chartArea4.Name = "caStochastic";
            chartArea4.Position.Auto = false;
            chartArea4.Position.Height = 20F;
            chartArea4.Position.Width = 96F;
            chartArea4.Position.X = 4F;
            chartArea4.Position.Y = 64F;
            chartArea5.AlignWithChartArea = "caCandle";
            chartArea5.Name = "caCCI";
            chartArea5.Position.Auto = false;
            chartArea5.Position.Height = 20F;
            chartArea5.Position.Width = 96F;
            chartArea5.Position.X = 4F;
            chartArea5.Position.Y = 80F;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.ChartAreas.Add(chartArea3);
            this.chart1.ChartAreas.Add(chartArea4);
            this.chart1.ChartAreas.Add(chartArea5);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 243);
            this.chart1.Name = "chart1";
            series1.ChartArea = "caCandle";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Legend = "Legend1";
            series1.Name = "sCandle";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "caVolume";
            series2.Legend = "Legend1";
            series2.Name = "sVolume";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series3.ChartArea = "caRSI";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
            series3.Legend = "Legend1";
            series3.MarkerStep = 10;
            series3.Name = "sRSI";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series4.ChartArea = "caCandle";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))), ((int)(((byte)(240)))));
            series4.Legend = "Legend1";
            series4.MarkerStep = 10;
            series4.Name = "sBB";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series4.YValuesPerPoint = 2;
            series5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series5.ChartArea = "caCandle";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(180)))), ((int)(((byte)(65)))));
            series5.Legend = "Legend1";
            series5.Name = "sMovingAverage";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series6.ChartArea = "caStochastic";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Red;
            series6.Legend = "Legend1";
            series6.Name = "sStochastic";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series7.ChartArea = "caStochastic";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Blue;
            series7.Legend = "Legend1";
            series7.Name = "sSMA";
            series8.ChartArea = "caCCI";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Legend = "Legend1";
            series8.Name = "sCCI";
            series8.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Series.Add(series7);
            this.chart1.Series.Add(series8);
            this.chart1.Size = new System.Drawing.Size(594, 390);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            //
            // rtbWarning
            //
            this.rtbWarning.Font = new System.Drawing.Font("굴림", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbWarning.Location = new System.Drawing.Point(295, 639);
            this.rtbWarning.Name = "rtbWarning";
            this.rtbWarning.Size = new System.Drawing.Size(211, 65);
            this.rtbWarning.TabIndex = 4;
            this.rtbWarning.Text = "";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(618, 716);
            this.Controls.Add(this.rtbWarning);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.rtbBPrice);
            this.Controls.Add(this.dgbTradeInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgbTradeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgbTradeInfo;
        private System.Windows.Forms.RichTextBox rtbBPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn high_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn high_volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn Low_Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Low_Volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn bPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn bVolume;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RichTextBox rtbWarning;
    }
}

