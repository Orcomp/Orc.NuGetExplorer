﻿namespace Orc.NuGetExplorer.Controls
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Catel.MVVM;
    using NuGetExplorer.Controls.Helpers;

    public class InfiniteScrollListBox : ListBox
    {
        private ScrollViewer _scrollViewer;

        public InfiniteScrollListBox()
        {
            Loaded += ListBoxLoaded;
        }

        private void ListBoxLoaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer = WpfHelper.FindVisualChild<ScrollViewer>(this);
        }

        private ScrollViewer ScrollViewer
        {
            get { return _scrollViewer; }
            set
            {
                if (value != _scrollViewer)
                {
                    if (_scrollViewer != null)
                    {
                        _scrollViewer.ScrollChanged -= OnScrollViewerScrollChanged;
                    }

                    _scrollViewer = value;

                    if (_scrollViewer != null)
                    {
                        _scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
                    }
                }
            }
        }

        private async void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrolled = _scrollViewer.VerticalOffset;

            var last = _scrollViewer.ViewportHeight + scrolled;

            if (ScrollSize > last)
            {
                return;
            }

            if (_scrollViewer.ViewportHeight > 0 && last >= Items.Count)
            {
                await ExecuteLoadingItemsCommandAsync();
            }
        }

        protected async Task ExecuteLoadingItemsCommandAsync()
        {
            Command?.Execute(CommandParameter);
        }

        public TaskCommand Command
        {
            get { return (TaskCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(TaskCommand), typeof(InfiniteScrollListBox), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(InfiniteScrollListBox), new PropertyMetadata(0));



        public bool IsCommandExecuting
        {
            get { return (bool)GetValue(IsCommandExecutingProperty); }
            set { SetValue(IsCommandExecutingProperty, value); }
        }

        public static readonly DependencyProperty IsCommandExecutingProperty =
            DependencyProperty.Register(nameof(IsCommandExecuting), typeof(bool), typeof(InfiniteScrollListBox),
                new PropertyMetadata(false, (s, e) => ((InfiniteScrollListBox)s).OnIsCommandExecutingChanged(e)));

        private void OnIsCommandExecutingChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        public int ScrollSize
        {
            get { return (int)GetValue(ScrollSizeProperty); }
            set { SetValue(ScrollSizeProperty, value); }
        }

        public static readonly DependencyProperty ScrollSizeProperty =
            DependencyProperty.Register(nameof(ScrollSize), typeof(int), typeof(InfiniteScrollListBox), new PropertyMetadata(0));

    }
}
