﻿using Jib.WPF.Controls.DataGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AssetExplorer.Models
{
    class BindableMultiSelectDataGrid : JibGrid
    {
        public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register("SelectedItems", typeof(IList), typeof(BindableMultiSelectDataGrid), new PropertyMetadata(default(IList)));

        public new IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => throw new Exception("This property is read-only. To bind to it you must use 'Mode=OneWayToSource'.");
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            SetValue(SelectedItemsProperty, base.SelectedItems);
        }
    }
}
