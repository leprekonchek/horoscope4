using System;
using System.Windows;
using _04_Lopukhina.Models;
using _04_Lopukhina.Tools.DataStorage;

namespace _04_Lopukhina.Tools.Managers
{
    class StationManager
    {
        public static event Action StopThreads;
        internal static Person CurrentPerson { get; set; }

        private static IDataStorage _dataStorage;

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("Closing the app...");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }

    }
}
