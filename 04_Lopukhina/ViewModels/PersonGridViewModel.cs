using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using _04_Lopukhina.Models;
using _04_Lopukhina.Tools;
using _04_Lopukhina.Tools.Managers;
using _04_Lopukhina.Tools.Navigation;

namespace _04_Lopukhina.ViewModels
{

    class PersonGridViewModel : INotifyPropertyChanged
    {
        #region Fields

        private List<Person> _personsList = StationManager.DataStorage.PersonsList;
        private Person _selectedPerson;
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        private RelayCommand<object> _refreshCommand;
        private RelayCommand<object> _filterCommand;
        private RelayCommand<object> _sortCommand;

        private string[] _sortByList = { "Name", "Surname", "Email", "Birthday", "SunSign", "ChineseSign" };
        private string[] _filterByList = { "Name", "Surname", "Email", "SunSign", "ChineseSign" };

        private int _sortByIndex = 0;
        private int _filterByIndex = 0;
        #endregion

        public List<Person> PersonsList
        {
            get { return _personsList; }
        }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> AddPersonCommand => _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(AddPersonImplementation));

        public RelayCommand<Object> EditPersonCommand => _editPersonCommand ?? (_editPersonCommand = new RelayCommand<object>(EditPersonImplementation));

        public RelayCommand<Object> DeletePersonCommand =>
            _deletePersonCommand ?? (_deletePersonCommand = new RelayCommand<object>(
                DeletePersonImplementation, CanExecute));

        public RelayCommand<Object> FilterCommand =>
            _filterCommand ?? (_filterCommand = new RelayCommand<object>());

        public RelayCommand<Object> SortCommand
        {
            get => _sortCommand ?? (_sortCommand = new RelayCommand<object>(
                       o => NavigationManager.Instance.Navigate(ViewType.PersonEditor)));
        }

        public RelayCommand<object> RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(
                (o => { OnPropertyChanged("PersonList"); })));

        private void AddPersonImplementation(object obj)
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.PersonEditor);
        }

        private void EditPersonImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.PersonEditor);
        }

        private void DeletePersonImplementation(object obj)
        {
            StationManager.DataStorage.DeletePerson(SelectedPerson);
            OnPropertyChanged("PersonsList");
        }

        public bool CanExecute(object obj) => _selectedPerson != null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
