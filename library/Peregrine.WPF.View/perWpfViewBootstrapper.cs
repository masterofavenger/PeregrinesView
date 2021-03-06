﻿using Peregrine.WPF.View.DialogService;
using Peregrine.WPF.ViewModel.DialogService;
using Peregrine.WPF.ViewModel.Helpers;

namespace Peregrine.WPF.View
{
    public static class perWpfViewBootstrapper
    {
        public static void Run()
        {
            perDispatcherHelper.Initialise();

            perIoC.RegisterInterfaceImplementation<IperDialogService, perDialogService>();
        }
    }
}
