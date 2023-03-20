using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class KeyPoint : INotifyPropertyChanged
    {   
        public int id { get; set; }

        public string name { get; set; }

        private bool _visited;
        public bool visited
        {
            get { return _visited; }
            set
            {
                _visited = value;
                OnPropertyChanged("visited");
            }
        }

        public int tourId { get; set; }

        public KeyPoint(string name, bool visited)
        {
            this.name = name;
            this.visited = visited;
        }
        public KeyPoint() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
