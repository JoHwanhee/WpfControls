using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// TimeSelectPanel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TimeSelectPanel : UserControl
    {
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(TimeSelectPanel));
        public static readonly DependencyProperty FirstTimeProperty = DependencyProperty.Register("FirstTime", typeof(string), typeof(TimeSelectPanel), new PropertyMetadata(null, FirstTimeChanged));
        public static readonly DependencyProperty LastTimeProperty = DependencyProperty.Register("LastTime", typeof(string), typeof(TimeSelectPanel), new PropertyMetadata(null, LastTimeChanged));
        public static readonly DependencyProperty CanTimesProperty = DependencyProperty.Register("CanTimes", typeof(List<string>), typeof(TimeSelectPanel), new PropertyMetadata(null, CanTimesChanged));
        public static readonly DependencyProperty SelectedTimesProperty = DependencyProperty.Register("SelectedTimes", typeof(List<string>), typeof(TimeSelectPanel), new PropertyMetadata(null, SelectedTimesChanged));
        public event RoutedEventHandler Click
        {
            add { AddHandler(TimeSelectPanel.ClickEvent, value); }
            remove { RemoveHandler(TimeSelectPanel.ClickEvent, value); }
        }


        private static void SelectedTimesChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is TimeSelectPanel)
            {
                TimeSelectPanel imgbtn = sender as TimeSelectPanel;
                imgbtn.OnSelectedTimesChanged(e.OldValue, e.NewValue);
            }
        }
        private static void CanTimesChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is TimeSelectPanel)
            {
                TimeSelectPanel imgbtn = sender as TimeSelectPanel;
                imgbtn.OnCanTimesChanged(e.OldValue, e.NewValue);
            }
        }

        private static void FirstTimeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is TimeSelectPanel)
            {
                TimeSelectPanel imgbtn = sender as TimeSelectPanel;
                imgbtn.OnFristTimeChanged(e.OldValue, e.NewValue);
            }
        }

        private static void LastTimeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is TimeSelectPanel)
            {
                TimeSelectPanel imgbtn = sender as TimeSelectPanel;
                imgbtn.OnLastTimeChanged(e.OldValue, e.NewValue);
            }
        }

        protected void OnFristTimeChanged(object oldValue, object newValue)
        {
            this.FirstTime = newValue as string;
            CaluTimes();
        }

        protected void OnLastTimeChanged(object oldValue, object newValue)
        {
            this.LastTime = newValue as string;
            CaluTimes();
        }
        protected void OnCanTimesChanged(object oldValue, object newValue)
        {
            this.CanTimes = newValue as List<string>;
            CaluTimes();
        }
        protected void OnSelectedTimesChanged(object oldValue, object newValue)
        {
            this.SelectedTimes = newValue as List<string>;
        }

        public string FirstTime
        {
            get
            {
                return (string)this.GetValue(FirstTimeProperty);
            }
            set
            {
                this.SetValue(FirstTimeProperty, value);
            }
        }
        public string LastTime
        {
            get
            {
                return (string)this.GetValue(LastTimeProperty);
            }
            set
            {
                this.SetValue(LastTimeProperty, value);
            }
        }
        public List<string> CanTimes
        {
            get
            {
                return (List<string>)this.GetValue(CanTimesProperty);
            }
            set
            {
                this.SetValue(CanTimesProperty, value);
            }
        }
        public List<string> SelectedTimes
        {
            get
            {
                return (List<string>)this.GetValue(SelectedTimesProperty);
            }
            set
            {
                this.SetValue(SelectedTimesProperty, value);
            }
        }

        List<ToggleButton> buttons = new List<ToggleButton>();
        public void CaluTimes()
        {
            if (string.IsNullOrWhiteSpace(FirstTime))
                return;
            if (string.IsNullOrWhiteSpace(LastTime))
                return;
            //if (CanTimes == null)
            //    return;

            DateTime firstTime = DateTime.Parse(FirstTime);
            DateTime lastTime = DateTime.Parse(LastTime);

            var runner = firstTime;

            StackPanel1.Children.Clear();

            int i = 1;
            while (runner.Hour != lastTime.Hour)
            {
                ToggleButton toggleButton = new ToggleButton();
                var time = $"{runner.Hour:D2}:{runner.Minute:D2}-{runner.AddMinutes(30).Hour:D2}:{runner.AddMinutes(30).Minute:D2}";
                //toggleButton.IsEnabled = CanTimes.Contains(time);
                toggleButton.Width = 91;
                toggleButton.Height = 24;
                toggleButton.Margin = new Thickness(-10, 3, 0, 0);
                toggleButton.Checked += ToggleButtonOnChecked;
                toggleButton.Unchecked += ToggleButtonOnUnchecked;
                toggleButton.Content = time;
                buttons.Add(toggleButton);
                runner = runner.AddMinutes(30);
                Line newLine = null;
                if (i % 2 == 0 && i != 0)
                {
                    newLine = new Line();
                    newLine.Margin = new Thickness(0, 6, 6, 0);
                    newLine.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ababab"));
                    newLine.StrokeThickness = 0.5;
                    newLine.Height = 1;
                    newLine.X1 = 0;
                    newLine.X2 = 118;
                }

                StackPanel1.Children.Add(toggleButton);
                if (newLine != null)
                    StackPanel1.Children.Add(newLine);

                i++;
            }
        }

        private void ToggleButtonOnUnchecked(object sender, RoutedEventArgs routedEventArgs)
        {
            CheckToggleButton(sender);
        }

        private void ToggleButtonOnChecked(object sender, RoutedEventArgs routedEventArgs)
        {
            CheckToggleButton(sender);
        }

        private void CheckToggleButton(object sender)
        {
            if (SelectedTimes == null)
            {
                SelectedTimes = new List<string>();
            }
            if (sender is ToggleButton toggle)
            {
                if (toggle.IsChecked.HasValue)
                {
                    var content = toggle.Content as string;
                    if (toggle.IsChecked.Value)
                    {
                        SelectedTimes.Add(content);
                    }
                    else
                    {
                        SelectedTimes.Remove(content);
                    }
                    RaiseEvent(new RoutedEventArgs(ClickEvent, this));
                }
            }
        }

        public TimeSelectPanel()
        {
            InitializeComponent();
            FirstTime = "09:00";
            LastTime = "18:00";
            DataContext = this;
        }
    }
}
