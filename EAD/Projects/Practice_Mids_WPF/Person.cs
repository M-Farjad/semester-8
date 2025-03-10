using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Mids_WPF
{
    internal class Person: INotifyPropertyChanged
    {
        private string name;
        private int age;
        
        public string Name {
            get { return name; }
            set { name = value;
                Notify("Name");
            }
        }

        public int Age
        {
            get { return age; } 
            set{
                age = value;
                Notify("Age");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify(string propertyChanged) { 
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyChanged));
            }
        }

    }
}
