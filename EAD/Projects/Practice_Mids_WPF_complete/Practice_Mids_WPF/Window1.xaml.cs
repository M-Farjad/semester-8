using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Practice_Mids_WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Person _person;
        private DataHandler _handler;
        internal Window1(Person person, DataHandler handler)
        {
            InitializeComponent();
            _handler = handler;
            _person = person;
            NameTextBox.Text = _person.Name;
            AgeTextBox.Text = _person.Age.ToString();
        }
        private void Click_On_Save_Button(object sender, RoutedEventArgs e)
        {
            _person = _handler.EditPerson(NameTextBox.Text, AgeTextBox.Text,_person );
            //t1.Text = "";
            //t2.Text = ""
            if (!string.IsNullOrWhiteSpace(NameTextBox.Text) && int.TryParse(AgeTextBox.Text, out int age))
            {
                _person.Name = NameTextBox.Text;
                _person.Age = age;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid data.");
            }
        }
    }
}
