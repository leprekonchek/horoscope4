using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using _04_Lopukhina.Tools.Exceptions;

namespace _04_Lopukhina.Models
{
    [Serializable]
    class Person : INotifyPropertyChanged
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthday;
        private string _zodiacSign;
        private string _chineseSign;
        private string[] _chinaSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
        public string HbCongratulations =
            "This Birthday wish is just for you, \n And I hope it comes true: \n B e yourself, love and appreciate yourself \n I magine and achieve all you can \n R elax and take it easy \n T ake time and do whatever you want, your \n H umor, never lose it and \n D o not give up, continue going \n A nd remember, you are loved by others \n Y esterday is gone, tomorrow is not here, live today and enjoy the year.";
        #endregion

        #region Properties
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }

        public int Age
        {
            get
            {
                int age = DateTime.Today.Year - _birthday.Year;
                if (_birthday > DateTime.Today.AddYears(-age)) age--;
                return age;
            }
        }

        public bool IsAdult => Age >= 18;

        public bool IsBirthday => _birthday.Day == DateTime.Today.Day && _birthday.Month == DateTime.Today.Month;

        public string SunSign => _zodiacSign ?? (_zodiacSign = CalculateSunSign());

        public string ChineseSign => _chineseSign ?? (_chineseSign = CalculateChineseSign());
        
        private string CalculateSunSign()
        {
            int month = _birthday.Month;
            int day = _birthday.Day;

            switch (month)
            {
                case 01 when day >= 20:
                case 02 when day <= 18:
                    _zodiacSign = "Aquarius";
                    break;
                case 02 when day >= 19:
                case 03 when day <= 20:
                    _zodiacSign = "Pisces";
                    break;
                case 03 when day >= 21:
                case 04 when day <= 19:
                    _zodiacSign = "Aries";
                    break;
                case 04 when day >= 20:
                case 05 when day <= 20:
                    _zodiacSign = "Taurus";
                    break;
                case 05 when day >= 21:
                case 06 when day <= 20:
                    _zodiacSign = "Gemini";
                    break;
                case 06 when day >= 21:
                case 07 when day <= 22:
                    _zodiacSign = "Cancer";
                    break;
                case 07 when day >= 23:
                case 08 when day <= 22:
                    _zodiacSign = "Leo";
                    break;
                case 08 when day >= 23:
                case 09 when day <= 22:
                    _zodiacSign = "Virgo";
                    break;
                case 09 when day >= 23:
                case 10 when day <= 22:
                    _zodiacSign = "Libra";
                    break;
                case 10 when day >= 23:
                case 11 when day <= 21:
                    _zodiacSign = "Scorpio";
                    break;
                case 11 when day >= 22:
                case 12 when day <= 21:
                    _zodiacSign = "Sagittarius";
                    break;
                case 12 when day >= 22:
                case 01 when day <= 19:
                    _zodiacSign = "Capricorn";
                    break;
            }

            return _zodiacSign;
        }

        private string CalculateChineseSign()
        {
            int index = Math.Abs(_birthday.Year - 1900) % 12;
            return _chinaSigns[index];
        }

        public void IsAgeCorrect(int age)
        {
            if (age <= 0)
            {
                throw new NotBornException();
            }

            if (age >= 135)
            {
                throw new OldToBeAliveException(age);
            }
        }

        public void EmailValidation(string email)
        {
            Regex emailRegex = new Regex("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`{}|~\\w])*)(?<=[0-9a-z])@))(?([)([(\\d{1,3}.){3}\\d{1,3}])|(([0-9a-z][-0-9a-z]*[0-9a-z]*.)+[a-z0-9][-a-z0-9]{0,22}[a-z0-9]))$");
            if (!emailRegex.IsMatch(email))
            {
                throw new NotValidEmailException(email);
            }
        }

        #endregion

        #region Constructors
        public Person(string name, string lastName, string email, DateTime birthday)
        {
            _firstName = name;
            _lastName = lastName;
            _email = email;
            _birthday = birthday;
        }

        public Person(string name, string lastName, string email)
        {
            _firstName = name;
            _lastName = lastName;
            _email = email;
        }

        public Person(string name, string lastName, DateTime birthday)
        {
            _firstName = name;
            _lastName = lastName;
            _birthday = birthday;
        }

        #endregion

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}
