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

            lst.Add(new product { Pname = "burger", Price = 200});
            lst.Add(new product { Pname="biryani", Price=150});
            lst.Add(new product { Pname = "Shawarma", Price = 150 });
            lst.Add(new product { Pname = "sandwich", Price = 150 });
            
            mygrid.ItemsSource = lst;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (t1.Text == "" || t2.Text == "")
                {
                    MessageBox.Show("Please Fill Text boxes");
                }
                else
                {
                    lst.Add(new product { Pname = t1.Text, Price = Convert.ToInt32(t2.Text) });
                    t1.Text = "";
                    t2.Text = "";
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            product? temp = (mygrid.SelectedItem as product);
            lst.Remove(temp);
        }

        private void MakeProductEditable(object sender, RoutedEventArgs e)
        {
            product ?temp = (mygrid.SelectedItem as product);
            t1.Text = temp.Pname;
            t2.Text = temp.Price.ToString();
            b1.Visibility = Visibility.Hidden;
            b2.Visibility = Visibility.Visible;
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            //shoping temp = new shoping();
            //temp.Show();
            //this.Close();
            product ?temp = (mygrid.SelectedItem as product);
            temp.Pname = t1.Text;
            temp.Price = Convert.ToInt32(t2.Text);
            t1.Text = "";
            t2.Text = "";
            b1.Visibility = Visibility.Visible;
            b2.Visibility = Visibility.Hidden;
        }
    }
}