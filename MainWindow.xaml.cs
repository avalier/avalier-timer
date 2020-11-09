using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Avalier.Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly System.Timers.Timer _timer;
        private DateTimeOffset _startedAt;
        private bool _isStarted = false;
        
        public MainWindow()
        {
            InitializeComponent();

            this.Topmost = true;
            
            this.MouseDown += (sender, args) =>
            {
                if (args.ChangedButton == MouseButton.Left) this.DragMove();
            };

            this.MouseRightButtonUp += (sender, args) =>
            {
                _startedAt = DateTimeOffset.Now;
                this.TextBlock.Text = "00:00:00";
            };

            this.MouseDoubleClick += (sender, args) =>
            {
                if (_isStarted)
                {
                    this.Stop();
                }
                else
                {
                    this.Start();
                }
            };
            
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (sender, args) =>
            {
                var elapsed = DateTimeOffset.Now.Subtract(_startedAt);
                var text = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}";
                this.Dispatcher.Invoke(() =>
                {
                    this.TextBlock.Text = text;
                });
            };
            this.Start();
        }

        protected void Start()
        {
            this.TextBlock.Text = "00:00:00";
            _startedAt = DateTimeOffset.Now;
            _timer.Start();
            _isStarted = true;
        }

        protected void Stop()
        {
            this._timer.Stop();
            _isStarted = false;
        }
    }
}