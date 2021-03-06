﻿using System;
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
    class PersonAdderViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Person _person = StationManager.CurrentPerson;
        #endregion

        #region Commands
        private RelayCommand<object> _proceedCommand;
        #endregion

       #region Properties

        public Person Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(ProceedImplementation, CanExecute));

        #endregion

        private async void ProceedImplementation(object obj)
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
                _person = new Person("", "", "");
                Person = _person;
                StationManager.gridVM.Update();
                NavigationManager.Instance.Navigate(ViewType.PersonGrid);
            }

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
