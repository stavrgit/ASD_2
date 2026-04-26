namespace ASD_2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            panelTop = new Panel();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboTestType = new ComboBox();
            txtNmin = new TextBox();
            txtNmax = new TextBox();
            txtPoints = new TextBox();
            txtReal = new TextBox();
            btnRun = new Button();
            chartTime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            statusLabel = new Label();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartStats).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.Controls.Add(label3);
            panelTop.Controls.Add(label2);
            panelTop.Controls.Add(label1);
            panelTop.Controls.Add(comboTestType);
            panelTop.Controls.Add(txtNmin);
            panelTop.Controls.Add(txtNmax);
            panelTop.Controls.Add(txtPoints);
            panelTop.Controls.Add(txtReal);
            panelTop.Controls.Add(btnRun);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(900, 92);
            panelTop.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(441, 13);
            label3.Name = "label3";
            label3.Size = new Size(37, 20);
            label3.TabIndex = 8;
            label3.Text = "Шаг";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(334, 14);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 7;
            label2.Text = "мин";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(231, 14);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 6;
            label1.Text = "макс";
            // 
            // comboTestType
            // 
            comboTestType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTestType.Items.AddRange(new object[] { "Худший случай (aaaaaa...)", "Лучший случай (abcdef...)", "Случайный", "Реальный текст" });
            comboTestType.Location = new Point(12, 37);
            comboTestType.Name = "comboTestType";
            comboTestType.Size = new Size(200, 28);
            comboTestType.TabIndex = 0;
            // 
            // txtNmin
            // 
            txtNmin.Location = new Point(231, 37);
            txtNmin.Name = "txtNmin";
            txtNmin.Size = new Size(73, 27);
            txtNmin.TabIndex = 1;
            txtNmin.Text = "100";
            // 
            // txtNmax
            // 
            txtNmax.Location = new Point(334, 37);
            txtNmax.Name = "txtNmax";
            txtNmax.Size = new Size(74, 27);
            txtNmax.TabIndex = 2;
            txtNmax.Text = "1000";
            // 
            // txtPoints
            // 
            txtPoints.Location = new Point(445, 37);
            txtPoints.Name = "txtPoints";
            txtPoints.Size = new Size(73, 27);
            txtPoints.TabIndex = 3;
            txtPoints.Text = "5";
            // 
            // txtReal
            // 
            txtReal.Location = new Point(783, 59);
            txtReal.Name = "txtReal";
            txtReal.Size = new Size(117, 27);
            txtReal.TabIndex = 4;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(560, 10);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(100, 54);
            btnRun.TabIndex = 5;
            btnRun.Text = "Запустить";
            btnRun.Click += btnRun_Click;
            // 
            // chartTime
            // 
            chartArea1.Name = "TimeArea";
            chartTime.ChartAreas.Add(chartArea1);
            chartTime.Dock = DockStyle.Top;
            legend1.Name = "Legend1";
            chartTime.Legends.Add(legend1);
            chartTime.Location = new Point(0, 92);
            chartTime.Name = "chartTime";
            series1.ChartArea = "TimeArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Array";
            series2.ChartArea = "TimeArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "List";
            chartTime.Series.Add(series1);
            chartTime.Series.Add(series2);
            chartTime.Size = new Size(900, 250);
            chartTime.TabIndex = 1;
            // 
            // chartStats
            // 
            chartArea2.Name = "StatsArea";
            chartStats.ChartAreas.Add(chartArea2);
            chartStats.Dock = DockStyle.Fill;
            legend2.Name = "Legend2";
            chartStats.Legends.Add(legend2);
            chartStats.Location = new Point(0, 342);
            chartStats.Name = "chartStats";
            series3.ChartArea = "StatsArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend2";
            series3.Name = "ArrayStats";
            series4.ChartArea = "StatsArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend2";
            series4.Name = "ListStats";
            chartStats.Series.Add(series3);
            chartStats.Series.Add(series4);
            chartStats.Size = new Size(900, 231);
            chartStats.TabIndex = 0;
            // 
            // statusLabel
            // 
            statusLabel.Dock = DockStyle.Bottom;
            statusLabel.Location = new Point(0, 573);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(900, 27);
            statusLabel.TabIndex = 3;
            statusLabel.Text = "Готово";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            ClientSize = new Size(900, 600);
            Controls.Add(chartStats);
            Controls.Add(chartTime);
            Controls.Add(panelTop);
            Controls.Add(statusLabel);
            Name = "Form1";
            Text = "ЛР2 — Суффиксные деревья";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chartTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartStats).EndInit();
            ResumeLayout(false);
        }

        private Panel panelTop;
        private ComboBox comboTestType;
        private TextBox txtNmin;
        private TextBox txtNmax;
        private TextBox txtPoints;
        private TextBox txtReal;
        private Button btnRun;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTime;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStats;

        private Label statusLabel;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
