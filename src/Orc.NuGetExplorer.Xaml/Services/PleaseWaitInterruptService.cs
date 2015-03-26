﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PleaseWaitInterruptService.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using System;
    using Catel;
    using Catel.Services;

    internal class PleaseWaitInterruptService : IPleaseWaitInterruptService
    {
        #region Fields
        private readonly IPleaseWaitService _pleaseWaitService;
        #endregion

        #region Constructors
        public PleaseWaitInterruptService(IPleaseWaitService pleaseWaitService)
        {
            Argument.IsNotNull(() => pleaseWaitService);

            _pleaseWaitService = pleaseWaitService;
        }
        #endregion

        #region Methods
        public IDisposable InterruptTemporarily()
        {
            return _pleaseWaitService.HideTemporarily();
        }
        #endregion
    }
}