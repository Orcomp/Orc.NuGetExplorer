﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPackageCommandService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Orc.NuGetExplorer
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IPackageCommandService
    {
        #region Methods
        string GetActionName(PackageOperationType operationType);

        [ObsoleteEx(TreatAsErrorFromVersion = "4.0", RemoveInVersion = "5.0", ReplacementTypeOrMember = "ExecuteAsync")]
        Task ExecuteAsync(PackageOperationType operationType, IPackageDetails packageDetails, IRepository sourceRepository = null, bool allowedPrerelease = false);
        Task ExecuteAsync(PackageOperationType operationType, IPackageDetails packageDetails);
        Task<bool> CanExecuteAsync(PackageOperationType operationType, IPackageDetails package);
        bool IsRefreshRequired(PackageOperationType operationType);
        string GetPluralActionName(PackageOperationType operationType);
        Task ExecuteInstallAsync(IPackageDetails packageDetails, CancellationToken token);
        Task ExecuteUninstallAsync(IPackageDetails packageDetails, CancellationToken token);
        Task ExecuteUpdateAsync(IPackageDetails packageDetails, CancellationToken token);
        #endregion
    }
}
