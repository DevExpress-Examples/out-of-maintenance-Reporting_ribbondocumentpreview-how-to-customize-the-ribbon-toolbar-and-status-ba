Imports System.Windows

Namespace E4589

    Public Partial Class Resources
        Inherits ResourceDictionary

        Private Shared instanceField As Resources

        Public Shared ReadOnly Property Instance As Resources
            Get
                Return instanceField
            End Get
        End Property

        Shared Sub New()
            instanceField = New Resources()
        End Sub

        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
