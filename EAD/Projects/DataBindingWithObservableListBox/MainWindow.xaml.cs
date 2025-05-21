using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DataBindingWithObservableListBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _selectedName;
        public ObservableCollection<string> Names { get; set; }

        public string SelectedName
        {
            get { return _selectedName; }
            set
            {
                _selectedName = value;
                OnPropertyChanged(nameof(SelectedName));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this; // Set the DataContext to this instance
            Names = new ObservableCollection<string> { "Ali", "Ahmed", "Asad" };
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Names.Add("New Name");
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedName != null)
            {
                int index = Names.IndexOf(SelectedName);
                if (index >= 0)
                {
                    Names[index] = "Updated Name";
                    OnPropertyChanged(nameof(Names)); // Notify UI about changes
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedName != null)
            {
                Names.Remove(SelectedName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
