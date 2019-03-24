using _04_Lopukhina.Tools.Navigation;
using _04_Lopukhina.ViewModels;

namespace _04_Lopukhina.Views
{
    public partial class PersonEditor : INavigatable
    {
        public PersonEditor()
        {
            InitializeComponent();
            DataContext = new PersonEditorViewModel();
        }
    }
}
