﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FeedVerificationResult.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    public enum FeedVerificationResult
    {
        Unknown,
        Valid,
        AuthenticationRequired,
        Invalid
    }
}