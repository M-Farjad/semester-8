using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace Practice_Mids_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataHandler handler = new DataHandler();
        public MainWindow()
        {
            InitializeComponent();
            myGrid.ItemsSource = handler.GetAllData();
            handler.AddPerson("Farjad", "22");
            handler.AddPerson("Ali", "23");
            handler.AddPerson("Ahmed", "24");
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Person temp = (myGrid.SelectedItem as Person);
            handler.RemovePerson(temp);
        }

        private void MakeEditable(object sender, RoutedEventArgs e)
        {
            Person temp = (myGrid.SelectedItem as Person);
            //t1.Text = temp.Name;
            //t2.Text = temp.Age.ToString();
            //b1.Visibility = Visibility.Hidden;
            //b2.Visibility = Visibility.Visible;
            Window1 window1 = new Window1(temp, handler);
            window1.Show();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            handler.AddPerson(t1.Text,t2.Text);
            t1.Text = "";
            t2.Text = "";
        }

        private void EditPerson(object sender, RoutedEventArgs e)
        {
            
            //temp = handler.EditPerson(t1.Text,t2.Text, temp);
            //t1.Text = "";
            //t2.Text = "";
            //b1.Visibility = Visibility.Visible;
            //b2.Visibility = Visibility.Hidden;
        }
    }

    internal class DataHandler
    {
        ObservableCollection<Person> lst = new ObservableCollection<Person>();

        public ObservableCollection<Person> GetAllData()
        {
            return lst;
        }

        public void RemovePerson(Person person)
        {
            lst.Remove(person);
        }

        public void AddPerson(string name, string age)
        {
            lst.Add(new Person { Name = name, Age = Convert.ToInt32(age) });
        }

        public Person EditPerson(string name, string age, Person person)
        {
            person.Age = Convert.ToInt32(age);
            person.Name = name;
            return person;
        }
    }
}
