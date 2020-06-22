﻿namespace Orc.NuGetExplorer.Resolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel.IoC;
    using NuGet.Common;
    using NuGet.Configuration;
    using NuGet.Packaging;
    using NuGet.Packaging.Core;
    using NuGet.Protocol.Core.Types;
    using NuGet.Resolver;

    public class PackageResolverContext : NuGet.Resolver.PackageResolverContext
    {
        public static PackageResolverContext Empty = new PackageResolverContext(
            DependencyBehavior.Lowest,
            Enumerable.Empty<string>(),
            Enumerable.Empty<string>(),
            Enumerable.Empty<PackageReference>(),
            Enumerable.Empty<PackageIdentity>(),
            Enumerable.Empty<SourcePackageDependencyInfo>(),
            Enumerable.Empty<PackageSource>(),
            Enumerable.Empty<string>(),
            ServiceLocator.Default.ResolveType<ILogger>()
            );

        public PackageResolverContext(DependencyBehavior dependencyBehavior, IEnumerable<string> targetIds, IEnumerable<string> requiredPackageIds, IEnumerable<PackageReference> packagesConfig, IEnumerable<PackageIdentity> preferredVersions, IEnumerable<SourcePackageDependencyInfo> availablePackages, IEnumerable<PackageSource> packageSources, IEnumerable<string> ignoredIds, ILogger log) 
            : base(dependencyBehavior, targetIds, requiredPackageIds, packagesConfig, preferredVersions, availablePackages, packageSources, log)
        {
            IgnoredIds = ignoredIds;
        }

        /// <summary>
        /// Support ignore list for package dependencies
        /// </summary>
        public IEnumerable<string> IgnoredIds { get; set; }
    }
}
