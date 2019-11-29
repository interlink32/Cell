using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace localdb
{
    public class ocollection<T> : ObservableCollection<T>
    {
        public override event NotifyCollectionChangedEventHandler CollectionChanged;
        public event Action<ocollection<T>> reset_e;
        public ocollection()
        {
            base.CollectionChanged += Ocollection_CollectionChanged;
        }
        private void Ocollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(sender, e);
            reset_e?.Invoke(this);
        }
        public void reset()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            reset_e?.Invoke(this);
        }
    }
}