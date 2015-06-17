﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Monitor_kl2.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private string _adress;
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public string Address
        {
            get { return _adress; }
            set
            {
                _adress = value;
                NotifyOfPropertyChange(() => Address);
                NotifyOfPropertyChange(() => CanConnect);
            }
        }

        public bool CanConnect
        {
            get { return (!string.IsNullOrWhiteSpace(Address) && !Items.Any(item => item.DisplayName == Address)); }
        }

        public void Connect()
        {
            ActivateItem(new MonitorViewModel(_windowManager, new MonitorHost(Address)));
            NotifyOfPropertyChange(() => CanConnect);
        }

        public void Close()
        {

        }

        protected override IScreen EnsureItem(IScreen newItem)
        {
            return base.EnsureItem(newItem);
        }

    }
}
