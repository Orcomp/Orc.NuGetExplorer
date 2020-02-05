﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDefaultPackageSourcesProvider.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System.Collections.Generic;

    public interface IDefaultPackageSourcesProvider
    {
        #region Properties
        string DefaultSource { get; set; }
        #endregion

        #region Methods
        IEnumerable<IPackageSource> GetDefaultPackages();
        #endregion
    }
}
