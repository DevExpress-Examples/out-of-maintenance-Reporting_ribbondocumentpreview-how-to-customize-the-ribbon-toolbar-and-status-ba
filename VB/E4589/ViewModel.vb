Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands
Imports DevExpress.Xpf.Printing

Namespace E4589
	Friend Class ViewModel
		Implements INotifyPropertyChanged

		Private data() As String
'INSTANT VB NOTE: The field createDocumentCommand was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private createDocumentCommand_Conflict As DelegateCommand(Of Object)
'INSTANT VB NOTE: The field clearDocumentCommand was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private clearDocumentCommand_Conflict As DelegateCommand(Of Object)
'INSTANT VB NOTE: The field previewModel was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private previewModel_Conflict As LinkPreviewModel

		Public ReadOnly Property PreviewModel() As IDocumentPreviewModel
			Get
				If previewModel_Conflict Is Nothing Then
					previewModel_Conflict = New LinkPreviewModel()
					previewModel_Conflict.Link = CreateLink()
				End If
				Return previewModel_Conflict

			End Get
		End Property
		Public ReadOnly Property CreateDocumentCommand() As ICommand
			Get
				If createDocumentCommand_Conflict Is Nothing Then
					createDocumentCommand_Conflict = New DelegateCommand(Of Object)(AddressOf ExecuteCreateDocumentCommand, AddressOf CanExecuteCreateDocumentCommand)
				End If
				Return createDocumentCommand_Conflict
			End Get
		End Property
		Public ReadOnly Property ClearDocumentCommand() As ICommand
			Get
				If clearDocumentCommand_Conflict Is Nothing Then
					clearDocumentCommand_Conflict = New DelegateCommand(Of Object)(AddressOf ExecuteClearDocumentCommand, AddressOf CanExecuteClearDocumentCommand)
				End If
				Return clearDocumentCommand_Conflict
			End Get
		End Property

		Private Function CanExecuteCreateDocumentCommand(ByVal parameter As Object) As Boolean
			Return True
		End Function
		Private Sub ExecuteCreateDocumentCommand(ByVal parameter As Object)
			DirectCast(PreviewModel, LinkPreviewModel).Link.CreateDocument(True)
			clearDocumentCommand_Conflict.RaiseCanExecuteChanged()
		End Sub

		Private Function CanExecuteClearDocumentCommand(ByVal parameter As Object) As Boolean
			Return Not PreviewModel.IsEmptyDocument
		End Function
		Private Sub ExecuteClearDocumentCommand(ByVal parameter As Object)
			previewModel_Conflict.Link.PrintingSystem.ClearContent()
			clearDocumentCommand_Conflict.RaiseCanExecuteChanged()
		End Sub

		Private Function CreateLink() As LinkBase
			data = CultureInfo.CurrentCulture.DateTimeFormat.DayNames
			Dim link = New SimpleLink()
			link.DetailTemplate = DirectCast(Resources.Instance("dayNameTemplate"), DataTemplate)
			link.DetailCount = data.Length
			AddHandler link.CreateDetail, AddressOf link_CreateDetail
			Return link
		End Function

		Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
			e.Data = data(e.DetailIndex)
		End Sub

		#Region "INotifyPropertyChanged"

		Public Event PropertyChanged As PropertyChangedEventHandler

		Private Sub RaisePropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
		#End Region
	End Class
End Namespace
