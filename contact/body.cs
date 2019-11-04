using Connection;
using controllibrary;
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
    class body : uiapp
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
        long userid;
        public override FrameworkElement element => grid;
        client client = default;
        public override void create(long userid)
        {
            client = new client(this.userid);
            this.userid = userid;
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