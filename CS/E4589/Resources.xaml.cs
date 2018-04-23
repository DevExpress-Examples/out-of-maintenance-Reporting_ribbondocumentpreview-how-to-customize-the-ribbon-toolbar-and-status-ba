using System.Windows;

namespace E4589 {
    public partial class Resources : ResourceDictionary {
        static Resources instance;
        public static Resources Instance { get { return instance; } }
        
        static Resources(){
            instance = new Resources();
        }
        
        public Resources() {
            InitializeComponent();
        }
    }
}
