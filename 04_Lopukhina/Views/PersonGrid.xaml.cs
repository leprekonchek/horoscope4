using System.Windows.Controls;
using _04_Lopukhina.Tools.Navigation;
using _04_Lopukhina.ViewModels;

namespace _04_Lopukhina.Views
{
    public partial class PersonGrid : UserControl, INavigatable
    {
        public PersonGrid()
        {
            InitializeComponent();
            DataContext = new PersonGridViewModel();
        }
    }
}
