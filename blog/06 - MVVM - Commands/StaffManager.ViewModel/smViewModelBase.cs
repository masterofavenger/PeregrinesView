﻿using System.Collections.Generic;
using Peregrine.WPF.ViewModel;
using StaffManager.Model;
using System.ComponentModel;

namespace StaffManager.ViewModel
{
    public abstract class smViewModelBase : perViewModelBase
    {
    }

    public abstract class smViewModelBase<TModel> : smViewModelBase where TModel : smModelBase, new()
    {
        private readonly Dictionary<string, HashSet<string>> _relatedProperties = new Dictionary<string, HashSet<string>>();

        private TModel _model;

        public TModel Model
        {
            get => _model;
            set
            {
                var oldModel = Model;

                if (Set(nameof(Model), ref _model, value))
                {
                    if (oldModel != null)
                        oldModel.PropertyChanged -= ModelPropetyChanged;

                    if (_model != null)
                        _model.PropertyChanged += ModelPropetyChanged;

                    // refresh any VM properties that depend on the model
                    RaisePropertyChanged(string.Empty);
                }
            }
        }

        protected virtual void ModelPropetyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!_relatedProperties.TryGetValue(args.PropertyName, out var vmPropertyNames))
                return;

            foreach (var vmPropertyName in vmPropertyNames)
                RaisePropertyChanged(vmPropertyName);
        }

        /// <summary>
        /// Call RaisePropertyChanged() for a (calculated) property on the VM, whenever the specied model property is updated  
        /// </summary>
        /// <param name="modelPropertyName"></param>
        /// <param name="vmPropertyName"></param>
        protected void AddModelPropertyDependency(string modelPropertyName, string vmPropertyName)
        {
            if (!_relatedProperties.ContainsKey(modelPropertyName))
                _relatedProperties[modelPropertyName] = new HashSet<string>();

            _relatedProperties[modelPropertyName].Add(vmPropertyName);
        }
    }
}