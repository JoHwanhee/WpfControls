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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls
{
    /// <summary>
    /// BoardListView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BoardListView : UserControl
    {
        public Headers Headers { get; } = new Headers();

        public List<TodoItem> items { get; } = new List<TodoItem>();

        public BoardListView()
        {
            InitializeComponent();
            DataContext = this;

            
        }
    }

    public enum CheckType
    {
        None,
        Half,
        Check,
    }

    public class Headers
    {
        public bool IsAllSelected = false;

    }

    public class TodoItem
    {
        public bool Select { get; set; }
        public int Completion { get; set; }
    }

    
}
