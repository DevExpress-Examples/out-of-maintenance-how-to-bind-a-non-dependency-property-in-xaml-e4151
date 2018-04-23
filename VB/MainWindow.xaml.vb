Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports DevExpress.Xpf.RichEdit

Namespace RichEditDependencyPropertyWrapperWpf
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
		End Sub
	End Class

	Public Class RichEditHelper
		Public Shared ReadOnly IsReadOnlyProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsReadOnly", GetType(Boolean), GetType(RichEditHelper), New FrameworkPropertyMetadata(False, New PropertyChangedCallback(AddressOf IsReadOnlyPropertyChanged)))

		Private Shared Sub IsReadOnlyPropertyChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim richEditControl As RichEditControl = TryCast(obj, RichEditControl)

			If richEditControl IsNot Nothing Then
				richEditControl.ReadOnly = Convert.ToBoolean(e.NewValue)
			End If
		End Sub

		Public Shared Sub SetIsReadOnly(ByVal element As UIElement, ByVal value As Boolean)
			element.SetValue(IsReadOnlyProperty, value)
		End Sub

		Public Shared Function GetIsReadOnly(ByVal element As UIElement) As Boolean
			Return CBool(element.GetValue(IsReadOnlyProperty))
		End Function
	End Class
End Namespace