using _04_Lopukhina.Tools.Managers;
using _04_Lopukhina.Tools.Navigation;
using _04_Lopukhina.ViewModels;

namespace _04_Lopukhina.Views
{
    public partial class PersonGridTable : INavigatable
    {
        public PersonGridTable()
        {
            InitializeComponent();
            DataContext = new PersonGridViewModel();
            StationManager.PersonGrid = PersonTable;
        }
    }
}
