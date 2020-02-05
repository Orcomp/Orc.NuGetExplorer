﻿namespace Orc.NuGetExplorer.Views
{
    using System.Windows;
    using Catel.MVVM.Views;
    using Models;

    internal partial class PackageDetailsView
    {
        static PackageDetailsView()
        {
            typeof(PackageDetailsView).AutoDetectViewPropertiesToSubscribe();

        }

        public PackageDetailsView()
        {
            InitializeComponent();
        }

        [ViewToViewModel(MappingType = ViewToViewModelMappingType.ViewToViewModel)]
        public NuGetPackage Package
        {
            get { return (NuGetPackage)GetValue(PackageProperty); }
            set { SetValue(PackageProperty, value); }
        }

        public static readonly DependencyProperty PackageProperty = DependencyProperty.Register(
            "Package", typeof(NuGetPackage), typeof(PackageDetailsView), new PropertyMetadata(default(NuGetPackage)));
    }
}
