namespace _04_Lopukhina.Tools.Navigation
{
    internal enum ViewType
    {
        PersonGrid,
        PersonEditor,
        PersonAdder
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
