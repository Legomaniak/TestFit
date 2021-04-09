using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Optimization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TestFit
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
        Vector<double> sample0, sample1;
        IUnconstrainedMinimizer BM;
        public IObjectiveFunction MyFuncE;
        Vector<double> BaseVal = CreateVector.DenseOfArray(new double[] { 10 , 1 });
        private int _MaxIter = 100;

        public int MaxIter
        {
            get { return _MaxIter; }
            set { _MaxIter = value; OnPropertyChanged("MaxIter"); }
        }
        private double _Gradient = 1;

        public double Gradient
        {
            get { return _Gradient; }
            set { _Gradient = value; OnPropertyChanged("Gradient"); }
        }

        private string _Path1 = "Test data";

        public string Path1
        {
            get { return _Path1; }
            set { _Path1 = value; OnPropertyChanged("Path1"); }
        }
        private string _Path2 = "Test data*2+5";

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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            show.showName = false;

            BM = new MyConjugateGradientMinimizer(Gradient, MaxIter);
            MyFuncE = ObjectiveFunction.Gradient(MinimizeE, MinimizeG);

            Random r = new Random();
            var N = 100;
            double A = 5.1, B = 1.8;
            var s0 = new double[N];
            var s1 = new double[N];
            for (int i = 0; i < N; i++)
            {
                s0[i] = i + (r.Next(2)-1.0) / 2.0;
                s1[i] = A + B * i + (r.Next(2) - 1.0) * B / 2.0;
            }
            sample0 = CreateVector.DenseOfArray(s1);
            sample1 = CreateVector.DenseOfArray(s0);
            StartFit();
        }

        public void StartFit()
        {
            show.addData(sample0.ToArray(), 0);
            try
            {
                var res = BM.FindMinimum(MyFuncE, BaseVal);
                var x = res.MinimizingPoint;
                show.addData((x[1]*sample1+x[0]).ToArray(), 1);
                var sb = new StringBuilder();
                sb.AppendLine("Iterations " + res.Iterations);
                sb.AppendLine("Value " + res.FunctionInfoAtMinimum.Value);
                sb.AppendLine("MinimizingPoint\n" + string.Join("\n", res.MinimizingPoint));
                sb.AppendLine("\nFunctionInfoAtMinimum\n" + string.Join("\n", res.FunctionInfoAtMinimum.Point));

                Results = sb.ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Vector<double> MinimizeG(Vector<double> v)
        {
            return v;
            //return v*0.9;
            //return CreateVector.DenseOfArray(new double[] { v[0]*v[0] });
            //return CreateVector.DenseOfArray(new double[] { v[0] });
            //return CreateVector.DenseOfArray(new double[] { v[0], v[1] });
        }
        public double MinimizeE(Vector<double> x)
        {
            return (sample0 - sample1 * x[1] - x[0]).PointwisePower(2).Sum();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BM = new MyConjugateGradientMinimizer(Gradient, MaxIter);
            show.Clear();
            StartFit();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ofd = new System.Windows.Forms.SaveFileDialog();
            ofd.ShowDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path1 = ofd.FileName;
            }
            try
            {
                sample0 = LoadVector(Path1);
            }
            catch
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var ofd = new System.Windows.Forms.SaveFileDialog();
            ofd.ShowDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path2 = ofd.FileName;
            }
            try
            {
                sample1 = LoadVector(Path2);
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
