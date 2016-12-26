using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace RemindRestingTime
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const int REST_SECOND = 60;
        private const int WORK_SECOND = 3600;

        private int _count;
        private int _countDownNumber;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _count = 0;

            var timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += TimerOnTick;
            timer.Start();

            Hide();
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (Visibility == Visibility.Hidden)
            {
                _count++;
                if (_count == WORK_SECOND)
                {
                    Reset();
                    Show();
                    WindowState = WindowState.Maximized;
                }
            }
            else
            {
                CountDownNumber--;
                if (CountDownNumber == 0)
                {
                    Hide();
                    Reset();
                }
            }
        }

        public int CountDownNumber
        {
            get { return _countDownNumber; }
            set
            {
                _countDownNumber = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Reset()
        {
            CountDownNumber = REST_SECOND;
            _count = 0;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}