using ASD_2;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ASD_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            int nMin = int.Parse(txtNmin.Text);
            int nMax = int.Parse(txtNmax.Text);
            int points = int.Parse(txtPoints.Text);

            chartTime.Series["Array"].Points.Clear();
            chartTime.Series["List"].Points.Clear();

            chartStats.Series["ArrayStats"].Points.Clear();
            chartStats.Series["ListStats"].Points.Clear();

            chartTime.Series["Array"].LegendText = "Array (дерево)";
            chartTime.Series["List"].LegendText = "List (дерево)";
            chartStats.Series["ArrayStats"].LegendText = "Array (массив)";
            chartStats.Series["ListStats"].LegendText = "List (массив)";

            chartTime.Series["Array"].BorderWidth = 3;
            chartTime.Series["List"].BorderWidth = 3;
            chartStats.Series["ArrayStats"].BorderWidth = 3;
            chartStats.Series["ListStats"].BorderWidth = 3;

            chartTime.Series["Array"].Color = Color.DarkGreen;
            chartTime.Series["List"].Color = Color.Red;
            chartStats.Series["ArrayStats"].Color = Color.DarkGreen;
            chartStats.Series["ListStats"].Color = Color.Red;

            chartTime.ChartAreas[0].AxisX.Title = "Размер строки (n)";
            chartTime.ChartAreas[0].AxisY.Title = "Время (мс)";
            chartStats.ChartAreas[0].AxisX.Title = "Размер строки (n)";
            chartStats.ChartAreas[0].AxisY.Title = "Время (мс)";



            int step = (nMax - nMin) / (points - 1);

            for (int n = nMin; n <= nMax; n += step)
            {
                string s = GenerateString(n);
                Program.GlobalText = s;
                var tree = new CompressedSuffixTree(s);

                // Построение дерева
                var sw = Stopwatch.StartNew();
                var treeArray = new CompressedSuffixTree(s);
                sw.Stop();
                long tArrayBuild = sw.ElapsedTicks;
                var statsArray = treeArray.ComputeStats();

                sw.Restart();
                var treeList = new CompressedSuffixTree(s);
                sw.Stop();
                long tListBuild = sw.ElapsedTicks;
                var statsList = treeList.ComputeStats();

                // Обход дерева 
                sw.Restart();
                treeArray.BuildSuffixArray();
                sw.Stop();
                long tArrayDFS = sw.ElapsedTicks;

                sw.Restart();
                treeList.BuildSuffixArray();
                sw.Stop();
                long tListDFS = sw.ElapsedTicks;

                // График 1 — построение дерева
                chartTime.Series["Array"].Points.AddXY(n, tArrayBuild);
                chartTime.Series["List"].Points.AddXY(n, tListBuild);

                // График 2 — обход дерева
                chartStats.Series["ArrayStats"].Points.AddXY(n, tArrayDFS);
                chartStats.Series["ListStats"].Points.AddXY(n, tListDFS);


                statusLabel.Text =
                        $"n={n} | " +
                        $"Array: ветвлений={statsArray.branching}, ср. степень={statsArray.avgDegree:F2} | " +
                        $"List: ветвлений={statsList.branching}, ср. степень={statsList.avgDegree:F2}";


                Application.DoEvents();
                try
                {
                    tree.Validate();
                    //statusLabel.Text = "Корректность: OK";
                }
                catch (Exception ex)
                {
                    //statusLabel.Text = "Ошибка: " + ex.Message;
                }

            }

        }

        private string GenerateString(int n)
        {
            string type = comboTestType.SelectedItem?.ToString() ?? "";

            if (type.Contains("Худший"))
                return new string('a', n);

            if (type.Contains("Лучший"))
            {
                return string.Concat(Enumerable.Range(0, n)
                .Select(i => (char)(1000 + i))); 
            }

            if (type.Contains("Случайный"))
            {
                var rnd = new Random();
                char[] arr = new char[n];
                for (int i = 0; i < n; i++)
                    arr[i] = (char)('a' + rnd.Next(26));
                return new string(arr);
            }

            if (type.Contains("Реальный") && txtReal.Text.Length > 0)
            {
                string real = txtReal.Text;
                while (real.Length < n)
                    real += real;
                return real.Substring(0, n);
            }

            return new string('a', n);
        }
    }

}