using Connection;
using controllibrary;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace contact
{
    class body : uibase
    {
        DataGrid grid = new DataGrid()
        {
            AutoGenerateColumns = false,
            CanUserAddRows = false,
            FlowDirection = FlowDirection.RightToLeft,
            Margin = new Thickness(20, 0, 20, 20)
        };
        ObservableCollection<row> rows = new ObservableCollection<row>();
        usercolumn usercolumn = new usercolumn();
        mysetting mysetting = new mysetting();
        partnersetting partnersetting = new partnersetting();
        public readonly long user;

        public override FrameworkElement element => grid;
        client client = default;
        public body(long user)
        {
            client = new client(user);
            this.user = user;
            grid.Columns.Add(usercolumn);
            grid.Columns.Add(mysetting);
            grid.Columns.Add(partnersetting);
            grid.ItemsSource = rows;
            usercolumn.filter.KeyDown += userculomn_KeyDown;
            mysetting.filter.SelectionChanged += mysetting_SelectionChanged;
            partnersetting.filter.SelectionChanged += partnersetting_SelectionChanged;
        }
        private void partnersetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reset();
        }

        private void mysetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reset();
        }
        private void userculomn_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                reset();
        }
        void reset()
        {

        }
    }
}