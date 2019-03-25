using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using _04_Lopukhina.Models;
using _04_Lopukhina.Tools;
using _04_Lopukhina.Tools.Exceptions;
using _04_Lopukhina.Tools.Managers;
using _04_Lopukhina.Tools.Navigation;

namespace _04_Lopukhina.ViewModels
{
    class PersonEditorViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = StationManager.CurrentPerson;
        #endregion

        #region Commands
        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _backCommand;
        #endregion

        #region Properties
        public Person Person =>_person;
        #endregion

        #region Commands

        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(o => ProceedImplementation(), CanExecute));

        public RelayCommand<object> BackCommand =>
            _backCommand ?? (_backCommand = new RelayCommand<object>(o => BackImplementation(), CanExecute));

        #endregion

        private async void ProceedImplementation()
        {
            LoaderManager.Instance.ShowLoader();
            bool isFinished = await Task.Run(() =>
            {
                try
                {
                    Person.EmailValidation(Person.Email);
                }
                catch (NotValidEmailException e)
                {
                    MessageBox.Show($"Email is not valid: {e.Message}");
                    return false;
                }

                try
                {
                    Person.IsAgeCorrect(Person.Age);
                }
                catch (NotBornException e)
                {
                    MessageBox.Show($"Not correct age: {e.Message}");
                    return false;
                }
                catch (OldToBeAliveException e)
                {
                    MessageBox.Show($"Not correct age: {e.Message}");
                    return false;
                }

                if (Person.IsBirthday)
                {
                    MessageBox.Show(Person.HbCongratulations);
                }

                return true;
            });
            LoaderManager.Instance.HideLoader();

            if (isFinished)
            {
                StationManager.DataStorage.AddPerson(_person);
                StationManager.DataStorage.SaveChanges();
                // StationManager.PersonGrid.Update();
                NavigationManager.Instance.Navigate(ViewType.PersonGrid);
            }
        }

        private void BackImplementation()
        {
            NavigationManager.Instance.Navigate(ViewType.PersonGrid);
        }

        private bool CanExecute(object obj) =>
            !String.IsNullOrEmpty(Person.FirstName) &&
            !String.IsNullOrEmpty(Person.LastName) &&
            !String.IsNullOrEmpty(Person.Email);

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
