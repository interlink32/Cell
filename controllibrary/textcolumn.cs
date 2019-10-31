using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace controllibrary
{
    public class textcolumn : DataGridTextColumn
    {
        public textcolumn(string Binding, style style, style editstyle)
        {
            this.Binding = new Binding(Binding);
            ElementStyle = style;
            EditingElementStyle = editstyle;
        }
    }
}