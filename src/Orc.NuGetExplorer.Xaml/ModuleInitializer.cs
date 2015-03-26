﻿using Catel.IoC;
using Orc.NuGetExplorer;

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

        serviceLocator.RegisterType<IImageResolveService, ImageResolveService>();  
        serviceLocator.RegisterType<IPackageBatchService, PackageBatchService>();
        serviceLocator.RegisterType<IPackageCommandService, PackageCommandService>();
        serviceLocator.RegisterType<IPackageDetailsService, PackageDetailsService>(); 
        serviceLocator.RegisterType<IPackagesUIService, PackagesUIService>();
        serviceLocator.RegisterType<IPagingService, PagingService>();  
        serviceLocator.RegisterType<IRepositoryNavigatorService, RepositoryNavigatorService>();
        serviceLocator.RegisterType<ISearchSettingsService, SearchSettingsService>();
        serviceLocator.RegisterType<ISearchResultService, SearchResultService>();        

        serviceLocator.RegisterType<IRepositoryNavigationFactory, RepositoryNavigationFactory>();
    }
}