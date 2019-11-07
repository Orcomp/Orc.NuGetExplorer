﻿using System.ComponentModel;
using Catel.IoC;
using Catel.MVVM;
using Catel.Services;
using Orc.NuGetExplorer;
using Orc.NuGetExplorer.Configuration;
using Orc.NuGetExplorer.Models;
using Orc.NuGetExplorer.Providers;
using Orc.NuGetExplorer.Scenario;
using Orc.NuGetExplorer.ViewModels;
using Orc.NuGetExplorer.Views;
using Orc.NuGetExplorer.Windows;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IApplicationCacheProvider, ExplorerCacheProvider>();
        serviceLocator.RegisterType<INuGetProjectContextProvider, NuGetProjectContextProvider>();

        serviceLocator.RegisterType<ISynchronizeInvoke, SynchronizeInvoker>();
        serviceLocator.RegisterType<IPackageMetadataMediaDownloadService, PackageMetadataMediaDownloadService>();
        serviceLocator.RegisterType<IImageResolveService, PackageMetadataMediaDownloadService>();
        serviceLocator.RegisterType<IPackageBatchService, PackageBatchService>();
        serviceLocator.RegisterType<IPackageCommandService, PackageCommandService>();
        serviceLocator.RegisterType<IPackageDetailsService, PackageDetailsService>();
        serviceLocator.RegisterType<IPackagesUIService, PackagesUIService>();
        serviceLocator.RegisterType<IPagingService, PagingService>();
        serviceLocator.RegisterType<IRepositoryNavigatorService, RepositoryNavigatorService>();
        serviceLocator.RegisterType<ISearchSettingsService, SearchSettingsService>();
        serviceLocator.RegisterType<ISearchResultService, SearchResultService>();
        serviceLocator.RegisterType<IPleaseWaitInterruptService, XamlPleaseWaitInterruptService>();
        serviceLocator.RegisterType<IMessageDialogService, MessageDialogService>();
        serviceLocator.RegisterType<ISynchronousUiVisualizer, SynchronousUIVisualizerService>();
        serviceLocator.RegisterType<IAnimationService, AnimationService>();
        serviceLocator.RegisterType<IProgressManager, ProgressManager>();

        serviceLocator.RegisterType<IRepositoryNavigationFactory, RepositoryNavigationFactory>();

        serviceLocator.RegisterType<IModelProvider<NuGetFeed>, ModelProvider<NuGetFeed>>();
        serviceLocator.RegisterType<IModelProvider<ExplorerSettingsContainer>, ExplorerSettingsContainerModelProvider>();

        var viewModelLocator = serviceLocator.ResolveType<IViewModelLocator>();
        viewModelLocator.Register(typeof(PackageSourceSettingControl), typeof(PackageSourceSettingViewModel));

        //pre-initialization to prepare old data to new NuGetExplorer versions
        var upgradeRunner = serviceLocator.RegisterTypeAndInstantiate<RunScenarioConfigurationVersionChecker>();
        var basicV3Scenario = serviceLocator.ResolveType<ITypeFactory>().CreateInstanceWithParametersAndAutoCompletion<V3RestorePackageConfigAndReinstall>();
        upgradeRunner.AddUpgradeScenario(basicV3Scenario);
        upgradeRunner.Check();

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource("Orc.NuGetExplorer.Xaml", "Orc.NuGetExplorer.Properties", "Resources"));
    }
}
