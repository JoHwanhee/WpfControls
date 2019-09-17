using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Text { get; } = "hui";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var msg = "http://www.naver.com";

            Uri uri = new Uri(msg, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                Uri.TryCreate(msg, UriKind.Absolute, out uri);

            Hyperlink link = new Hyperlink()
            {
                NavigateUri = uri,
            };
            var tb = new SelectableTextBlock();
            tb.Text = msg;
            var run = new Run();
            run.Text = msg;
            link.Inlines.Add("asd");
            link.Inlines.Add(" ");
            link.Click += Hyperlink_Click;
            TextOptions.SetTextFormattingMode(link, TextFormattingMode.Ideal);
            block.Inlines.Add(link);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SelectableTextBlock_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
    }
}
