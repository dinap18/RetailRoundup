using BE;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PL.ViewModel
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : UserControl
    {

        public BL.BL db = new BL.BL();
        public Dictionary<string, ChartValues<int>> counts = new Dictionary<string, ChartValues<int>>();
        public Dictionary<string, double> counts1 = new Dictionary<string, double>();
        public List<Product> purchased = new List<Product>();

        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection PieCollection { get; set; } 
        
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public Graphs()
        {
            InitializeComponent();

            foreach (var i in db.GetPurchases())
            {
                foreach (var j in i.products)
                {
                    int value;
                    if (!counts.ContainsKey(j.productName))
                    {
                        counts[j.productName] = new ChartValues<int>();
                        counts[j.productName].Add(1);
                    }
                    else
                    {
                        counts[j.productName][0]+=1;
                    }
                    purchased.Add(j);
                }
            }
            var groups = purchased.GroupBy(x => x.productName);
            List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
            SeriesCollection = new SeriesCollection();
            List<string> labelNames = new List<string>();
            foreach (var g in groups)
            {
                int count = g.Count() - 1;
                string name = g.First().productName;
                labelNames.Add(name);
                SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                
        }
            
            //Labels = purchased.Select(x=>x.productName).Distinct().ToArray();
            //   g.ItemsSource = forGraph;
            DataContext = this;
            pickType.ItemsSource = new List<string> { "Item Count", "Products by Month", "Products by Week" ,"Products by Day",
            "Stores by Month", "Stores by Week","Stores by Day","Category by Month","Category by Week","Category by Day","Price by Month", "Price by Week", "Price by Day"};




         
            foreach (var i in db.GetPurchases())
            {
                foreach (var j in i.products)
                {
                    if (!counts1.ContainsKey(j.productName))
                    {
                        counts1[j.productName] = 0;
                        counts1[j.productName] += 1;
                    }
                    else
                    {
                        counts1[j.productName] += 1;
                    }
                    purchased.Add(j);
                }
            }
             groups = purchased.GroupBy(x => x.productName);
            forGraph = new List<KeyValuePair<string, int>>();
            PieCollection = new SeriesCollection();
            labelNames = new List<string>();
            foreach (var g in groups)
            {
                int count = g.Count() - 1;
                string name = g.First().productName;
                labelNames.Add(name);
                PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


            }
            Labels = purchased.Select(x => x.productName).Distinct().ToArray();
            //   g.ItemsSource = forGraph;
            Pie.DataContext = this;
            pickMonth.Visibility = Visibility.Hidden;
            pickWeek.Visibility = Visibility.Hidden;
        }




        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pickWeek.SelectedIndex = -1;
            pickWeek.SelectedItem = null;
            pickMonth.SelectedIndex = -1;
            pickMonth.SelectedItem = null;

            if (pickType.SelectedIndex == 0)
            {

                pickMonth.Visibility = Visibility.Hidden;
                pickWeek.Visibility = Visibility.Hidden;
                foreach (var i in db.GetPurchases())
                {
                    foreach (var j in i.products)
                    {
                        int value;
                        if (!counts.ContainsKey(j.productName))
                        {
                            counts[j.productName] = new ChartValues<int>();
                            counts[j.productName].Add(1);
                            counts1[j.productName] = 1;
                        }
                        else
                        {
                            counts[j.productName][0] += 1;
                            counts1[j.productName] += 1;
                        }
                        purchased.Add(j);
                    }
                }
                var groups = purchased.GroupBy(x => x.productName);
                List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                SeriesCollection.Clear();
                if (PieCollection != null)
                {
                    PieCollection.Clear();
                }
                List<string> labelNames = new List<string>();
                foreach (var g in groups)
                {
                    int count = g.Count() - 1;
                    string name = g.First().productName;
                    labelNames.Add(name);
                    SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                    PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


                }
               // Labels = purchased.Select(x => x.productName).Distinct().ToArray();
                //   g.ItemsSource = forGraph;
                DataContext = null;
                graph.DataContext = this;
            }
            if (pickType.SelectedIndex == 1 || pickType.SelectedIndex == 2 || pickType.SelectedIndex == 3) // product
            {

                pickMonth.Visibility = Visibility.Visible;

                pickWeek.Visibility = Visibility.Hidden;
                pickMonth.ItemsSource = db.GetProducts().Select(x => x.productName).Distinct().ToList();

            }
        
            if (pickType.SelectedIndex == 4 || pickType.SelectedIndex == 5 || pickType.SelectedIndex == 6) // stores
            {

                pickMonth.Visibility = Visibility.Visible;

                pickWeek.Visibility = Visibility.Hidden;
                pickMonth.ItemsSource = db.GetStores().Select(x=>x.name.ToLower()).Distinct().ToList();

            }
            if (pickType.SelectedIndex == 7 || pickType.SelectedIndex == 8 || pickType.SelectedIndex == 9) // category
            {

                pickMonth.Visibility = Visibility.Visible;

                pickWeek.Visibility = Visibility.Hidden;
                pickMonth.ItemsSource = new List<string> { BE.Category.Clothes.ToString(), BE.Category.Cosmetics.ToString(),BE.Category.Electronics.ToString(),
                    BE.Category.Food.ToString(),BE.Category.Stationery.ToString(),BE.Category.Toys.ToString() };

            }
            if (pickType.SelectedIndex == 10 ) // price by month
            {

                pickMonth.Visibility = Visibility.Visible;
                pickWeek.Visibility = Visibility.Hidden;
                pickMonth.ItemsSource = new List<string> { "1","2","3","4","5","6","7","8","9","10","11","12" };

            }
        }
        private void pickMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            counts.Clear();
            counts1.Clear();
            DataContext = null;
            if (pickMonth.SelectedItem != null)
            {
                if (pickType.SelectedIndex == 1)
                {
                    foreach (var i in db.GetPurchases().Where(x => x.products.Select(y => y.productName).Contains(pickMonth.SelectedItem)))
                    {
                        
                        if (!counts.ContainsKey(i.purchaseDate.Month.ToString()))
                        {
                            counts[i.purchaseDate.Month.ToString()] = new ChartValues<int>();
                            counts[i.purchaseDate.Month.ToString()].Add(1);

                            counts1[i.purchaseDate.Month.ToString()]=1;
                        }
                        else
                        {
                            counts1[i.purchaseDate.Month.ToString()]+= 1;
                        }

                    }
                    var groups = db.GetPurchases().Where(x => x.products.Select(y => y.productName).Contains(pickMonth.SelectedItem)).GroupBy(x => x.purchaseDate.Month);
                    List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                    SeriesCollection.Clear();
                    if (PieCollection != null)
                    {
                        PieCollection.Clear();
                    }
                    List<string> labelNames = new List<string>();
                    foreach (var g in groups)
                    {
                        string name = g.First().purchaseDate.Month.ToString();
                        labelNames.Add(name);
                        SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                        PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });

                    }
                    //   g.ItemsSource = forGraph;
                    DataContext = null;
                    graph.DataContext = this;

                    pickWeek.Visibility = Visibility.Hidden;
                }
                if (pickType.SelectedIndex == 2 || pickType.SelectedIndex==3 ) // product by week or day
                {
                    pickWeek.ItemsSource = db.GetPurchases().Where(x => x.products.Select(y => y.productName).Contains(pickMonth.SelectedItem)).Select(x => x.purchaseDate.Month).Distinct();
                    pickWeek.SelectedIndex = -1;
                    pickWeek.SelectedItem = null;

                    pickWeek.Visibility = Visibility.Visible;

                }
                if (pickType.SelectedIndex == 5 || pickType.SelectedIndex == 6) // store by week or day
                {
                    pickWeek.ItemsSource = db.GetPurchases().Where(x => x.seller.ToString().ToLower() == pickMonth.SelectedItem.ToString().ToLower()).Select(x => x.purchaseDate.Month).Distinct();
                    pickWeek.SelectedIndex = -1;
                    pickWeek.SelectedItem = null;


                    pickWeek.Visibility = Visibility.Visible;
                }
                if (pickType.SelectedIndex == 8 || pickType.SelectedIndex == 9) // category by week or day
                {
                    pickWeek.ItemsSource = db.GetPurchases().Where(x => x.products.First().category.ToString()==pickMonth.SelectedItem.ToString()).Select(x => x.purchaseDate.Month).Distinct();
                    pickWeek.SelectedIndex = -1;
                    pickWeek.SelectedItem = null;


                    pickWeek.Visibility = Visibility.Visible;
                }
                if (pickType.SelectedIndex == 4)
                {
                    foreach (var i in db.GetPurchases().Where(x => x.seller.ToLower() == pickMonth.SelectedItem.ToString().ToLower()))
                    {
                        int value;
                        if (!counts.ContainsKey(i.purchaseDate.Month.ToString()))
                        {
                            counts[i.purchaseDate.Month.ToString()] = new ChartValues<int>();
                            counts[i.purchaseDate.Month.ToString()].Add(1);
                            counts1[i.purchaseDate.Month.ToString()] = 1;
                        }
                        else
                        {
                            counts[i.purchaseDate.Month.ToString()][0] += 1;
                            counts1[i.purchaseDate.Month.ToString()]+= 1;
                        }

                    }
                    var groups = db.GetPurchases().Where(x => x.seller.ToLower() == pickMonth.SelectedItem.ToString().ToLower()).GroupBy(x => x.purchaseDate.Month);
                    List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                    SeriesCollection.Clear();
                    if(Pie.Visibility==Visibility.Visible)
                    {
                        PieCollection.Clear();
                    }
                    List<string> labelNames = new List<string>();
                    foreach (var g in groups)
                    {
                        string name = g.First().purchaseDate.Month.ToString();
                        labelNames.Add(name);
                        SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                        PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });

                    }
                    //   g.ItemsSource = forGraph;
                    DataContext = null;
                    graph.DataContext = this;
                }
                if (pickType.SelectedIndex == 10)
                {
                    foreach (var i in db.GetPurchases().Where(x => x.purchaseDate.Month.ToString() == pickMonth.SelectedItem.ToString()))
                    {
                        int value;
                        if (!counts.ContainsKey(i.purchaseId.ToString()))
                        {
                            counts[i.purchaseId.ToString()] = new ChartValues<int>();
                            counts[i.purchaseId.ToString()].Add((int)i.products.Sum(x => x.price));
                        }

                    }
                    var groups = db.GetPurchases().Where(x => x.purchaseDate.Month.ToString() == pickMonth.SelectedItem.ToString());
                    List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                    SeriesCollection.Clear();
                    List<string> labelNames = new List<string>();
                    foreach (var g in groups)
                    {
                        string name = g.purchaseId.ToString();
                        labelNames.Add(name);
                        SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });


                    }
                    //   g.ItemsSource = forGraph;
                    graph.DataContext = this;
                }
                if (pickType.SelectedIndex == 7)//price by month
                {
                    foreach (var i in db.GetPurchases().Where(x => x.products.First().category.ToString() == pickMonth.SelectedItem.ToString()))
                    {
                        int value;
                        if (!counts.ContainsKey(i.purchaseDate.Month.ToString()))
                        {
                            counts[i.purchaseDate.Month.ToString()] = new ChartValues<int>();
                            counts[i.purchaseDate.Month.ToString()].Add(1);
                        }
                        else
                        {
                            counts[i.purchaseDate.Month.ToString()][0] += 1;
                        }

                    }
                    var groups = db.GetPurchases().Where(x => x.products.First().category.ToString() == pickMonth.SelectedItem.ToString()).GroupBy(x => x.purchaseDate.Month);
                    List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                    SeriesCollection.Clear();
                    List<string> labelNames = new List<string>();
                    foreach (var g in groups)
                    {
                        string name = g.First().purchaseDate.Month.ToString();
                        labelNames.Add(name);
                        SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });


                    }
                    //   g.ItemsSource = forGraph;
                    graph.DataContext = this;
                }
            }
        }

        private void pickWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (pickWeek.SelectedItem != null)
                {
                    if (pickType.SelectedIndex == 3)
                    {

                        foreach (var i in db.GetPurchases().Where(x => x.products.Select(y => y.productName).Contains(pickMonth.SelectedItem.ToString()) && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())))
                        {
                            int value = i.purchaseDate.Day;
                            if (!counts.ContainsKey(value.ToString()))
                            {
                                counts[value.ToString()] = new ChartValues<int>();
                                counts[value.ToString()].Add(1);
                                counts1[value.ToString()] = 1;
                            }
                            else
                            {
                                counts[value.ToString()][0] += 1;
                                counts1[value.ToString()] += 1;
                            }

                        }
                        var groups = db.GetPurchases().Where(x => x.products.Select(y => y.productName).Contains(pickMonth.SelectedItem.ToString()) && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())).GroupBy(x => (int)x.purchaseDate.Day);
                        List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();

                        SeriesCollection.Clear();
                        if (Pie.Visibility == Visibility.Visible)
                        {
                            PieCollection.Clear();
                        }
                        List<string> labelNames = new List<string>();
                        foreach (var g in groups)
                        {
                            string name = ((int)(g.First().purchaseDate.Day)).ToString();
                            labelNames.Add(name);
                            SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                            PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


                        }
                        //   g.ItemsSource = forGraph;
                        graph.DataContext = this;
                    }
                    if (pickType.SelectedIndex == 2)
                    {
                        foreach (var i in db.GetPurchases().Where(x => x.products.Select(y => y.productName).Contains(pickMonth.SelectedItem.ToString()) && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())))
                        {
                            int value = i.purchaseDate.Day / 7;
                            if (!counts.ContainsKey(value.ToString()))
                            {
                                counts[value.ToString()] = new ChartValues<int>();
                                counts[value.ToString()].Add(1);
                                counts1[value.ToString()] = 1;
                            }
                            else
                            {
                                counts[value.ToString()][0] += 1;
                                counts1[value.ToString()] += 1;
                            }

                        }
                        var groups = db.GetPurchases().Where(x => x.products.Select(y => y.productName).Contains(pickMonth.SelectedItem.ToString()) && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())).GroupBy(x => (int)x.purchaseDate.Day / 7);
                        List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                        SeriesCollection.Clear();
                        if (PieCollection!=null)
                        {
                            PieCollection.Clear();
                        }
                        List<string> labelNames = new List<string>();
                        foreach (var g in groups)
                        {
                            string name = ((int)(g.First().purchaseDate.Day / 7)).ToString();
                            labelNames.Add(name);
                            SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                            PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


                        }
                        //   g.ItemsSource = forGraph;
                        graph.DataContext = this;
                    }
                    if (pickType.SelectedIndex == 5)//store by week
                    {
                        foreach (var i in db.GetPurchases().Where(x => x.seller.ToLower()==pickMonth.SelectedItem.ToString().ToLower() && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())))
                        {
                            int value = i.purchaseDate.Day / 7;
                            if (!counts.ContainsKey(value.ToString()))
                            {
                                counts[value.ToString()] = new ChartValues<int>();
                                counts[value.ToString()].Add(1);
                                counts1[value.ToString()] = 1;
                            }
                            else
                            {
                                counts[value.ToString()][0] += 1;
                                counts1[value.ToString()] += 1;
                            }

                        }
                        var groups = db.GetPurchases().Where(x => x.seller.ToLower()==pickMonth.SelectedItem.ToString().ToLower()&& (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())).GroupBy(x => (int)x.purchaseDate.Day / 7);
                        List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                        SeriesCollection.Clear();
                        if (PieCollection!=null)
                        {
                            PieCollection.Clear();
                        }
                        List<string> labelNames = new List<string>();
                        foreach (var g in groups)
                        {
                            string name = ((int)(g.First().purchaseDate.Day / 7)).ToString();
                            labelNames.Add(name);
                            SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                            PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


                        }
                        //   g.ItemsSource = forGraph;
                        graph.DataContext = this;
                    }
                    if (pickType.SelectedIndex == 6)//store by day
                    {

                        foreach (var i in db.GetPurchases().Where(x => x.seller.ToLower()==pickMonth.SelectedItem.ToString() && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())))
                        {
                            int value = i.purchaseDate.Day;
                            if (!counts.ContainsKey(value.ToString()))
                            {
                                counts[value.ToString()] = new ChartValues<int>();
                                counts[value.ToString()].Add(1);
                                counts1[value.ToString()] = 1;
                            }
                            else
                            {
                                counts[value.ToString()][0] += 1;
                                counts1[value.ToString()] += 1;
                            }

                        }
                        var groups = db.GetPurchases().Where(x => x.seller.ToLower()==pickMonth.SelectedItem.ToString() && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())).GroupBy(x => (int)x.purchaseDate.Day);
                        List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();

                        SeriesCollection.Clear();
                        if (PieCollection != null)
                        {
                            PieCollection.Clear();
                        }
                        List<string> labelNames = new List<string>();
                        foreach (var g in groups)
                        {
                            string name = ((int)(g.First().purchaseDate.Day)).ToString();
                            labelNames.Add(name);
                            SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                            PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


                        }
                        //   g.ItemsSource = forGraph;
                        graph.DataContext = this;
                    }
                    if (pickType.SelectedIndex == 8)//category by week
                    {
                        foreach (var i in db.GetPurchases().Where(x => x.products.First().category.ToString()==pickMonth.SelectedItem.ToString() && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())))
                        {
                            int value = i.purchaseDate.Day / 7;
                            if (!counts.ContainsKey(value.ToString()))
                            {
                                counts[value.ToString()] = new ChartValues<int>();
                                counts[value.ToString()].Add(1);
                                counts1[value.ToString()] = 1;
                            }
                            else
                            {
                                counts[value.ToString()][0] += 1;
                                counts1[value.ToString()] += 1;
                            }

                        }
                        var groups = db.GetPurchases().Where(x => x.products.First().category.ToString() == pickMonth.SelectedItem.ToString() && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())).GroupBy(x => (int)x.purchaseDate.Day / 7);
                        List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
                        SeriesCollection.Clear();
                        if (PieCollection != null)
                        {
                            PieCollection.Clear();
                        }
                        List<string> labelNames = new List<string>();
                        foreach (var g in groups)
                        {
                            string name = ((int)(g.First().purchaseDate.Day / 7)).ToString();
                            labelNames.Add(name);
                            SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                            PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


                        }
                        //   g.ItemsSource = forGraph;
                        graph.DataContext = this;
                    }
                    if (pickType.SelectedIndex == 9)//category by day
                    {

                        foreach (var i in db.GetPurchases().Where(x => x.products.First().category.ToString() == pickMonth.SelectedItem.ToString() && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())))
                        {
                            int value = i.purchaseDate.Day;
                            if (!counts.ContainsKey(value.ToString()))
                            {
                                counts[value.ToString()] = new ChartValues<int>();
                                counts[value.ToString()].Add(1);
                                counts1[value.ToString()] = 1;
                            }
                            else
                            {
                                counts[value.ToString()][0] += 1;
                                counts1[value.ToString()] += 1;
                            }

                        }
                        var groups = db.GetPurchases().Where(x => x.products.First().category.ToString() == pickMonth.SelectedItem.ToString() && (x.purchaseDate.Month.ToString() == pickWeek.SelectedItem.ToString())).GroupBy(x => (int)x.purchaseDate.Day);
                        List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();

                        SeriesCollection.Clear();
                        if (PieCollection != null)
                        {
                            PieCollection.Clear();
                        }
                        List<string> labelNames = new List<string>();
                        foreach (var g in groups)
                        {
                            string name = ((int)(g.First().purchaseDate.Day)).ToString();
                            labelNames.Add(name);
                            SeriesCollection.Add(new ColumnSeries { Title = name, Values = counts[name] });

                            PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


                        }
                        //   g.ItemsSource = forGraph;
                        graph.DataContext = this;
                    }
                 
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Please try again");
            }

        }

        private void switchType_Click(object sender, RoutedEventArgs e)
        {
            if (Pie.Visibility == Visibility.Visible)
            {
                barChart.Visibility = Visibility.Visible;
                Pie.Visibility = Visibility.Hidden;

                Button button = sender as Button;
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                bitmap.UriSource = new Uri("C:/Users/dp18/source/repos/WPFProject/Pictures/pie-chart.png", UriKind.Absolute);
                bitmap.EndInit();
                brush.ImageSource = bitmap;
                button.Background = brush;
            }
            else
            {

                Pie.Visibility = Visibility.Visible;
                barChart.Visibility = Visibility.Hidden;
                Button button = sender as Button;
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                bitmap.UriSource = new Uri("C:/Users/dp18/source/repos/WPFProject/Pictures/bar-chart.png", UriKind.Absolute);
                bitmap.EndInit();
                brush.ImageSource = bitmap;
                button.Background = brush;
                counts.Clear();
                pickType.SelectedIndex = 0;
                pickMonth.SelectedItem = null;
                pickWeek.SelectedItem = null;
            }
            counts.Clear();
            pickType.SelectedIndex = 0;
            pickMonth.SelectedItem = null;
            pickWeek.SelectedItem = null;

         Dictionary<string, double> counts1 = new Dictionary<string, double>();
            foreach (var i in db.GetPurchases())
            {
                foreach (var j in i.products)
                {
                    if (!counts1.ContainsKey(j.productName))
                    {
                        counts1[j.productName] = 0;
                        counts1[j.productName]+=1;
                    }
                    else
                    {
                        counts1[j.productName] += 1;
                    }
                    purchased.Add(j);
                }
            }
            var groups = purchased.GroupBy(x => x.productName);
            List<KeyValuePair<string, int>> forGraph = new List<KeyValuePair<string, int>>();
            PieCollection = new SeriesCollection();
            List<string> labelNames = new List<string>();
            foreach (var g in groups)
            {
                int count = g.Count() - 1;
                string name = g.First().productName;
                labelNames.Add(name);
                PieCollection.Add(new PieSeries { Title = name, Values = new ChartValues<ObservableValue> { new ObservableValue((double)counts1[name]) } });


            }
            Labels = purchased.Select(x => x.productName).Distinct().ToArray();
            //   g.ItemsSource = forGraph;
            Pie.DataContext = this;

        }
    }
    
}