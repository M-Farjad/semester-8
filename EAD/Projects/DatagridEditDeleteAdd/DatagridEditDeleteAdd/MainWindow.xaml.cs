using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatagridEditDeleteAdd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<product> lst = new ObservableCollection<product>();

        public MainWindow()
        {
            
            InitializeComponent();

            lst.Add(new product { Pname = "burger", price = 200});
            lst.Add(new product { Pname="biryani", price=150});
            lst.Add(new product { Pname = "Shawarma", price = 150 });
            lst.Add(new product { Pname = "sandwich", price = 150 });
            
            mygrid.ItemsSource = lst;
        }

        private void Add_Product(object sender, RoutedEventArgs e)
        {
            lst.Add(new product { Pname = t1.Text, price = Convert.ToInt32( t2.Text)});
        }

        private void Remove_Product(object sender, RoutedEventArgs e)
        {
            product? temp = (mygrid.SelectedItem as product);

            lst.Remove(temp);


        }
    }
}