﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OnlineExtensionsViewModel.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Catel;
    using Catel.Collections;
    using Catel.MVVM;
    using Catel.Services;
    using NuGet;

    internal class OnlineExtensionsViewModel : ViewModelBase
    {
        #region Fields
        private IPackageRepository _packageRepository;
        private readonly IDispatcherService _dispatcherService;
        private readonly IPackageQueryService _packageQueryService;
        #endregion

        #region Constructors
        public OnlineExtensionsViewModel(IPackageQueryService packageQueryService, IDispatcherService dispatcherService)
        {
            Argument.IsNotNull(() => packageQueryService);
            Argument.IsNotNull(() => dispatcherService);

            _packageQueryService = packageQueryService;
            _dispatcherService = dispatcherService;

            AvailablePackages = new ObservableCollection<PackageDetails>();

            PackageAction = new Command(OnPackageActionExecute);

            Search();
        }
        #endregion

        #region Properties
        public string SearchFilter { get; set; }
        public PackageDetails SelectedPackage { get; set; }
        public NamedRepo NamedRepo { get; set; }
        public ObservableCollection<PackageDetails> AvailablePackages { get; private set; }
        public int TotalPackagesCount { get; set; }
        public int PackagesToSkip { get; set; }
        public string ActionName { get; set; }
        #endregion

        #region Methods
        private void OnPackagesToSkipChanged()
        {
            Search();
        }
        
        private void OnPackageSourceChanged()
        {
            UpdateRepository();            
        }

        private void OnSearchFilterChanged()
        {
            UpdateRepository();            
        }

        private void OnActionNameChanged()
        {
            UpdateRepository();
        }

        private static bool _updatingRepisitory;

        private void UpdateRepository()
        {
            if (_updatingRepisitory)
            {
                return;
            }

            using (new DisposableToken(this, x => _updatingRepisitory = true, x => _updatingRepisitory = false))
            {
                _packageRepository = NamedRepo.Value;
                PackagesToSkip = 0;
                TotalPackagesCount = _packageQueryService.GetPackagesCount(_packageRepository, SearchFilter);                
            }

            Search();
        }

        private void Search()
        {
            if (_updatingRepisitory)
            {
                return;
            }

            if (NamedRepo != null)
            {
                _dispatcherService.BeginInvoke(() =>
                {
                    var packageDetails = _packageQueryService.GetPackages(_packageRepository, SearchFilter, PackagesToSkip).ToArray();
                    AvailablePackages.ReplaceRange(packageDetails);
                });
            }
        }
        #endregion

        #region Commands
        public Command PackageAction { get; private set; }

        private void OnPackageActionExecute()
        {
        }
        #endregion
    }
}