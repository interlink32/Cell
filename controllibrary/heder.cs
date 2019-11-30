using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace controllibrary
{
    public abstract class heder : StackPanel
    {
        public virtual event Action<bool> reset_e = default;
        public heder()
        {
            reset_e?.Invoke(true);
        }
    }
    public class lableheder : heder<Label>
    {
        Label label = new Label();
        public lableheder(string text = default) : base(text, 0)
        {
        }

        public override Label filter => label;
    }
    public abstract class heder<T> : heder where T : FrameworkElement
    {
        Label label = new Label();
        public heder(string text, int minwidth = 120)
        {
            Children.Add(filter);
            Children.Add(label);
            label.Content = text;
            MinWidth = minwidth;
        }
        public abstract T filter { get; }
    }
    public class textheder : heder<textbox>
    {
        public override event Action<bool> reset_e;
        textbox filterf = new textbox();
        public textheder(string text) : base(text)
        {
            filter.KeyUp += Filter_KeyDown;
        }
        private void Filter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            reset_e?.Invoke(e.Key == System.Windows.Input.Key.Enter);
        }
        public override textbox filter => filterf;
        public string text
        {
            get
            {
                return filter.Text;
            }
        }
    }
    public class comboheder : heder<combo>
    {
        public override event Action<bool> reset_e;
        public comboheder(string text) : base(text)
        {
            filter.SelectionChanged += Filter_SelectionChanged;
        }
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reset_e?.Invoke(true);
        }

        combo filterf = new combo();
        public override combo filter => filterf;
        public int selectedindex => filter.SelectedIndex;
    }
}