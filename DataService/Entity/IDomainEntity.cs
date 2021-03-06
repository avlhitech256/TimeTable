﻿using System;
using System.ComponentModel;

namespace DataService.Entity
{
    public interface IDomainEntity<T> : INotifyPropertyChanged where T : class
    {
        long Position { get; set; }
        long Id { get; }
        string Code { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        DateTime Created { get; }
        DateTime LastModify { get; }
        string UserModify { get; }
        T Entity { get; }
        bool HasChanges { get; }
        void UpdateHasChanges();
        void RefreshChildItems();
    }
}
