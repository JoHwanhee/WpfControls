using System.Windows.Input;

namespace Controls
{
    public static class KeyEventArgsExtension
    {
        public static bool CtrlC(this KeyEventArgs e)
        {
            return e.Key == Key.C && Keyboard.Modifiers == ModifierKeys.Control;
        }

        public static bool CtrlV(this KeyEventArgs e)
        {
            return e.Key == Key.V && Keyboard.Modifiers == ModifierKeys.Control;
        }

        public static bool CtrlE(this KeyEventArgs e)
        {
            return e.Key == Key.E && Keyboard.Modifiers == ModifierKeys.Control;
        }

        public static bool CtrlY(this KeyEventArgs e)
        {
            return e.Key == Key.Y && Keyboard.Modifiers == ModifierKeys.Control;
        }

        public static bool CtrlZ(this KeyEventArgs e)
        {
            return e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control;
        }

        public static bool F2(this KeyEventArgs e)
        {
            return (e.Key == Key.F2);
        }

        public static bool CtrlShiftZ(this KeyEventArgs e)
        {
            return e.Key == Key.Z && Keyboard.Modifiers == (ModifierKeys.Shift | ModifierKeys.Control);
        }

        public static bool ESC(this KeyEventArgs e)
        {
            return e.Key == Key.Escape;
        }

        public static bool Enter(this KeyEventArgs e)
        {
            return e.Key == Key.Enter;
        }

        public static bool LeftKey(this KeyEventArgs e)
        {
            return e.Key == Key.Left;
        }

        public static bool RightKey(this KeyEventArgs e)
        {
            return e.Key == Key.Right;
        }

        public static bool UpKey(this KeyEventArgs e)
        {
            return (e.Key == Key.Up);
        }

        public static bool DownKey(this KeyEventArgs e)
        {
            return (e.Key == Key.Down);
        }

        public static bool IsArrows(this KeyEventArgs e)
        {
            return LeftKey(e) || RightKey(e) || UpKey(e) || DownKey(e);
        }

        public static bool Plus(this KeyEventArgs e)
        {
            return e.Key == Key.OemPlus || e.Key == Key.Add;
        }

        public static bool Minus(this KeyEventArgs e)
        {
            return e.Key == Key.OemMinus || e.Key == Key.Subtract;
        }

        public static bool Insert(this KeyEventArgs e)
        {
            return e.Key == Key.Insert;
        }

        public static bool Delete(this KeyEventArgs e)
        {
            return (e.Key == Key.Delete);
        }

    }
}
