<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
<!-- default file list end -->
# How to bind a non-dependency property in XAML


<p><strong>Problem:</strong></p><p>I want to bind <a href="http://documentation.devexpress.com/#WPF/DevExpressXpfRichEditRichEditControl_ReadOnlytopic"><u>RichEditControl.ReadOnly Property</u></a> to a property in my view model. I am trying to use the following XAML for this purpose:<br />
</p>

```xaml
<dxre:RichEditControl ReadOnly="{Binding IsReadOnly}" />


```

<p>However, I get the following error: <strong><i>A 'Binding' cannot be set on the 'ReadOnly' property of type 'RichEditControl'. A 'Binding' can only be set on a DependencyProperty of a DependencyObject.</i></strong></p><p><br />
<strong>Solution:</strong></p><p>This issue occurs because the <strong>RichEditControl.ReadOnly</strong> property is not defined as a <a href="http://wpftutorial.net/DependencyProperties.html"><u>Dependency Property</u></a>. In simple scenarios you can solve this problem by swapping a binding target with a binding source. Here is an XAML snippet that illustrates this statement in action:<br />
</p>

```xaml
        <dxre:RichEditControl Name="richEdit" Height="200"/> <!--binding source-->
        <CheckBox Content="ReadOnly" IsChecked="{Binding ElementName=richEdit, Path=ReadOnly, Mode=OneWayToSource}" /> <!--binding target-->


```

<p>However, this simple approach is not appropriate in the case of MVVM pattern implementation (see <a href="https://www.devexpress.com/Support/Center/p/E3497">DXRichEdit for WPF: Implementing MVVM</a>) because in this scenario, a view model should play the role of the binding source.</p><p>We can implement a helper class with an <a href="http://msdn.microsoft.com/en-us/library/ms749011.aspx"><u>Attached Property</u></a> for this purpose:<br />
</p>

```cs
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


```

<p>This will allow you to specify binding in the following manner: <br />
</p>

```xaml
        <CheckBox Name="checkBox1" Content="ReadOnly" /> <!--binding source-->
        <dxre:RichEditControl local:RichEditHelper.IsReadOnly="{Binding ElementName=checkBox1, Path=IsChecked}"/> <!--binding target-->


```

<p>We use a CheckBox in this example only for illustration purposes. You can easily replace it with your view model, which is not possible without involving our helper class. Note that the sample approach can be used in Silverlight. For instance, review the <a href="https://www.devexpress.com/Support/Center/p/Q419179">Use EventToCommand with Appointment events</a> ticket.</p>

<br/>


