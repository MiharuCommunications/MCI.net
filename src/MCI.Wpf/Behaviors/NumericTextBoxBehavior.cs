using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Miharu.Wpf.Behaviors
{
    public class NumericTextBoxBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty IntegerOnlyProperty = DependencyProperty.Register(
            "IntegerOnly",
            typeof(bool),
            typeof(NumericTextBoxBehavior),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIntegerOnlyChanged));

        public static readonly DependencyProperty PositiveOnlyProperty = DependencyProperty.Register(
            "PositiveOnly",
            typeof(bool),
            typeof(NumericTextBoxBehavior),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPositiveOnlyChanged));


        private static void OnPositiveOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NumericTextBoxBehavior)d).OnPositiveOnlyChanged((bool)e.OldValue, (bool)e.NewValue);
        }



        private static void OnIntegerOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((NumericTextBoxBehavior)d).OnIntegerOnlyChanged((bool)e.OldValue, (bool)e.NewValue);
        }




        [Bindable(true)]
        public bool IntegerOnly
        {
            get
            {
                return (bool)this.GetValue(IntegerOnlyProperty);
            }

            set
            {
                this.SetValue(IntegerOnlyProperty, value);
            }
        }


        [Bindable(true)]
        public bool PositiveOnly
        {
            get
            {
                return (bool)this.GetValue(PositiveOnlyProperty);
            }

            set
            {
                this.SetValue(PositiveOnlyProperty, value);
            }
        }

        protected virtual void OnPositiveOnlyChanged(bool oldValue, bool newValue)
        {

        }


        protected virtual void OnIntegerOnlyChanged(bool oldValue, bool newValue)
        {

        }



        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
            this.AssociatedObject.TextChanged += AssociatedObject_TextChanged;

            this.AssociatedObject.PreviewGotKeyboardFocus += AssociatedObject_PreviewGotKeyboardFocus;

            DataObject.AddPastingHandler(this.AssociatedObject, TextBoxPastingEventHandler);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
            this.AssociatedObject.TextChanged -= AssociatedObject_TextChanged;

            DataObject.RemovePastingHandler(this.AssociatedObject, TextBoxPastingEventHandler);
        }

        private void AssociatedObject_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.AssociatedObject.SelectAll();
        }


        private void AssociatedObject_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.None && e.Key.IsNumericKey())
            {
                //数字キー
                e.Handled = false;
            }
            else if ((Key.Left <= e.Key && e.Key <= Key.Down) || Key.Delete == e.Key || Key.Back == e.Key || Key.Tab == e.Key)
            {
                //方向、Del、BackSpace、Tabキー
                e.Handled = false;
            }
            else
            {
                //それ以外は受け付けない
                e.Handled = true;
            }
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "0";
                textBox.SelectAll();
            }
        }

        private static void TextBoxPastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            // クリップボード経由の貼り付けチェック
            var textBox = sender as TextBox;
            var clipboard = e.DataObject.GetData(typeof(string)) as string;

            int clipval;
            if ((int.TryParse(clipboard, out clipval) == false) || (clipval < 0))
            {
                clipboard = null;
            }

            if (textBox != null && !string.IsNullOrEmpty(clipboard))
            {
                textBox.Text = clipboard;
            }

            e.CancelCommand();
            e.Handled = true;
        }
    }
}
