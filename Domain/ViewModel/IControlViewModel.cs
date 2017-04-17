﻿using System.ComponentModel;
using System.Windows.Input;
using Common.Annotations;
using Common.Data.Enum;
using Common.DomainContext;
using Common.Messenger;

namespace Domain.ViewModel
{
    public interface IControlViewModel : INotifyPropertyChanged
    {
        [CanBeNull]
        IDomainContext DomainContext { get; }

        [CanBeNull]
        IMessenger Messenger { get; }

        [CanBeNull]
        ICommand BackToSearchButtonCommand { get; }

        [CanBeNull]
        ICommand NewButtonCommand { get; }

        [CanBeNull]
        ICommand ViewButtonCommand { get; }

        [CanBeNull]
        ICommand EditButtonCommand { get; }

        [CanBeNull]
        ICommand SaveButtonCommand { get; }

        [CanBeNull]
        ICommand SaveAndNewButtonCommand { get; }

        [CanBeNull]
        ICommand DeleteButtonCommand { get; }

        [CanBeNull]
        ICommand SearchButtonCommand { get; }

        [CanBeNull]
        ICommand ChangeEditModeButtonCommand { get; }

        [CanBeNull]
        string ToolTipForEditButton { get; }
        bool ReadOnly { get; set; }
        bool Enabled { get; set; }
        bool IsEditControl { get; set; }
        bool HasChanges { get; }
        void ApplySearchCriteria();
        void Add();
        void View();
        void Edit();
        void Save();
        void SaveAndAdd();
        void Delete();
        void SetResponseForDelete(ValueEnum response);
        void Back();
        void SetResponseForBack(ValueEnum response);
    }
}
