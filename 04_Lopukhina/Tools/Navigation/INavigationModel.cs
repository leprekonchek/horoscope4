namespace _04_Lopukhina.Tools.Navigation
{
    internal enum ViewType
    {
        PersonGrid,
        PersonEditor
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
