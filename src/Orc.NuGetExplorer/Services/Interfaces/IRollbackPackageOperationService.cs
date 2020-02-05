﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRollbackPackageOperationService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;

    public interface IRollbackPackageOperationService
    {
        #region Methods
        void PushRollbackAction(Action rollbackAction, IPackageOperationContext context);
        void Rollback(IPackageOperationContext context);
        void ClearRollbackActions(IPackageOperationContext context);
        #endregion
    }
}
