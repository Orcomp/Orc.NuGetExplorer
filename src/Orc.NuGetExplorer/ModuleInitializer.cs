﻿using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Orc.NuGetExplorer;
using NuGet;
using IPackageManager = Orc.NuGetExplorer.IPackageManager;
using PackageManager = Orc.NuGetExplorer.PackageManager;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static class ModuleInitializer
{
    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IPackageQueryService, PackageQueryService>();
        serviceLocator.RegisterType<IPackageCacheService, PackageCacheService>();
        serviceLocator.RegisterType<IRepositoryNavigationFactory, RepositoryNavigationFactory>();
        serviceLocator.RegisterType<IPackageRepositoryService, PackageRepositoryService>();
        serviceLocator.RegisterType<IRepositoryNavigationService, RepositoryNavigationService>();
        serviceLocator.RegisterType<INuGetConfigurationService, NuGetConfigurationService>();
        serviceLocator.RegisterType<IPackagesUIService, PackagesUIService>();
        serviceLocator.RegisterType<IPackageManager, PackageManager>();
        serviceLocator.RegisterType<IPackageDetailsService, PackageDetailsService>();
        serviceLocator.RegisterType<IPagingService, PagingService>();
        serviceLocator.RegisterType<IPackageCommandService, PackageCommandService>();
        serviceLocator.RegisterType<INuGetFeedVerificationService, NuGetFeedVerificationService>();
        serviceLocator.RegisterType<ISettings, NuGetSettings>();
        serviceLocator.RegisterType<ICredentialProvider, CredentialProvider>();
        serviceLocator.RegisterType<IAuthenticationProvider, AuthenticationProvider>();
        serviceLocator.RegisterType<IPackageSourceProvider, NuGetPackageSourceProvider>();
        serviceLocator.RegisterType<IDefaultPackageSourcesProvider, EmptyDefaultPackageSourcesProvider>();
        serviceLocator.RegisterType<IPackageSourceFactory, PackageSourceFactory>();
        serviceLocator.RegisterType<INuGetLogListeningSevice, NuGetLogListeningSevice>();
        serviceLocator.RegisterType<ILogger, NuGetLogger>();
        serviceLocator.RegisterType<IPackagesUpdatesSearcherService, PackagesUpdatesSearcherService>();
        serviceLocator.RegisterType<IAuthenticationSilencerService, AuthenticationSilencerService>();
        serviceLocator.RegisterType<IImageResolveService, ImageResolveService>();
        serviceLocator.RegisterType<IPackageBatchService, PackageBatchService>();
        serviceLocator.RegisterType<IPackageOperationContextService, PackageOperationContextService>();
        serviceLocator.RegisterType<IRollbackPackageOperationService, RollbackPackageOperationService>();
        serviceLocator.RegisterType<IPackageOperationService, IPackageOperationService>();

        serviceLocator.RegisterInstance<IPackageRepositoryFactory>(PackageRepositoryFactory.Default);

        Log.Debug("Forcing the loading of assembly Catel by the following types");
        Log.Debug("  * {0}", typeof(DispatcherService).Name);

        var typeFactory = serviceLocator.ResolveType<ITypeFactory>();
        HttpClient.DefaultCredentialProvider = typeFactory.CreateInstance<NuGetSettingsCredentialProvider>();

        var nuGetPackageManager = serviceLocator.ResolveType<IPackageManager>();
        serviceLocator.RegisterInstance(typeof(IPackageOperationNotificationService), nuGetPackageManager);

        serviceLocator.RegisterTypeAndInstantiate<DeletemeWatcher>();
        serviceLocator.RegisterTypeAndInstantiate<RollbackWatcher>();
        serviceLocator.RegisterTypeAndInstantiate<NuGetToCatelLogTranstalor>();
    }
}