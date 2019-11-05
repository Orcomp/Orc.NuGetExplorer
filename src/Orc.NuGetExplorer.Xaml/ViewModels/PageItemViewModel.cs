﻿namespace Orc.NuGetExplorer.ViewModels
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.Fody;
    using Catel.Logging;
    using Catel.MVVM;
    using NuGet.Versioning;
    using Orc.NuGetExplorer.Enums;
    using Orc.NuGetExplorer.Models;
    using Orc.NuGetExplorer.Providers;

    internal class PageItemViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IRepositoryContextService _repositoryService;

        private readonly IModelProvider<ExplorerSettingsContainer> _settingsProvider;

        public PageItemViewModel(NuGetPackage package, IRepositoryContextService repositoryService, IModelProvider<ExplorerSettingsContainer> settingsProvider)
        {
            Argument.IsNotNull(() => package);
            Argument.IsNotNull(() => repositoryService);
            Argument.IsNotNull(() => settingsProvider);

            _repositoryService = repositoryService;
            _settingsProvider = settingsProvider;

            Package = package;
        }

        [Model(SupportIEditableObject = false)]
        [Expose("Title")]
        [Expose("Description")]
        [Expose("Summary")]
        [Expose("DownloadCount")]
        [Expose("Authors")]
        [Expose("IconUrl")]
        [Expose("Identity")]
        public NuGetPackage Package { get; set; }

        [ViewModelToModel]
        public PackageStatus Status { get; set; }

        public bool IsDownloadCountShowed { get; private set; }

        public bool CanBeAddedInBatchOperation { get; set; }

        public bool IsSecondaryVersionShowed { get; private set; } = false;

        public bool IsChecked { get; set; }

        public NuGetVersion FirstVersion { get; set; }

        public NuGetVersion SecondaryVersion { get; set; }

        protected override Task InitializeAsync()
        {
            var packageOrigin = Package.FromPage;

            IsDownloadCountShowed = packageOrigin != MetadataOrigin.Installed;
            CanBeAddedInBatchOperation = packageOrigin == MetadataOrigin.Updates;

            FirstVersion = Package.Identity.Version;

            switch (packageOrigin)
            {
                case MetadataOrigin.Browse:
                    IsSecondaryVersionShowed = true;
                    SecondaryVersion = Package.InstalledVersion;
                    break;

                case MetadataOrigin.Updates:
                    FirstVersion = Package.InstalledVersion;
                    break;
            }

            return base.InitializeAsync();
        }
    }
}
