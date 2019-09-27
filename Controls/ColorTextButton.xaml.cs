using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Messenger.Resources.Control
{
    /// <summary>
    /// ColorTextButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorTextButton : UserControl
    {
        #region dependency property
        public static readonly DependencyProperty NormalColorProperty = DependencyProperty.Register("NormalColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(NormalColorChangedCallback)));
        public static readonly DependencyProperty PressedColorProperty = DependencyProperty.Register("PressedColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(PressedColorChangedCallback)));
        public static readonly DependencyProperty OverColorProperty = DependencyProperty.Register("OverColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(OverColorChangedCallback)));
        public static readonly DependencyProperty DisabledColorProperty = DependencyProperty.Register("DisabledColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(DisabledColorChangedCallback)));
        
        public static readonly DependencyProperty OverTextColorProperty = DependencyProperty.Register("OverTextColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(OverTextColorChangedCallback)));
        public static readonly DependencyProperty NormalTextColorProperty = DependencyProperty.Register("NormalTextColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(NormalTextColorChangedCallback)));
        public static readonly DependencyProperty PressedTextColorProperty = DependencyProperty.Register("PressedTextColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(PressedTextColorChangedCallback)));
        private static readonly DependencyProperty DisabledTextColorProperty = DependencyProperty.Register("DisabledTextColor", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(DisabledTextColorChangedCallback)));

        public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register("TextContent", typeof(string), typeof(ColorTextButton), new PropertyMetadata(null, new PropertyChangedCallback(TextContentPropertyChangedCallBack)));

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
"Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorTextButton));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        private void OnClick()
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

        #endregion

        #region callback
        private static void NormalColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnNormalColorChanged(e.OldValue, e.NewValue);
            }
        }

        private static void TextContentPropertyChangedCallBack(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnTextContentChanged(e.OldValue, e.NewValue);
            }
        }

        private static void PressedColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnPressedColorChanged(e.OldValue, e.NewValue);
            }
        }

        private static void OverColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnOverColorChanged(e.OldValue, e.NewValue);
            }
        }

        private static void DisabledColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnDisabledColorChanged(e.OldValue, e.NewValue);
            }
        }

        private static void DisabledTextColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnDisabledTextColorChanged(e.OldValue, e.NewValue);
            }
        }


        private static void PressedTextColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnPressedTextColorChanged(e.OldValue, e.NewValue);
            }
        }

        private static void OverTextColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnOverTextColorChanged(e.OldValue, e.NewValue);
            }
        }

        private static void NormalTextColorChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is ColorTextButton)
            {
                ColorTextButton imgbtn = sender as ColorTextButton;
                imgbtn.OnNormalTextColorChanged(e.OldValue, e.NewValue);
            }
        }

        #endregion

        #region public property
      
        public string NormalColor
        {
            get
            {
                return this.GetValue(NormalColorProperty) as string;
            }
            set
            {
                this.SetValue(NormalColorProperty, value);
            }
        }

        public string PressedColor
        {
            get
            {
                return this.GetValue(PressedColorProperty) as string;
            }
            set
            {
                this.SetValue(PressedColorProperty, value);
            }
        }

        public string OverColor
        {
            get
            {
                return this.GetValue(OverColorProperty) as string;
            }
            set
            {
                this.SetValue(OverColorProperty, value);
            }
        }

        public string DisabledColor
        {
            get
            {
                return this.GetValue(DisabledColorProperty) as string;
            }
            set
            {
                this.SetValue(DisabledColorProperty, value);
            }
        }

        public string NormalTextColor
        {
            get
            {
                return (string)this.GetValue(NormalTextColorProperty);
            }
            set
            {
                this.SetValue(NormalTextColorProperty, value);
            }
        }

        public string PressedTextColor
        {
            get
            {
                return (string)this.GetValue(PressedTextColorProperty);
            }
            set
            {
                this.SetValue(PressedTextColorProperty, value);
            }
        }

        public string OverTextColor
        {
            get
            {
                return (string)this.GetValue(OverTextColorProperty);
            }
            set
            {
                this.SetValue(OverTextColorProperty, value);
            }
        }

        public string TextContent
        {
            get
            {
                return (string)this.GetValue(TextContentProperty);
            }
            set
            {
                this.SetValue(TextContentProperty, value);
            }
        }

        public string DisabledTextColor
        {
            get
            {
                return (string)this.GetValue(DisabledTextColorProperty);
            }
            set
            {
                this.SetValue(DisabledTextColorProperty, value);
            }
        }

        #endregion

        #region protected method
        protected void OnNormalColorChanged(object oldValue, object newValue)
        {
            this.NormalColor = newValue as string;
        }
        private void OnDisabledTextColorChanged(object oldValue, object newValue)
        {
            this.DisabledTextColor = newValue as string;
        }

        protected void OnPressedColorChanged(object oldValue, object newValue)
        {
            this.PressedColor = newValue as string;
        }

        protected void OnOverColorChanged(object oldValue, object newValue)
        {
            this.OverColor = newValue as string;
        }

        protected void OnDisabledColorChanged(object oldValue, object newValue)
        {
            this.DisabledColor = newValue as string;
        }
        
        private void OnOverTextColorChanged(object oldValue, object newValue)
        {
            OverTextColor = (string)newValue;
        }

        private void OnPressedTextColorChanged(object oldValue, object newValue)
        {
            PressedTextColor = (string)newValue;
        }

        private void OnNormalTextColorChanged(object oldValue, object newValue)
        {
            NormalTextColor = (string)newValue;
        }

        private void OnTextContentChanged(object oldValue, object newValue)
        {
            TextContent = (string)newValue;
        }

        #endregion

        public ColorTextButton()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += ColorTextButton_Loaded;
            Btn.FocusVisualStyle = null;
            Btn.PreviewMouseLeftButtonUp += Btn_PreviewMouseLeftButtonUp;
            Btn.PreviewKeyUp += Btn_PreviewKeyUp;
            Btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ColorTextButton_Loaded(object sender, RoutedEventArgs e)
        {
            Btn.Focus();
        }

        private void Btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OnClick();
        }

        private void Btn_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                OnClick();
                e.Handled = true;
            }
        }
    }
}
