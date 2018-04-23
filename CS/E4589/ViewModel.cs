using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using DevExpress.Xpf.Printing;

namespace E4589 {
    class ViewModel : INotifyPropertyChanged {
        string[] data;
        DelegateCommand<object> createDocumentCommand;
        DelegateCommand<object> clearDocumentCommand;
        LinkPreviewModel previewModel;

        public IDocumentPreviewModel PreviewModel {
            get {
                if(previewModel == null) {
                    previewModel = new LinkPreviewModel();
                    previewModel.Link = CreateLink();
                }
                return previewModel;

            }
        }
        public ICommand CreateDocumentCommand {
            get {
                if(createDocumentCommand == null)
                    createDocumentCommand = new DelegateCommand<object>(ExecuteCreateDocumentCommand, CanExecuteCreateDocumentCommand);
                return createDocumentCommand;
            }
        }
        public ICommand ClearDocumentCommand {
            get {
                if(clearDocumentCommand == null)
                    clearDocumentCommand = new DelegateCommand<object>(ExecuteClearDocumentCommand, CanExecuteClearDocumentCommand);
                return clearDocumentCommand;
            }
        }

        bool CanExecuteCreateDocumentCommand(object parameter) {
            return true;
        }
        void ExecuteCreateDocumentCommand(object parameter) {
            ((LinkPreviewModel)PreviewModel).Link.CreateDocument(true);
            clearDocumentCommand.RaiseCanExecuteChanged();
        }

        bool CanExecuteClearDocumentCommand(object parameter) {
            return !PreviewModel.IsEmptyDocument;
        }
        void ExecuteClearDocumentCommand(object parameter) {
            previewModel.Link.PrintingSystem.ClearContent();
            clearDocumentCommand.RaiseCanExecuteChanged();
        }

        LinkBase CreateLink() {
            data = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
            var link = new SimpleLink();
            link.DetailTemplate = (DataTemplate)Resources.Instance["dayNameTemplate"];
            link.DetailCount = data.Length;
            link.CreateDetail += link_CreateDetail;
            return link;
        }

        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            e.Data = data[e.DetailIndex];
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void RaisePropertyChanged(string propertyName) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
