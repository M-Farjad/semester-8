using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatagridEditDeleteAdd
{
    internal class product : INotifyPropertyChanged
    {
        private string pname;
        public string Pname {
            get
            {
                return pname;
            }
            set
            {
                pname = value;

                Notify("Pname");
            }
        }
        private int price;
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                Notify("Price");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Notify(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
