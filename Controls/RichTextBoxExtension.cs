using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Messenger.Resources.Control
{
    public static class RichTextBoxExtension
    {
        public static string GetText(this RichTextBox richTextBox)
        {
            return new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
        }

        public static void MoveCaretToEnd(this RichTextBox richTextBox)
        {
            richTextBox.CaretPosition = richTextBox.CaretPosition.DocumentEnd;
        }

        public static Paragraph GetFirstParagraph(this RichTextBox richTextBox)
        {
            foreach (var item in richTextBox.Document.Blocks)
            {
                if (item is Paragraph para)
                {
                    return para as Paragraph;
                }
            }

            return null;
        }

        public static int GetUIElementCount(this RichTextBox richTextBox)
        {
            var cnt = 0;

            foreach (var blcok in richTextBox.Document.Blocks)
            {
                var para = blcok as Paragraph;
                foreach (var inline in para.Inlines)
                {
                    if (inline is InlineUIContainer)
                    {
                        cnt++;
                    }
                }
            }

            return cnt;
        }

        public static string[] GetInlineUIContainerNames(this RichTextBox richTextBox) 
        {
            string[] names = new string[richTextBox.GetUIElementCount()];
            int i = 0;
            foreach (var blcok in richTextBox.Document.Blocks)
            {
                var para = blcok as Paragraph;
                foreach (var inline in para.Inlines)
                {
                    if (inline is InlineUIContainer uiInline)
                    {
                        var uIElement = uiInline.Child as Button;
                        names[i++] = uIElement.Name;
                    }
                }
            }

            return names;
        }

        public static T GetChildOfType<T>(this DependencyObject depObj)
    where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }


        public static T FindChild<T>(this FrameworkElement obj, string name)
        {
            DependencyObject dObj = obj as DependencyObject;
            T findChild = default(T);

            if (dObj != null)
            {
                int childCount = VisualTreeHelper.GetChildrenCount(dObj);

                for (int i = 0; i < childCount; i++)
                {

                    DependencyObject childObj = VisualTreeHelper.GetChild(dObj, i);
                    FrameworkElement child = childObj as FrameworkElement;

                    if (child.GetType() == typeof(T) && child.Name == name)
                    {
                        findChild = (T)Convert.ChangeType(child, typeof(T));
                        break;
                    }

                    findChild = child.FindChild<T>(name);
                    if (findChild != null) break;
                }
            }
            return findChild;
        }


        public static List<Button> GetButtons(this RichTextBox richTextBox)
        {
            List<Button> buttons = new List<Button>();
            foreach (var blcok in richTextBox.Document.Blocks)
            {
                var para = blcok as Paragraph;
                foreach (var inline in para.Inlines)
                {
                    if (inline is InlineUIContainer uiInline)
                    {
                        var uIElement = uiInline.Child as Button;
                        buttons.Add(uIElement);
                    }
                }
            }

            return buttons;
        }

    }
}
