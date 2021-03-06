﻿namespace Gu.Wpf.ModernUI.Demo
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class SampleFormViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private string firstName = "John";
        private string lastName;

        private bool isDirty;

        public SampleFormViewModel()
        {
            this.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName != "IsDirty")
                {
                    this.IsDirty = true;
                }
            };
            this.SubmitCommand = new RelayCommand(_ => this.IsDirty = false, _ => this.IsDirty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { get; private set; }

        public bool IsDirty
        {
            get => this.isDirty;
            set
            {
                if (value == this.isDirty)
                {
                    return;
                }

                this.isDirty = value;
                this.OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => this.firstName;
            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "FirstName")
                {
                    return string.IsNullOrEmpty(this.firstName) ? "Required value" : null;
                }

                if (columnName == "LastName")
                {
                    return string.IsNullOrEmpty(this.lastName) ? "Required value" : null;
                }

                return null;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
