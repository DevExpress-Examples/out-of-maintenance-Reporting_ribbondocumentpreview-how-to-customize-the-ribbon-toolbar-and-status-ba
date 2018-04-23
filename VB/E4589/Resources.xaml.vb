Imports Microsoft.VisualBasic
Imports System.Windows

Namespace E4589
	Partial Public Class Resources
		Inherits ResourceDictionary
		Private Shared instance_Renamed As Resources
		Public Shared ReadOnly Property Instance() As Resources
			Get
				Return instance_Renamed
			End Get
		End Property

		Shared Sub New()
			instance_Renamed = New Resources()
		End Sub

		Public Sub New()
			InitializeComponent()
		End Sub
	End Class
End Namespace
