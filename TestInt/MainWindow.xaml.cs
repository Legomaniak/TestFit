using MathNet.Numerics.Interpolation;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestInt
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private double _Wstart = 8000;

        public double Wstart
        {
            get { return _Wstart; }
            set { _Wstart = value; OnPropertyChanged("Wstart"); }
        }
        private double _Wstop = 12000;

        public double Wstop
        {
            get { return _Wstop; }
            set { _Wstop = value; OnPropertyChanged("Wstop"); }
        }
        private int _Wnum = 100;

        public int Wnum
        {
            get { return _Wnum; }
            set { _Wnum = value; OnPropertyChanged("Wnum"); }
        }
        private string _Path1 = "D:\\repos\\Pythoviny\\APPLIC\\FIFI\\testW.bin";

        public string Path1
        {
            get { return _Path1; }
            set { _Path1 = value; OnPropertyChanged("Path1"); }
        }
        private string _Path2 = "D:\\repos\\Pythoviny\\APPLIC\\FIFI\\testD.bin";

        public string Path2
        {
            get { return _Path2; }
            set { _Path2 = value; OnPropertyChanged("Path2"); }
        }
        private string _Results = "";

        public string Results
        {
            get { return _Results; }
            set { _Results = value; OnPropertyChanged("Results"); }
        }

        double[] sample0, sample1;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            show.showName = false;

            sample0 = LoadVector(Path1).ToArray();
            sample1 = LoadVector(Path2).ToArray();

            Button_Click(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            show.addData(sample0.ToArray(), sample1.ToArray(), 0);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var x = new double[Wnum];
            var y = new double[Wnum];
            IInterpolation spline = null;
            switch (cbM.SelectedIndex)
            {
                case 1:
                    spline = CubicSpline.InterpolateAkimaSorted(sample0, sample1);
                    break;
                default:
                    spline = LinearSpline.Interpolate(sample0, sample1);
                    break;
            }
            var Wdelta = (Wstop - Wstart) / Wnum;
            for (int i = 0; i < Wnum; i++)
            {
                y[i] = Wstart + i * Wdelta;
                x[i] = spline.Interpolate(y[i]);
            }
            sw.Stop();
            var tm = sw.ElapsedMilliseconds;
            show.addData(y, x, 1);

            var sb = new StringBuilder();
            sb.AppendLine("Time [ms] " + tm);

            Results = sb.ToString();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path1 = ofd.FileName;
            }
            try
            {
                sample0 = LoadVector(Path1).ToArray();
            }
            catch
            {

            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path2 = ofd.FileName;
            }
            try
            {
                sample1 = LoadVector(Path2).ToArray();
            }
            catch
            {

            }
        }
        public Vector<double> LoadVector(string cesta)
        {
            Vector<double> r = null;
            using (FileStream fs = new FileStream(cesta, FileMode.Open))
            {
                using (BinaryReader w = new BinaryReader(fs))
                {
                    int X = w.ReadInt32(); //velikost pole
                    r = CreateVector.Dense<double>(X);
                    for (int j = 0; j < X; j++)
                    {
                        r[j] = w.ReadDouble();
                    }
                }
            }
            return r;
        }
        public void SaveVector(string cesta, Vector<double> data)
        {
            using (FileStream fs = new FileStream(cesta, FileMode.OpenOrCreate))
            {
                using (BinaryWriter w = new BinaryWriter(fs))
                {
                    w.Write(data.Count);
                    for (int j = 0; j < data.Count; j++)
                    {
                        w.Write(data[j]);
                    }
                }
            }
        }
    }
}
