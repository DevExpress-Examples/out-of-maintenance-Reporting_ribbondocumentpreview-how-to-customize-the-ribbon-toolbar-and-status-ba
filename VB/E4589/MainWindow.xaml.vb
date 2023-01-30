Imports DevExpress.Xpf.Bars
Imports System.Windows

Namespace E4589

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf Me.MainWindow_Loaded
        End Sub

        Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim removableLink = Me.documentPreview.RibbonStatusBar.RightItemLinks.Find(Function(x) Equals(CType(x, BarItemLink).BarItemName, "zoomFactor"))
            Me.documentPreview.RibbonStatusBar.RightItemLinks.Remove(removableLink)
            Me.documentPreview.RibbonStatusBar.RightItemLinks.Add(New BarStaticItemLink() With {.BarItemName = "zoomValue"})
            Me.documentPreview.RibbonStatusBar.RightItemLinks.Insert(0, New BarSubItemLink() With {.BarItemName = "zoomMode"})
        End Sub
    End Class
End Namespace
