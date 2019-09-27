using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    public enum CheckboxStatus
    {
        None,
        Checked,
        Over,
        Half
    }

    /// <summary>
    /// AnyTypeCheckbox.xaml에 대한 상호 작용 논리
    /// </summary>
    [DefaultBindingProperty("CheckboxStatus2")]
    public partial class ThreeTypeCheckbox : UserControl, INotifyPropertyChanged
    {
        public CheckboxStatus cache;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isChecked = false;
        public static readonly DependencyProperty CheckboxStatusProperty = 
            DependencyProperty.Register("CheckboxStatus", 
                typeof(CheckboxStatus), 
                typeof(ThreeTypeCheckbox), 
                new PropertyMetadata(CheckboxStatus.None, new PropertyChangedCallback(CheckboxStatusPropertyChangedCallBack)));

        public static readonly DependencyProperty CheckboxStatus2Property =
      DependencyProperty.Register("CheckboxStatus2",
          typeof(CheckboxStatus),
          typeof(ThreeTypeCheckbox),
          new PropertyMetadata(CheckboxStatus.None, new PropertyChangedCallback(CheckboxStatusPropertyChangedCallBack2)));

        public static readonly DependencyProperty SamplePropertyProperty = 
            DependencyProperty.Register("SampleProperty", 
                typeof(string), typeof(ThreeTypeCheckbox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        private CheckboxStatus _status;
        public CheckboxStatus CheckboxStatus2
        {
            get { return _status; }
            set {
                _status = value;
                OnPropertyChanged("CheckboxStatus2");
            }
        }

        private static void CheckboxStatusPropertyChangedCallBack(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ThreeTypeCheckbox)
            {
                ThreeTypeCheckbox imgbtn = sender as ThreeTypeCheckbox;
                imgbtn.CheckboxStatusChanged(e.OldValue, e.NewValue);
            }
        }
        private static void CheckboxStatusPropertyChangedCallBack2(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ThreeTypeCheckbox)
            {
                ThreeTypeCheckbox imgbtn = sender as ThreeTypeCheckbox;
                imgbtn.CheckboxStatusChanged2(e.OldValue, e.NewValue);
            }
        }
        protected void CheckboxStatusChanged(object oldValue, object newValue)
        {
            this.CheckboxStatus = (CheckboxStatus)newValue;
        }
        protected void CheckboxStatusChanged2(object oldValue, object newValue)
        {
            this.CheckboxStatus2 = (CheckboxStatus)newValue;
        }

        public CheckboxStatus CheckboxStatus
        {
            get
            {
                return (CheckboxStatus)GetValue(CheckboxStatusProperty);
            }
            set
            {
                this.SetValue(CheckboxStatusProperty, value);
                OnPropertyChanged(nameof(CheckboxStatus));
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ThreeTypeCheckbox()
        {
            InitializeComponent();
            DataContext = this;
            checkBox.IsChecked = false;
        }

        private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if(CheckboxStatus == CheckboxStatus.Half)
            {
                return;
            }

            if (isChecked)
            {
                CheckboxStatus = CheckboxStatus.Checked;
            }
            else
            {
                CheckboxStatus = CheckboxStatus.Over;
            }

            OnPropertyChanged(nameof(CheckboxStatus));
        }

        private void CheckBox_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeNomalIcon();
            OnPropertyChanged(nameof(CheckboxStatus));
        }

        public void ChangeHalf()
        {
            cache = CheckboxStatus;
            CheckboxStatus = CheckboxStatus.Half;
            OnPropertyChanged(nameof(CheckboxStatus));
        }

        public void ChangeNone()
        {
            cache = CheckboxStatus;
            CheckboxStatus = CheckboxStatus.None;
            isChecked = false;
            checkBox.IsChecked = isChecked;
            OnPropertyChanged(nameof(CheckboxStatus));
        }

        private void ChangeNomalIcon()
        {
            if (CheckboxStatus == CheckboxStatus.Half)
            {
                return;
            }

            if (checkBox.IsChecked.HasValue && checkBox.IsChecked.Value)
            {
                CheckboxStatus = CheckboxStatus.Checked;
            }
            else
            {
                CheckboxStatus = CheckboxStatus.None;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if(CheckboxStatus == CheckboxStatus.Half)
            {
                isChecked = false;
            }

            if (isChecked)
            {
                CheckboxStatus = CheckboxStatus.None;
                isChecked = false;
            }
            else
            {
                CheckboxStatus = CheckboxStatus.Checked;
                isChecked = true;
            }

            checkBox.IsChecked = isChecked;
            OnPropertyChanged(nameof(CheckboxStatus));
        }
    }
}
