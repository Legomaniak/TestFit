using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestFit
{
    /// <summary>
    /// Interaction logic for Show1D.xaml
    /// </summary>
    public partial class Show1D : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected Chart chart1;
        protected Series series1 = new Series();
        protected Series series2 = new Series();
        protected Series series3 = new Series();
        protected Series series4 = new Series();
        protected Legend legend1 = new Legend();
        protected Legend legend2 = new Legend();
        protected Legend legend3 = new Legend();
        protected Legend legend4 = new Legend();
        protected ChartArea chartArea1 = new ChartArea();
        //Title title1 = new Title();

        public string nazevOsyX
        {
            get { return _nazevOsyX; }
            set
            {
                _nazevOsyX = value;
                chartArea1.AxisX.Title = value;
                OnPropertyChanged("nazevOsyX");
            }
        }
        private string _nazevOsyX;
        public string nazevOsyY
        {
            get { return _nazevOsyY; }
            set
            {
                _nazevOsyY = value;
                chartArea1.AxisY.Title = value;
                OnPropertyChanged("nazevOsyY");
            }
        }
        private string _nazevOsyY;
        public string nameGrafu
        {
            get { return _nameGrafu; }
            set
            {
                _nameGrafu = value;
                //title1.Text = value;
                label.Content = _nameGrafu;
                OnPropertyChanged("nameGrafu");
            }
        }
        private string _nameGrafu = "Pixel Spektrum";
        public bool showName
        {
            get { return _showName; }
            set
            {
                _showName = value;
                if (value) row.Height = new GridLength(35);
                else row.Height = new GridLength(0);
            }
        }
        private bool _showName = false;
        public bool AutoX
        {
            get { return _AutoX; }
            set
            {
                _AutoX = value;
                OnPropertyChanged("AutoX");
            }
        }
        private bool _AutoX = true;
        public double MaximumX
        {
            get { return _MaximumX; }
            set
            {
                _MaximumX = value;
                chartArea1.AxisX.Maximum = value;
                OnPropertyChanged("MaximumX");
            }
        }
        private double _MaximumX = 1;
        public double MinimumX
        {
            get { return _MinimumX; }
            set
            {
                _MinimumX = value;
                chartArea1.AxisX.Minimum = value;
                OnPropertyChanged("MinimumX");
            }
        }
        private double _MinimumX = 0;
        public bool AutoY
        {
            get { return _AutoY; }
            set
            {
                _AutoY = value;
                OnPropertyChanged("AutoY");
            }
        }
        private bool _AutoY = true;
        public double MaximumY
        {
            get { return _MaximumY; }
            set
            {
                _MaximumY = value;
                chartArea1.AxisY.Maximum = value;
                OnPropertyChanged("MaximumY");
            }
        }
        private double _MaximumY = 0;
        public double MinimumY
        {
            get { return _MinimumY; }
            set
            {
                _MinimumY = value;
                chartArea1.AxisY.Minimum = value;
                OnPropertyChanged("MinimumY");
            }
        }
        private double _MinimumY = 0;
        //public bool showLine
        //{
        //    get { return _showLine; }
        //    set
        //    {
        //        _showLine = value;
        //        if (chart1.Series.Contains(series2) && !value) chart1.Series.Remove(series2);
        //        else if (!chart1.Series.Contains(series2) && value) chart1.Series.Add(series2);
        //        OnPropertyChanged("showLine");
        //    }
        //}
        //private bool _showLine = false;
        public bool HodnotaRezuShow
        {
            get { return hodnotaRezuShow; }
            set
            {
                hodnotaRezuShow = value;
                OnPropertyChanged("HodnotaRezuShow");
            }
        }
        private bool hodnotaRezuShow = true;
        public int HodnotaRezu
        {
            get { return hodnotaRezu; }
            set
            {
                hodnotaRezu = value;
                OnPropertyChanged("HodnotaRezu");
            }
        }
        private int hodnotaRezu = 0;
        public System.Drawing.Color[] Barvy = new System.Drawing.Color[] { System.Drawing.Color.Red, System.Drawing.Color.Blue, System.Drawing.Color.Green, System.Drawing.Color.Yellow, System.Drawing.Color.Violet, System.Drawing.Color.Orange };
        public Show1D()
        {
            InitializeComponent();
            chart1 = new Chart();
            chart1.BackColor = System.Drawing.Color.White;
            chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
            chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart1.BorderlineWidth = 0;
            chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.None;
            //chart1.Titles.Add(new Title());
            chartArea1.Area3DStyle.Enable3D = false;
            chartArea1.Area3DStyle.Inclination = 90;
            chartArea1.Area3DStyle.IsClustered = false;
            chartArea1.Area3DStyle.IsRightAngleAxes = true;
            chartArea1.Area3DStyle.Perspective = 0;
            chartArea1.Area3DStyle.Rotation = 0;
            chartArea1.Area3DStyle.WallWidth = 0;

            chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)(((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)
                        | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular);
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisX.ScrollBar.Size = 10;
            chartArea1.AxisX.Title = "";
            //chartArea1.AxisX.IsReversed = true;
            chartArea1.AxisX.Minimum = 0;

            chartArea1.AxisY.Minimum = 0;
            chartArea1.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)(((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont)
                        | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea1.AxisY.ScrollBar.Size = 10;
            chartArea1.AxisY.Title = "";
            //chartArea1.AxisY.IsReversed = true;
            //chartArea1.AxisY.TextOrientation = TextOrientation.Rotated270;
            //chartArea1.AxisY.LabelStyle.Angle = -90;
            //chartArea1.AxisY.IsLabelAutoFit = false;

            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "Default";
            chartArea1.ShadowColor = System.Drawing.Color.White;
            chartArea1.ShadowOffset = 0;
            chart1.ChartAreas.Add(chartArea1);

            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Enabled = false;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend1.ForeColor = System.Drawing.Color.Black;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);

            series1.BorderColor = Barvy[0];
            series1.BorderWidth = 3;
            series1.ChartArea = "Default";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = Barvy[0];
            series1.Legend = "Legend1";
            series1.MarkerSize = 8;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.ShadowColor = System.Drawing.Color.Black;
            series1.ShadowOffset = 0;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chart1.Series.Add(series1);

            series2.BorderColor = Barvy[1];
            series2.BorderWidth = 3;
            series2.ChartArea = "Default";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = Barvy[1];
            series2.Legend = "Legend1";
            series2.MarkerSize = 8;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.MarkerStyle = MarkerStyle.None;
            series2.Name = "Series2";
            series2.ShadowColor = System.Drawing.Color.Black;
            series2.ShadowOffset = 0;
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chart1.Series.Add(series2);

            series3.BorderColor = Barvy[2];
            series3.BorderWidth = 3;
            series3.ChartArea = "Default";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = Barvy[2];
            series3.Legend = "Legend1";
            series3.MarkerSize = 8;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.MarkerStyle = MarkerStyle.None;
            series3.Name = "Series3";
            series3.ShadowColor = System.Drawing.Color.Black;
            series3.ShadowOffset = 0;
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chart1.Series.Add(series3);

            wfhSample.Child = chart1;
        }

        double Odsazeni = 1;
        public void ShowLegend(bool show)
        {
            legend1.Enabled = show;
        }
        public void SetMezeX(double minX, double maxX)
        {
            MaximumX = maxX + Odsazeni;
            MinimumX = minX - Odsazeni;
        }
        public void SetMezeY(double minY, double maxY)
        {
            MaximumY = maxY + Odsazeni;
            MinimumY = minY - Odsazeni;
        }
        public void SetRez(int hodnota)
        {
            chart1.Series[0].Points.Clear();
            if (hodnota <= 0) return;
            else if (chart1.Series[1].Points.Count() <= hodnota - 1) return;
            hodnotaRezu = hodnota;
            DataPoint bod = chart1.Series[1].Points[hodnota];
            chart1.Series[0].Points.AddXY(bod.XValue, _MinimumY);
            chart1.Series[0].Points.AddXY(bod.XValue + 0.01, _MaximumY);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataY">dataY >= dataX</param>
        /// <param name="dataX"></param>
        public void addData(double[] dataX, double[] dataY)
        {
            addData(dataX, dataY, 1);
        }
        public void addData(double[] dataX, double[] dataY, int line)
        {
            chart1.Series[line].Points.Clear();
            for (int i = 0; i < dataX.Length; i++)
            {
                chart1.Series[line].Points.AddXY(dataX[i], dataY[i]);
            }

            if (AutoX) SetMezeX(dataX.Min(), dataX.Max());
            if (AutoY) SetMezeY(dataY.Take(dataX.Length).Min(), dataY.Take(dataX.Length).Max());
        }
        public void addData(string jmeno, double[] dataX, double[] dataY, int line)
        {
            chart1.Series[line].Points.Clear();
            chart1.Series[line].Name = jmeno;
            for (int i = 0; i < dataX.Length; i++)
            {
                chart1.Series[line].Points.AddXY(dataX[i], dataY[i]);
            }

            if (AutoX) SetMezeX(dataX.Min(), dataX.Max());
            if (AutoY) SetMezeY(dataY.Take(dataX.Length).Min(), dataY.Take(dataX.Length).Max());
        }
        Random random = new Random();
        public void addData(double[][] data)
        {
            var l = data.GetLength(0);
            if (chart1.Series.Count <= l)
            {
                chart1.Series.Clear();
                for (int i = 0; i < l + 1; i++)
                {
                    var barva = System.Drawing.Color.Black;
                    if (i > Barvy.Count())
                    {
                        barva = System.Drawing.Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
                    }
                    else barva = Barvy[i];

                    Series s = new Series();
                    s.BorderColor = barva;
                    s.BorderWidth = 3;
                    s.ChartArea = "Default";
                    s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                    s.Color = barva;
                    s.Legend = "Default";
                    s.MarkerSize = 8;
                    s.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                    s.Name = "Series" + i;
                    s.ShadowColor = System.Drawing.Color.Black;
                    s.ShadowOffset = 0;
                    s.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    s.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    chart1.Series.Add(s);
                }
            }
            double minX = 0, minY = double.MaxValue, maxX = 0, maxY = 0;
            for (int i = 1; i < l; i++)
            {
                chart1.Series[i].Points.Clear();
                var ll = data[i].GetLength(0);
                for (int j = 0; j < ll; j++)
                {
                    chart1.Series[i].Points.AddXY(j, data[i][j]);
                }
                if (minY > data[i].Min()) minY = data[i].Min();
                if (maxY < data[i].Max()) maxY = data[i].Max();
                if (maxX < ll) maxX = ll;
            }

            if (AutoX) SetMezeX(minX, maxX);
            if (AutoY) SetMezeY(minY, maxY);
            if (HodnotaRezuShow) SetRez(HodnotaRezu);
        }
        public void Clear()
        {
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Points.Clear();
            }
        }
        public void addData(Tuple<int, double> data)
        {
            chart1.Series[1].Points.AddXY(data.Item1, data.Item2);
            if (AutoX) SetMezeX(0, chart1.Series[1].Points.Count());
            if (AutoY) SetMezeY(0, chart1.Series[1].Points.Max(x => x.YValues[0]));
        }
        public void addData(short[] data)
        {
            if (data == null || data.Count() < 1) return;
            if (chart1.Series[1] != null) chart1.Series[1].Points.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                chart1.Series[1].Points.AddXY(i, (double)data[i]);
            }
            if (AutoX) SetMezeX(0, data.Count());
            if (AutoY) SetMezeY(0, data.Max());
            if (HodnotaRezuShow) SetRez(HodnotaRezu);
        }
        public void addData(double[] data)
        {
            if (data == null || data.Count() < 1) return;
            if (chart1.Series[1] != null) chart1.Series[1].Points.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                chart1.Series[1].Points.AddXY(i, data[i]);
            }
            if (AutoX) SetMezeX(0, data.Count());
            if (AutoY) SetMezeY(0, data.Max());
            if (HodnotaRezuShow) SetRez(HodnotaRezu);
        }
        public void addData(double[] data, int kanal)
        {
            if (data == null || data.Count() < 1) return;
            if (chart1.Series[kanal] != null) chart1.Series[kanal].Points.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                chart1.Series[kanal].Points.AddXY(i, data[i]);
            }
            if (AutoX) SetMezeX(0, data.Count());
            if (AutoY) SetMezeY(0, data.Max());
        }
        public void addData(string Name, string NameX, string NameY)
        {
            nameGrafu = Name;
            nazevOsyX = NameX;
            nazevOsyY = NameY;
        }

        public void SetInterval(int axe, double interval)
        {
            chartArea1.Axes[axe].Interval = interval;
        }
    }
}
