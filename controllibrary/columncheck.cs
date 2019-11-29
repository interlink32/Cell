using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace controllibrary
{
    public class columncheck : DataGridCheckBoxColumn
    {
        public columncheck(string binding, style style, style editstyle)
        {
            Binding b = new Binding(binding);
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b.Mode = BindingMode.TwoWay;
            b.Mode = BindingMode.TwoWay;
            Binding = b;
            ElementStyle = style;
            EditingElementStyle = editstyle;
        }
    }
}