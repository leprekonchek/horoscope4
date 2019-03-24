using System.Windows.Controls;
using _04_Lopukhina.Tools.DataStorage;
using _04_Lopukhina.Tools.Managers;
using _04_Lopukhina.Tools.Navigation;
using _04_Lopukhina.ViewModels;

namespace _04_Lopukhina
{
    public partial class MainWindow : IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.PersonGrid);
        }
    }
}
