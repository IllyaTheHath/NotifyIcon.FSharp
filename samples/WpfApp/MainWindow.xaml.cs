using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon.Icon? _icon;
        private NotifyIcon.NotifyIcon? _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            btnDestroy.IsEnabled = false;
        }

        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            btnCreate.IsEnabled = false;
            btnDestroy.IsEnabled = true;

            var ir = NotifyIcon.Icon.create("yukibox.ico");
            if (ir.IsOk)
            {
                this._icon = ir.ResultValue;
                this._notifyIcon = NotifyIcon.NotifyIcon.create();
                this._notifyIcon.setIcon(this._icon);
                this._notifyIcon.setTooltip("Wpf App Tooltip");
                this._notifyIcon.onMouseLeftButtonClick += _notifyIcon_onMouseLeftButtonClick;
                this._notifyIcon.onMouseRightButtonClick += _notifyIcon_onMouseRightButtonClick;
            }
        }

        private void Button_Destroy_Click(object sender, RoutedEventArgs e)
        {
            btnCreate.IsEnabled = true;
            btnDestroy.IsEnabled = false;

            DestroyIcon();
        }

        private void _notifyIcon_onMouseRightButtonClick(object sender, object args)
        {
            Button_Destroy_Click(this, new RoutedEventArgs());
        }

        private void _notifyIcon_onMouseLeftButtonClick(object sender, object args)
        {
            MessageBox.Show("clicked");
        }

        private void DestroyIcon()
        {
            this._notifyIcon?.Dispose();
            this._icon?.Dispose();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            DestroyIcon();
        }
    }
}
