using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssetExplorer.Models
{
    class Asset : INotifyPropertyChanged
    {
        #region Variables

        private string _deviceType;

        public string DeviceType
        {
            get { return _deviceType; }
            set
            {
                _deviceType = value;
                OnPropertyChanged();
            }
        }

        private string _serial;

        public string Serial
        {
            get { return _serial; }
            set
            {
                _serial = value;
                OnPropertyChanged();
            }
        }
        private string _mac;

        public string MAC
        {
            get { return _mac; }
            set
            {
                _mac = value;
                OnPropertyChanged();
            }
        }

        private string _user;

        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        private string _knox;

        public string Knox
        {
            get { return _knox; }
            set
            {
                _knox = value;
                OnPropertyChanged();
            }
        }

        private string _department;

        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged();
            }
        }

        private string _location;

        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        private string _ip;

        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged();
            }
        }

        private string _output;

        public string Output
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged();
            }
        }

        private string _input;

        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                OnPropertyChanged();
            }
        }

        private string _repair;

        public string Repair
        {
            get { return _repair; }
            set
            {
                _repair = value;
                OnPropertyChanged();
            }
        }

        private bool _isScrapped;

        public bool IsScrapped
        {
            get { return _isScrapped; }
            set
            {
                _isScrapped = value;
                OnPropertyChanged();
            }
        }

        private bool _isArchive;

        public bool IsArchive
        {
            get { return _isArchive; }
            set
            {
                _isArchive = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive;

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        private bool _isSelected;
        [NotMapped]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        [NotMapped]
        private bool _isModified;
        [NotMapped]
        public bool IsModified
        {
            get { return _isModified; }
            set
            {
                _isModified = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; set; }

        #endregion

        #region Constructors

        public Asset() { }

        public Asset(string deviceType, string serial, string mAC, string user, string knox, string department, string location, string iP, string output, string input, string repair, bool isScrapped, bool isArchive, bool isActive, bool isSelected, bool isModified)
        {
            DeviceType = deviceType;
            Serial = serial;
            MAC = mAC;
            User = user;
            Knox = knox;
            Department = department;
            Location = location;
            IP = iP;
            Output = output;
            Input = input;
            Repair = repair;
            IsScrapped = isScrapped;
            IsArchive = isArchive;
            IsActive = isActive;
            IsSelected = isSelected;
            IsModified = isModified;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
