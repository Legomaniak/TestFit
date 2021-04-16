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
        private double _Gradient = 0.01;

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

        public double TempK0 = -273.15;
        double StefanBoltzmannKonst = 5.670373 * Math.Pow(10, -8);
        double c0 = 299792458;//m/s
        double kb = 1.38064852 * Math.Pow(10, -23); //J/K
        double h = 6.626070040 * Math.Pow(10, -34); //J.s
        double c1 = 0;
        double c2 = 0;
        Vector<double> W = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            show.showName = false;

            c1 = 2 * h * Math.Pow(c0, 2); //W-um**4/m**2
            c2 = h * c0 / kb; //um-K

            StartFit();
        }

        public void StartFit()
        {
            Random r = new Random();
            var N = 100;
            switch (cbM.SelectedIndex)
            {
                case 1:
                    BM = new NelderMeadSimplex(Gradient, MaxIter);
                    MyFuncE = ObjectiveFunction.Value(MinimizePlanc);

                    double W1 = 7000*1e-9, W2 = 14000 * 1e-9;
                    double T = 50;
                    W = CreateVector.Dense<double>(N);
                    double dW = (W2 - W1) / N;
                    for (int i = 0; i < N; i++)
                    {
                        W[i] = W1 + i * dW;
                    }

                    //sample0 = PlancLaw(W, r.Next(10, 100) - TempK0);
                    sample0 = PlancLaw(W, T - TempK0);
                    for (int i = 0; i < N; i++)
                    {
                        sample0[i] += 100000*(r.Next(2) - 1.0);
                    }
                    BaseVal = CreateVector.DenseOfArray(new double[] { r.Next(10, 100) });
                    break;
                default:
                    BM = new NelderMeadSimplex(Gradient, MaxIter);
                    MyFuncE = ObjectiveFunction.Value(MinimizeE);

                    double A = 5.1, B = 1.8;
                    var s0 = new double[N];
                    var s1 = new double[N];
                    for (int i = 0; i < N; i++)
                    {
                        s0[i] = A + B * i + (r.Next(2) - 1.0) * B / 2.0;
                        s1[i] = i + (r.Next(2) - 1.0) / 2.0;
                    }
                    sample0 = CreateVector.DenseOfArray(s0);
                    sample1 = CreateVector.DenseOfArray(s1);
                    BaseVal = CreateVector.DenseOfArray(new double[] { 10, 1 });
                    break;
            }

            show.addData(sample0.ToArray(), 0);
            try
            {
                var res = BM.FindMinimum(MyFuncE, BaseVal);
                var x = res.MinimizingPoint;
                switch (cbM.SelectedIndex)
                {
                    case 1:
                        show.addData(PlancLaw(W, x[0] - TempK0).ToArray(), 1);
                        break;
                    default:
                        show.addData((x[1] * sample1 + x[0]).ToArray(), 1);
                        break;
                }
                var sb = new StringBuilder();
                sb.AppendLine("Iterations " + res.Iterations);
                sb.AppendLine("Value " + res.FunctionInfoAtMinimum.Value);
                sb.AppendLine("MinimizingPoint\n" + string.Join("\n", res.MinimizingPoint));
                //sb.AppendLine("\nFunctionInfoAtMinimum\n" + string.Join("\n", res.FunctionInfoAtMinimum.Point));

                Results = sb.ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Vector<double> MinimizeG(Vector<double> v)
        {
            return v;//TODO:Compute gradient function
        }
        public double MinimizeE(Vector<double> x)
        {
            return (sample0 - sample1 * x[1] - x[0]).PointwisePower(2).Sum();
        }
        public double MinimizePlanc(Vector<double> x)
        {
            sample1 = PlancLaw(W, x[0] - TempK0);
            return (sample0 - sample1).PointwisePower(2).Sum();
        }


        public double PlancLaw(double y, double T)
        {
            return 2 * h * Math.Pow(c0, 2) / Math.Pow(y, 5) / (Math.Exp(h * c0 / kb / y / T) - 1); //W/(m**2-um)
        }
        public Vector<double> PlancLaw(Vector<double> y, double T)
        {
            var c3 = c2 / T;
            return c1 / y.PointwisePower(5) / ((c3 / y).PointwiseExp() - 1); //W/(m**2-um)
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            show.Clear();
            StartFit();
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
                sample0 = LoadVector(Path1);
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
