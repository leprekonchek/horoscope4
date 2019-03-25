using System;
using _04_Lopukhina.Views;

namespace _04_Lopukhina.Tools.Navigation
{
    internal class InitNavigationModel : NavigationModel
    {
        public InitNavigationModel(IContentOwner contentOwner) : base(contentOwner) { }
        
        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.PersonEditor:
                    ViewsDictionary.Add(viewType, new PersonEditor());
                    break;
                case ViewType.PersonGrid:
                    ViewsDictionary.Add(viewType, new PersonGridTable());
                    break;
                case ViewType.PersonAdder:
                    ViewsDictionary.Add(viewType, new PersonAdder());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
