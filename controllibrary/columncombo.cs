using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace controllibrary
{
    public class columncombo : DataGridComboBoxColumn
    {
        public columncombo(string binding, comboadapter comboadapter, style style, style editstyle)
        {
            Binding b = new Binding(binding);
            b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            b.Mode = BindingMode.TwoWay;
            b.Converter = comboadapter;
            SelectedItemBinding = b;
            ItemsSource = comboadapter.itemsource;
            ElementStyle = style;
            EditingElementStyle = editstyle;
        }
    }
}