using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using _04_Lopukhina.Annotations;
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
        private int _sortIndex;
        private int _filterIndex;
        private string[] _sortList = { "FirstName", "LastName", "Email", "Birthday", "SunSign", "ChineseSign" };
        private string[] _filterList = { "FirstName", "LastName", "Email", "SunSign", "ChineseSign" };
        #endregion

        #region Commands
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _filterCommand;
        #endregion

        #region Properties

        public PersonGridViewModel()
        {
            StationManager.gridVM = this;
        }

        public string FilterWord { get; set; }

        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                _sortIndex = value;
                Update();
            }
        }

        public int FilterIndex
        {
            get => _filterIndex;
            set
            {
                _filterIndex = value;
                Update();
            }
        }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged();
                StationManager.CurrentPerson = _selectedPerson;
            }
        }

        public IEnumerable<Person> MyPersonsList
        {
            get
            {
                IEnumerable<Person> list = _personsList;
                switch (SortIndex)
                {
                    case 0: list = list.OrderBy(p => p.FirstName); break;
                    case 1: list = list.OrderBy(p => p.LastName); break;
                    case 2: list = list.OrderBy(p => p.Email); break;
                    case 3: list = list.OrderBy(p => p.Birthday); break;
                    case 4: list = list.OrderBy(p => p.SunSign); break;
                    case 5: list = list.OrderBy(p => p.ChineseSign); break;
                }

                if (String.IsNullOrWhiteSpace(FilterWord)) return list;

                switch (FilterIndex)
                {
                    case 0: list = list.Where(p => p.FirstName.Contains(FilterWord)); break;
                    case 1: list = list.Where(p => p.LastName.Contains(FilterWord)); break;
                    case 2: list = list.Where(p => p.Email.Contains(FilterWord)); break;
                    case 3: list = list.Where(p => p.SunSign.Contains(FilterWord)); break;
                    case 4: list = list.Where(p => p.ChineseSign.Contains(FilterWord)); break;
                }

                return list;
            }
        }

        public IEnumerable<string> SortList => _sortList;

        public IEnumerable<string> FilterList => _filterList;

        #endregion

        #region Commands

        public RelayCommand<object> AddPersonCommand => _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(o => AddPersonImplementation()));

        public RelayCommand<object> EditPersonCommand => _editPersonCommand ?? (_editPersonCommand = new RelayCommand<object>(o => EditPersonImplementation(), CanExecute));

        public RelayCommand<object> SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(o => SaveImplementation()));

        public RelayCommand<object> DeletePersonCommand => _deletePersonCommand ?? (_deletePersonCommand = new RelayCommand<object>(o => DeletePersonImplementation(), CanExecute));

        public RelayCommand<object> FilterCommand => _filterCommand ?? (_filterCommand = new RelayCommand<object>(o => { Update(); }));

        #endregion

        #region CommandImplementation

        private void AddPersonImplementation()
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.PersonAdder);
        }

        private void EditPersonImplementation()
        {
            StationManager.CurrentPerson = SelectedPerson;
            StationManager.TempPerson = new Person(StationManager.CurrentPerson.FirstName, StationManager.CurrentPerson.LastName, StationManager.CurrentPerson.Email, StationManager.CurrentPerson.Birthday);
            StationManager.editorVM.UpdatePersons();
            NavigationManager.Instance.Navigate(ViewType.PersonEditor);
        }

        private async void DeletePersonImplementation()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                StationManager.DataStorage.DeletePerson(SelectedPerson);
                OnPropertyChanged(nameof(MyPersonsList));
            });
            LoaderManager.Instance.HideLoader();
        }

        private void SaveImplementation()
        {
            StationManager.DataStorage.SaveChanges();
        }

        #endregion

        public bool CanExecute(object obj) => SelectedPerson != null;

        public void Update() { OnPropertyChanged(nameof(MyPersonsList)); }

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}