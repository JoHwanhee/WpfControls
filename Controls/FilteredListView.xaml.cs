using Controls;
using Messenger.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Messenger.Resources.Control
{
    /// <summary>
    /// FilteredListView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FilteredListView : UserControl, INotifyPropertyChanged
    {
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region DependencyProperty
        public static readonly DependencyProperty AvailableItemsSourceProperty = DependencyProperty.Register("AvailableItemsSource",
            typeof(IEnumerable),
            typeof(FilteredListView),
            new PropertyMetadata(null, new PropertyChangedCallback(AvailableItemsSourceChangedCallback)));

        public static readonly DependencyProperty SelectedItemsListProperty = DependencyProperty.Register("SelectedItemsList",
            typeof(List<object>),
            typeof(FilteredListView),
            new PropertyMetadata(null, null));

        private static void AvailableItemsSourceChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender != null && sender is FilteredListView)
            {
                FilteredListView filteredListView = sender as FilteredListView;
                filteredListView.OnAvailableItemsSourceChanged(e.OldValue, e.NewValue);
            }
        }

        private void OnAvailableItemsSourceChanged(object oldValue, object newValue)
        {
            AvailableItemsSource = (IEnumerable)newValue;
        }

        public IEnumerable AvailableItemsSource
        {
            get { return (IEnumerable)this.GetValue(AvailableItemsSourceProperty); }
            set { this.SetValue(AvailableItemsSourceProperty, value); }
        }

        public List<object> SelectedItemsList
        {
            get { return (List<object>)this.GetValue(SelectedItemsListProperty); }
            set { this.SetValue(SelectedItemsListProperty, value); }
        }
        #endregion

        private int PrevIndex { get; set; }
        private int Index { get; set; }
        public BindingList<object> Results { get; } = new BindingList<object>();
        public Visibility ScrollVisibility { get; } = Visibility.Collapsed;

        public object SlectedEmelent = new object();

        private List<Button> lastButtons = new List<Button>();
        public ICommand CheckboxMouseEnterCommand { get; private set; }
        public ICommand CheckboxMouseLeaveCommand { get; private set; }
        public double subjectElementHeight = 30;
        public FilteredListView()
        {
            InitializeComponent();
            SelectedItemsList = new List<object>();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                case Key.Back:
                    lastButtons = textBox.GetButtons();
                    break;
            }
        }

        private ContentPresenter GetCurrentBlock()
        {
            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                UIElement uiElement = (UIElement)itemsControl.ItemContainerGenerator.ContainerFromIndex(i);

                var tb = uiElement as ContentPresenter;
                if(tb.DataContext == Results[Index])
                {
                    return tb;
                }
            }

            return null;
        }

        public Point? FindFirstTextBlock()
        {
            Point? resPoint = null;

            try
            {
                var current = GetCurrentBlock();
                if(current != null)
                {
                    resPoint = current.TransformToAncestor(border)
                              .Transform(new Point(0, 0));
                }
                return resPoint;
            }
            catch (Exception e)
            {
            }

            return resPoint;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            PrevIndex = Index;

            switch (e.Key)
            {
                case Key.Up:
                    DecreaseIndex();
                    ChangeColorSelectedItem();
                    ChangeColorPreviousSelectedItem();
                    var tb = FindFirstTextBlock();
                    if (tb != null)
                    {
                        if(tb.Value.Y < 0)
                        {
                            scollViewer.ScrollToVerticalOffset(scollViewer.VerticalOffset - subjectElementHeight);
                        }

                    }

                    break;
                case Key.Down:
                    IncreaseIndex();
                    ChangeColorSelectedItem();
                    ChangeColorPreviousSelectedItem();
                    
                    if (scollViewer.ActualHeight < Index * subjectElementHeight)
                    {
                        scollViewer.ScrollToVerticalOffset(scollViewer.VerticalOffset + subjectElementHeight);
                    }
                    break;
                case Key.Enter:
                    if(Index < 0)
                    {
                        break;
                    }
                    if(Index >= Results.Count)
                    {
                        break;
                    }

                    AddSelectedItem(Results[Index]);
                    DecreaseIndex();

                    var para = textBox.GetFirstParagraph();
                    if (para != null)
                    {
                        List<Run> targets = new List<Run>();
                        foreach (var item in para.Inlines)
                        {
                            if (item is Run run)
                            {
                                targets.Add(run);
                            }
                        }

                        foreach (var item in targets)
                        {
                            para.Inlines.Remove(item);
                        }
                    }

                    InitIndex();
                    e.Handled = true;
                    break;
                case Key.Delete:
                case Key.Back:
                    var currentButtons = textBox.GetButtons();
                    List<Button> exceptedButtons = new List<Button>();
                    foreach (var button in lastButtons)
                    {
                        if(!currentButtons.Exists(x=>x.DataContext == button.DataContext))
                        {
                            exceptedButtons.Add(button);
                        }
                    }

                    List<object> removeTargets = new List<object>();
                    if(SelectedItemsList!= null)
                    {
                        foreach (var item in SelectedItemsList)
                        {
                            if(exceptedButtons.Exists(x => x.DataContext == item))
                            {
                                removeTargets.Add(item);
                            }
                        }
                    }

                    foreach (var item in removeTargets)
                    {
                        SelectedItemsList.Remove(item);
                        User user = item as User;
                        user.IsBlink = false;
                        Results.Add(user);
                    }

                    OnPropertyChanged(nameof(Results));
                   break;
                default:
                    var text = textBox.GetText();
                    if (string.IsNullOrWhiteSpace(Regex.Replace(text, " ", "")))
                    {
                        CollapsePopup();
                    }
                    else
                    {
                        SearchProcess(text);
                    }
                    break;
            }
            e.Handled = true;
        }

        private void SearchProcess(string text)
        {
            InitIndex();
            Results.Clear();
            Search(text);
            OnPropertyChanged(nameof(Results));
        }

        private void InitIndex()
        {
            Index = -1;
            PrevIndex = -1;
        }

        private void DecreaseIndex()
        {
            Index -= 1;
            CheckIndex();
        }

        private void IncreaseIndex()
        {
            Index += 1;
            CheckIndex();
        }

        private void CheckIndex()
        {
            if (Index < 0)
            {
                Index = -1;
            }

            if (Index > Results.Count -1)
            {
                Index = Results.Count -1;
            }

            if (PrevIndex > Results.Count -1)
            {
                PrevIndex = Results.Count - 1;
            }
        }

        private void ChangeColorSelectedItem()
        {
            if (Results.Count > 0 && Index != -1 && Results.Count > Index)
            {
                //var tb = resultStack.Children[Index] as TextBlock;
                //tb.Tag = true;

                var user = Results[Index] as User;
                user.IsBlink = true;
            }
        }

        private void ChangeColorPreviousSelectedItem()
        {
            if (Results.Count > 0 && PrevIndex != -1 && Results.Count > PrevIndex)
            {
                //var prevTb = resultStack.Children[PrevIndex] as TextBlock;
                //prevTb.Tag = false;

                var user = Results[PrevIndex] as User;
                user.IsBlink = false;
            }
        }

        private void Search(string query)
        {
            if (AvailableItemsSource == null)
            {
                return;
            }

            query = query.Replace("\r", "");
            query = query.Replace("\n", "").Trim();

            var res = Search(AvailableItemsSource, query);
            foreach (var item in res)
            {
                AddItem(item);
            }
        }

        private IEnumerable<object> Search(IEnumerable data, string word)
        {
            char[] charArray = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ',
                           'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ','ㅋ','ㅌ', 'ㅍ', 'ㅎ' };
            string[] singleWords = { "가", "까", "나", "다", "따", "라", "마", "바", "빠", "사", "싸",
                           "아", "자", "짜", "차","카","타", "파", "하" };
            int[] chrint = {44032,44620,45208,45796,46384,46972,47560,48148,48736,49324,49912,
                               50500,51088,51676,52264,52852,53440,54028,54616,55204};

            List<object> list = new List<object>();
            foreach (var d in data)
            {
                list.Add(d);
            }

            string pattern = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] >= 'ㄱ' && word[i] < '가')
                {
                    for (int j = 0; j < charArray.Length; j++)
                    {
                        if (word[i] == charArray[j])
                        {
                            pattern += "[" + singleWords[j] + "-" + (char)(chrint[j + 1] - 1) + "]";
                        }
                    }
                }
                else
                {
                    pattern += word[i];
                }
            }

            pattern = pattern.Replace("\\", "\\\\");
            pattern = pattern.Replace("(", "\\(");
            pattern = pattern.Replace(")", "\\)");

            return list.Where(e => Regex.IsMatch(e.ToString().ToLower(), pattern.ToLower()));
        }

        private void AddItem(object obj)
        {
            if (ContainsItem(SelectedItemsList, obj))
            {
                return;
            }
            
            Results.Add(obj);
            OnPropertyChanged(nameof(Results));
        }

        private void AddSelectedItem(object obj)
        {
            if (obj == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(obj.ToString()))
            {
                return;
            }

            if (Results.Contains(obj))
            {
                Results.Remove(obj);
            }

            //resultStack.Visibility = Visibility.Collapsed;
            foreach (var item in AvailableItemsSource)
            {
                if (item.ToString() == obj.ToString())
                {
                    if (SelectedItemsList == null)
                    {
                        SelectedItemsList = new List<object>();
                    }

                    if (SelectedItemsList.Contains(item))
                    {
                        return;
                    }

                    SelectedItemsList.Add(item);
                }
            }

            var bt = new Button();
            bt.Content = obj.ToString();
            bt.Name = obj.ToString();
            bt.DataContext = obj;

            var firstParagraph = textBox.GetFirstParagraph();
            if (firstParagraph != null)
            {
                firstParagraph.Inlines.Add(bt);
            }

            List<Inline> runs = new List<Inline>();
            foreach (var item in firstParagraph.Inlines)
            {
                if (item is Run)
                {
                    runs.Add(item);
                }
            }

            foreach (var item in runs)
            {
                firstParagraph.Inlines.Remove(item);
            }

            textBox.Focus();
            textBox.MoveCaretToEnd();
        }

        private bool ContainsItem(IEnumerable enumerable, object itemToFind)
        {
            if (enumerable == null)
            {
                return false;
            }

            foreach (var item in enumerable)
            {
                if (itemToFind.ToString() == item.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private void CollapsePopup()
        {
            //resultStack.Visibility = Visibility.Collapsed;
        }

        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            AddSelectedItem(textBlock?.DataContext);
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            var user = textBlock?.DataContext as User;

            var index = Results.IndexOf(textBlock?.DataContext);
            PrevIndex = Index;
            Index = index;
            user.IsBlink = true;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (var item in Results)
            {
                var user = item as User;
                user.IsBlink = false;
            }
        }
    }
}
