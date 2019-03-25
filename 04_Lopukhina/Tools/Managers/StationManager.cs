using System;
using System.Windows;
using System.Windows.Controls;
using _04_Lopukhina.Models;
using _04_Lopukhina.Tools.DataStorage;

namespace _04_Lopukhina.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;
        internal static Person CurrentPerson { get; set; }
        internal static DataGrid PersonGrid { get; set; }

        internal static IDataStorage DataStorage { get; private set; }

        internal static void Initialize(IDataStorage dataStorage)
        {
            DataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("Closing the app...");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }

    }
}
