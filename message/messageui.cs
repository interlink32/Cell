using controllibrary;
using Dna;
using Dna.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace message
{
    class messageui : ListBoxItem, Is_entity
    {
        Border border = new Border();
        TextBlock block = new TextBlock();
        public messageui()
        {
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            Content = border;
            border.Child = block;
            border.BorderThickness = new Thickness(2);
            border.Padding = new Thickness(10);
            border.MinWidth = 180;
            block.TextWrapping = TextWrapping.Wrap;
            block.MaxWidth = 300;
        }
        e_messagestate state
        {
            set
            {
                switch (value)
                {
                    case e_messagestate.send:
                        {
                            border.Background = Brushes.LightBlue;
                            border.HorizontalAlignment = HorizontalAlignment.Left;
                            border.CornerRadius = new CornerRadius(0, 10, 10, 0);
                        }
                        break;
                    case e_messagestate.receive:
                        {
                            border.Background = Brushes.LightGreen;
                            border.HorizontalAlignment = HorizontalAlignment.Right;
                            border.CornerRadius = new CornerRadius(10, 0, 0, 10);
                        }
                        break;
                }
            }
        }
        s_message message = new s_message();
        long Is_entity.id { get => message.id; set => message.id = value; }

        void Is_entity.update(long owner, s_entity entity)
        {
            message.update(owner, entity);
            state = message.messagestate;
            block.Text = message.text;
        }
    }
}