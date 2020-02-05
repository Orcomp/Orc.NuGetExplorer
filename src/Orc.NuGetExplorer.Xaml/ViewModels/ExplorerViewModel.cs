﻿namespace Orc.NuGetExplorer.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Catel;
    using Catel.Configuration;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.MVVM;
    using NuGet.Protocol.Core.Types;
    using NuGetExplorer.Models;
    using NuGetExplorer.Providers;
    using Orc.NuGetExplorer.Services;

    internal class ExplorerViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private const string DefaultStartPage = ExplorerPageName.Browse;
        private readonly IConfigurationService _configurationService;
        private readonly INuGetExplorerInitializationService _initializationService;
        private readonly ITypeFactory _typeFactory;

        private readonly IDictionary<string, INuGetExplorerInitialState> _pageSetup = new Dictionary<string, INuGetExplorerInitialState>()
        {
            { ExplorerPageName.Browse, new NuGetExplorerInitialState(ExplorerTab.Browse, null)},
            { ExplorerPageName.Installed, new NuGetExplorerInitialState(ExplorerTab.Installed, null)},
            { ExplorerPageName.Updates, new NuGetExplorerInitialState(ExplorerTab.Update, null)}
        };

        private string _startPage = DefaultStartPage;


        public ExplorerViewModel(ITypeFactory typeFactory, ICommandManager commandManager,
            IModelProvider<ExplorerSettingsContainer> settingsProvider, IConfigurationService configurationService, INuGetExplorerInitializationService initializationService)
        {
            Argument.IsNotNull(() => typeFactory);
            Argument.IsNotNull(() => commandManager);
            Argument.IsNotNull(() => settingsProvider);
            Argument.IsNotNull(() => configurationService);
            Argument.IsNotNull(() => initializationService);

            _typeFactory = typeFactory;
            _configurationService = configurationService;
            _initializationService = initializationService;

            CreateApplicationWideCommands(commandManager);

            if (settingsProvider is ExplorerSettingsContainerModelProvider settingsLazyProvider)
            {
                //force settings model re-initialization from config
                settingsLazyProvider.IsInitialized = false;
            }

            Settings = settingsProvider.Model;

            Title = "Package management";

            IsLogAutoScroll = true;
        }

        protected override Task InitializeAsync()
        {
            ExplorerPages = new ObservableCollection<ExplorerPageViewModel>();

            CreatePages();
            return base.InitializeAsync();
        }


        //View to viewmodel
        public string StartPage { get; set; } = DefaultStartPage;

        public ExplorerSettingsContainer Settings { get; set; }

        public IPackageSearchMetadata SelectedPackageMetadata { get; set; }

        public NuGetPackage SelectedPackageItem { get; set; }

        public ObservableCollection<ExplorerPageViewModel> ExplorerPages { get; set; }

        public bool IsLogAutoScroll { get; set; }

        public void ChangeStartPage(string name)
        {
            _startPage = name;
        }

        public void SetInitialPageParameters(INuGetExplorerInitialState initialState)
        {
            var pagename = initialState.Tab.Name;

            if (string.IsNullOrEmpty(pagename))
            {
                Log.Error("Name for explorer page cannot be null or empty");
                return;
            }

            if (_pageSetup.ContainsKey(pagename))
            {
                _pageSetup[pagename] = initialState;
            }
        }

        private void CreatePages()
        {
            foreach (var page in _pageSetup)
            {
                var newPage = _typeFactory.CreateInstanceWithParametersAndAutoCompletion<ExplorerPageViewModel>(page.Value);

                if (newPage != null)
                {
                    ExplorerPages.Add(newPage);
                }
            }

            StartPage = _startPage;
        }

        protected override Task OnClosingAsync()
        {
            _configurationService.SetLastRepository("Browse", Settings.ObservedFeed.Name);

            Settings.Clear();

            return base.OnClosingAsync();
        }

        private void CreateApplicationWideCommands(ICommandManager cm)
        {
            if (!cm.IsCommandCreated("RefreshCurrentPage"))
            {
                cm.CreateCommand("RefreshCurrentPage", new Catel.Windows.Input.InputGesture(Key.F5));
            }
        }
    }
}
