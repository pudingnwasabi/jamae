﻿namespace Jamae
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rtbWarning = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgbTradeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
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
            this.rtbBPrice.Location = new System.Drawing.Point(76, 436);
            this.rtbBPrice.Name = "rtbBPrice";
            this.rtbBPrice.Size = new System.Drawing.Size(209, 65);
            this.rtbBPrice.TabIndex = 1;
            this.rtbBPrice.Text = "";
            //
            // chart1
            //
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea3.AxisY.MaximumAutoSize = 20F;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.Name = "ChartArea1";
            chartArea3.Position.Auto = false;
            chartArea3.Position.Height = 94F;
            chartArea3.Position.Width = 76.1602F;
            chartArea3.Position.X = 4F;
            chartArea3.Position.Y = 3F;
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(12, 243);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.YValuesPerPoint = 4;
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(594, 110);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            //
            // chart2
            //
            chartArea4.Name = "ChartArea1";
            chartArea4.Position.Auto = false;
            chartArea4.Position.Height = 94F;
            chartArea4.Position.Width = 69F;
            chartArea4.Position.X = 11F;
            chartArea4.Position.Y = 3F;
            this.chart2.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart2.Legends.Add(legend4);
            this.chart2.Location = new System.Drawing.Point(12, 346);
            this.chart2.Name = "chart2";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            series4.YValuesPerPoint = 4;
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(594, 69);
            this.chart2.TabIndex = 3;
            this.chart2.Text = "chart2";
            //
            // rtbWarning
            //
            this.rtbWarning.Font = new System.Drawing.Font("굴림", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbWarning.Location = new System.Drawing.Point(330, 436);
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
            this.ClientSize = new System.Drawing.Size(618, 509);
            this.Controls.Add(this.rtbWarning);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.rtbBPrice);
            this.Controls.Add(this.dgbTradeInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgbTradeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.RichTextBox rtbWarning;
    }
}

