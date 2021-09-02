Imports System.Windows

Namespace E4589
	Partial Public Class Resources
		Inherits ResourceDictionary

'INSTANT VB NOTE: The field instance was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private Shared instance_Conflict As Resources
		Public Shared ReadOnly Property Instance() As Resources
			Get
				Return instance_Conflict
			End Get
		End Property

		Shared Sub New()
			instance_Conflict = New Resources()
		End Sub

		Public Sub New()
			InitializeComponent()
		End Sub
	End Class
End Namespace
