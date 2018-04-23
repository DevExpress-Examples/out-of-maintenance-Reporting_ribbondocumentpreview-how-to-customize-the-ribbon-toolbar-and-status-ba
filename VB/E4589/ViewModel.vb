Imports Microsoft.VisualBasic
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
		Private createDocumentCommand_Renamed As DelegateCommand(Of Object)
		Private clearDocumentCommand_Renamed As DelegateCommand(Of Object)
		Private previewModel_Renamed As LinkPreviewModel

		Public ReadOnly Property PreviewModel() As IDocumentPreviewModel
			Get
				If previewModel_Renamed Is Nothing Then
					previewModel_Renamed = New LinkPreviewModel()
					previewModel_Renamed.Link = CreateLink()
				End If
				Return previewModel_Renamed

			End Get
		End Property
		Public ReadOnly Property CreateDocumentCommand() As ICommand
			Get
				If createDocumentCommand_Renamed Is Nothing Then
					createDocumentCommand_Renamed = New DelegateCommand(Of Object)(AddressOf ExecuteCreateDocumentCommand, AddressOf CanExecuteCreateDocumentCommand)
				End If
				Return createDocumentCommand_Renamed
			End Get
		End Property
		Public ReadOnly Property ClearDocumentCommand() As ICommand
			Get
				If clearDocumentCommand_Renamed Is Nothing Then
					clearDocumentCommand_Renamed = New DelegateCommand(Of Object)(AddressOf ExecuteClearDocumentCommand, AddressOf CanExecuteClearDocumentCommand)
				End If
				Return clearDocumentCommand_Renamed
			End Get
		End Property

		Private Function CanExecuteCreateDocumentCommand(ByVal parameter As Object) As Boolean
			Return True
		End Function
		Private Sub ExecuteCreateDocumentCommand(ByVal parameter As Object)
			CType(PreviewModel, LinkPreviewModel).Link.CreateDocument(True)
			clearDocumentCommand_Renamed.RaiseCanExecuteChanged()
		End Sub

		Private Function CanExecuteClearDocumentCommand(ByVal parameter As Object) As Boolean
			Return Not PreviewModel.IsEmptyDocument
		End Function
		Private Sub ExecuteClearDocumentCommand(ByVal parameter As Object)
			previewModel_Renamed.Link.PrintingSystem.ClearContent()
			clearDocumentCommand_Renamed.RaiseCanExecuteChanged()
		End Sub

		Private Function CreateLink() As LinkBase
			data = CultureInfo.CurrentCulture.DateTimeFormat.DayNames
			Dim link = New SimpleLink()
			link.DetailTemplate = CType(Resources.Instance("dayNameTemplate"), DataTemplate)
			link.DetailCount = data.Length
			AddHandler link.CreateDetail, AddressOf link_CreateDetail
			Return link
		End Function

		Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
			e.Data = data(e.DetailIndex)
		End Sub

		#Region "INotifyPropertyChanged"

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub RaisePropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
		#End Region
	End Class
End Namespace
