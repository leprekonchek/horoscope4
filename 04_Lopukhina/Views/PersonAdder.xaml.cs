using _04_Lopukhina.Tools.Navigation;
using _04_Lopukhina.ViewModels;

namespace _04_Lopukhina.Views
{
    public partial class PersonAdder : INavigatable
    {
        public PersonAdder()
        {
            InitializeComponent();
            DataContext = new PersonAdderViewModel();
        }
    }
}
