﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PackageOperationContext.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System.Collections.Generic;
    using NuGet;

    internal class PackageOperationContext
    {
        #region Fields
        private static int _contextCounter;
        #endregion

        #region Constructors
        public PackageOperationContext()
        {
            Id = _contextCounter++;
        }
        #endregion

        #region Properties
        public int Id { get; private set; }
        public bool CanCrashParentContext { get; set; }
        public IPackageRepository Repository { get; set; }
        public IPackageDetails[] Packages { get; set; }
        public PackageOperationType OperationType { get; set; }
        public PackageOperationContext Parent { get; set; }
        #endregion
    }
}