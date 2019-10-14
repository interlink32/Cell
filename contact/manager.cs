using Connection;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace contact
{
    class manager
    {
        public DataGrid grid = new DataGrid() { AutoGenerateColumns = false, CanUserAddRows = false };
        ObservableCollection<row> rows = new ObservableCollection<row>();
        usercolumn usercolumn = new usercolumn();
        mysetting mysetting = new mysetting();
        partnersetting partnersetting = new partnersetting();
        public readonly long user;
        public manager(long user)
        {
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
        async void reset()
        {
            var dv = await client.question(new q_loadallcontact()
            {
                mysettingfilter = mysetting.connectionsetting,
                partnersettingfilter = partnersetting.connectionsetting
            }) as q_loadallcontact.done;
            rows.Clear();
            foreach (var i in dv.contacts)
            {
                rows.Add(new row()
                {
                    id = i.id,
                    partner = i.partner,
                    mysetting = r_connectionsetting.get(i.mysetting),
                    partnersettin = r_connectionsetting.get(i.partnersetting)
                });
            }
        }
    }
}