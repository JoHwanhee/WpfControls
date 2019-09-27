using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Controls
{
    /// <summary>
    /// ImportFilePanel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ImportFilePanel : UserControl
    {
        List<FileInfo> Files = new List<FileInfo>();
        public ImportFilePanel()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void RedrawFileList()
        {
            fileListPanel.Children.Clear();
            foreach (var file in Files)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.VerticalAlignment = VerticalAlignment.Center;

                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("C:\\Users\\JoHwanHui\\source\\repos\\WpfApp4\\WpfApp4\\bin\\Debug\\img.png");
                bi3.EndInit();
                myImage3.Width = 24;
                myImage3.Height = 24;
                myImage3.Stretch = Stretch.Fill;
                myImage3.Source = bi3;

                TextBlock textBlock = new TextBlock();
                textBlock.Text = $"{file.Name} ({GetFileSize(file.Length)})";
                textBlock.Tag = file.FullName;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.Style = this.FindResource("fileTitleBlock") as Style;
                textBlock.MouseLeftButtonUp += TextBlock_MouseLeftButtonUp1;

                TextBlock textBlock2 = new TextBlock();
                textBlock2.Style = this.FindResource("fileTitleRemove") as Style;
                textBlock2.Tag = file.FullName;
                textBlock2.VerticalAlignment = VerticalAlignment.Center;
                textBlock2.PreviewMouseLeftButtonUp += TextBlock2_PreviewMouseLeftButtonUp;

                panel.Children.Add(myImage3);
                panel.Children.Add(textBlock);
                panel.Children.Add(textBlock2);

                fileListPanel.Children.Add(panel);
            }
        }

        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.#}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.#}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.#}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }

        private void TextBlock2_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            var found = Files.Find(x => x.FullName == tb.Tag as string);
            if(found != null)
            {
                Files.Remove(found);
            }
            RedrawFileList();
        }

        private void TextBlock_MouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            Process.Start(tb.Tag as string);
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.DefaultExt = "db";
            openFileDlg.Filter = "JPG (*.jpg)|*.jpg;";
            openFileDlg.Multiselect = true;
            openFileDlg.ShowDialog();
            if (openFileDlg.FileName.Length > 0)
            {
                foreach (string filename in openFileDlg.FileNames)
                {
                    var fi = new FileInfo(filename);
                    if (!Files.Exists(x=>x.FullName == fi.FullName))
                    {
                        Files.Add(fi);
                    }
                }
            }

            RedrawFileList();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFile(); 
        }
    }
}
