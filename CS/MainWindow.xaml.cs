using System;
using System.Windows;
using DevExpress.Xpf.RichEdit;

namespace RichEditDependencyPropertyWrapperWpf {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
    }

    public class RichEditHelper {
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.RegisterAttached("IsReadOnly",
            typeof(bool), typeof(RichEditHelper), 
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(IsReadOnlyPropertyChanged)));

        private static void IsReadOnlyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            RichEditControl richEditControl = obj as RichEditControl;

            if (richEditControl != null) {
                richEditControl.ReadOnly = Convert.ToBoolean(e.NewValue);
            }
        }

        public static void SetIsReadOnly(UIElement element, bool value) {
            element.SetValue(IsReadOnlyProperty, value);
        }

        public static bool GetIsReadOnly(UIElement element) {
            return (bool)element.GetValue(IsReadOnlyProperty);
        }
    }
}