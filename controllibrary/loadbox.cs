using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace controllibrary
{
    public class loadbox : uibase
    {
        Grid grid = new Grid();
        Border cover = new Border();
        Border icon = new Border();
        UIElement contentf = default;
        public UIElement content
        {
            get
            {
                return contentf;
            }
            set
            {
                if (contentf != null)
                    grid.Children.Remove(contentf);
                contentf = value;
                if (contentf != null)
                {
                    grid.Children.Add(contentf);
                    Panel.SetZIndex(cover, 1);
                    Panel.SetZIndex(contentf, 0);
                }
            }
        }
        public void dialog(uibase uibase)
        {
            uibase.close_e += Uibase_close_e;
            cover.Child = uibase.element;
            cover.Visibility = Visibility.Visible;
        }

        private void Uibase_close_e(uibase obj)
        {
            closedialog();
        }
        void closedialog()
        {
            cover.Child = null;
            cover.Visibility = Visibility.Collapsed;
        }
        void opendialog(UIElement element)
        {
            cover.Child = element;
            cover.Visibility = Visibility.Visible;
        }
        public override FrameworkElement element => grid;
        static loadbox ds;
        public loadbox()
        {
            if (ds == null)
                ds = this;
            icon.Width = icon.Height = 64;
            icon.HorizontalAlignment = HorizontalAlignment.Center;
            icon.VerticalAlignment = VerticalAlignment.Center;
            icon.Background = CreateBrushFromBitmap(resource.loading);
            icon.RenderTransform = transform;
            cover.Child = icon;
            grid.Children.Add(cover);
            cover.Background = new SolidColorBrush() { Color = Colors.White, Opacity = .5 };
            release();
        }

        RotateTransform transform = new RotateTransform()
        {
            CenterX = 32,
            CenterY = 32,
        };
        public static void maindialog(uibase uibase)
        {
            ds.dialog(uibase);
        }

        public static void mainwaiting()
        {
            ds?.waiting();
        }
        public void waiting()
        {
            run(() =>
            {
                DoubleAnimation animation = new DoubleAnimation()
                {
                    From = 0,
                    To = 360,
                    Duration = TimeSpan.FromSeconds(3),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                transform.BeginAnimation(RotateTransform.AngleProperty, animation);
                opendialog(icon);
            });
        }
        public static void mainrelease()
        {
            ds?.release();
        }
        public void release()
        {
            run(() =>
            {
                closedialog();
                transform.BeginAnimation(RotateTransform.AngleProperty, null);
            });
        }
        public static System.Windows.Media.Brush CreateBrushFromBitmap(Bitmap bitmap)
        {
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return new ImageBrush(bitmapSource);
        }
    }
}