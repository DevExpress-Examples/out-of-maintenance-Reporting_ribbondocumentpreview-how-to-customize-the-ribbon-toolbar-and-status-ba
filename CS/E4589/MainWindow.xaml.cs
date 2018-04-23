using DevExpress.Xpf.Bars;
using System.Windows;

namespace E4589 {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            var removableLink = this.documentPreview.RibbonStatusBar.RightItemLinks.Find(x => ((BarItemLink)x).BarItemName == "zoomFactor");
            this.documentPreview.RibbonStatusBar.RightItemLinks.Remove(removableLink);

            this.documentPreview.RibbonStatusBar.RightItemLinks.Add(new BarStaticItemLink() {
                BarItemName = "zoomValue"
            });
            this.documentPreview.RibbonStatusBar.RightItemLinks.Insert(0, new BarSubItemLink() {
                BarItemName = "zoomMode"
            });
        }
    }
}
