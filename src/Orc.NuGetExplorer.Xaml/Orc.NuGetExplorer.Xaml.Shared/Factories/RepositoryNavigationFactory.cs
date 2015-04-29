﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryNavigationFactory.cs" company="Wild Gums">
//   Copyright (c) 2008 - 2015 Wild Gums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.NuGetExplorer
{
    using Catel;

    internal class RepositoryNavigationFactory : IRepositoryNavigationFactory
    {
        #region Fields
        private readonly IRepositoryService _repositoryService;
        #endregion

        #region Constructors
        public RepositoryNavigationFactory(IRepositoryService repositoryService)
        {
            Argument.IsNotNull(() => repositoryService);

            _repositoryService = repositoryService;
        }
        #endregion

        #region Methods
        public RepositoryNavigator CreateRepoNavigator()
        {
            var navigator = new RepositoryNavigator();

            navigator.RepositoryCategories.Add(CreateRepositoryCategory(PackageOperationType.Uninstall));
            navigator.RepositoryCategories.Add(CreateRepositoryCategory(PackageOperationType.Install));
            navigator.RepositoryCategories.Add(CreateRepositoryCategory(PackageOperationType.Update));

            return navigator;
        }

        private RepositoryCategory CreateRepositoryCategory(PackageOperationType packageOperationType)
        {
            var repoCategory = new RepositoryCategory();

            switch (packageOperationType)
            {
                case PackageOperationType.Install:
                    repoCategory.Name = RepositoryCategoryName.Online;
                    break;

                case PackageOperationType.Uninstall:
                    repoCategory.Name = RepositoryCategoryName.Installed;
                    break;

                case PackageOperationType.Update:
                    repoCategory.Name = RepositoryCategoryName.Update;
                    break;
            }

            foreach (var repository in _repositoryService.GetRepositories(packageOperationType))
            {
                repoCategory.Repositories.Add(repository);
            }

            return repoCategory;
        }
        #endregion
    }
}