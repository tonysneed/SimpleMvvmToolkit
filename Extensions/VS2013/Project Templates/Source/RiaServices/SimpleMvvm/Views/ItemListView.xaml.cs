using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

// Toolkit namespace
using SimpleMvvmToolkit;

namespace SimpleMvvm
{
    public partial class ItemListView : Page
    {
        // Reference view model
        ItemListViewModel model;

        public ItemListView()
        {
            InitializeComponent();

            // Get model from data context
            model = (ItemListViewModel)DataContext;

            // Subscribe to notifications from the ViewModel
            model.ErrorNotice += OnErrorNotice;
            model.SaveChangesNotice += OnSaveChangesNotice;
            model.ItemsSavedNotice += OnItemsSavedNotice;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void itemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set SelectedItem on view model
            if (model.Items != null &&
                itemsDataGrid.SelectedIndex > 0)
            {
                model.SelectedItem = model.Items[itemsDataGrid.SelectedIndex];
            }
        }

        void OnErrorNotice(object sender, NotificationEventArgs<Exception> e)
        {
            // Show user message string
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);

            // Trace information
            Debug.WriteLine(e.Data.ToString());
        }

        void OnSaveChangesNotice(object sender, NotificationEventArgs<object, bool> e)
        {
            // Prompt user to save changes
            var mbResult = MessageBox.Show("Save changes?", "Save Changes", MessageBoxButton.OKCancel);

            // Call back ViewModel with response
            e.Completed(mbResult == MessageBoxResult.OK);
        }

        void OnItemsSavedNotice(object sender, NotificationEventArgs e)
        {
            // Inform user product was saved
            MessageBox.Show(e.Message, "Save Changes", MessageBoxButton.OK);
        }

        // Add item
        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a product detail model
            Item newItem = new Item();
            ItemDetailViewModel detailModel = new ItemDetailViewModel(newItem);

            // Show ProductDetail view
            ItemDetailView itemDetail = new ItemDetailView(detailModel);
            itemDetail.Closed += (s, ea) =>
            {
                if (itemDetail.DialogResult == true)
                {
                    model.Add(newItem);
                }
            };
            itemDetail.Show();
        }

        // Edit item
        private void editItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Exit if no product selected
            if (model.SelectedItem == null) return;

            // Create a product detail model
            ItemDetailViewModel detailModel =
                new ItemDetailViewModel(model.SelectedItem);

            // Start editing
            detailModel.BeginEdit();

            // Show ProductDetail view
            ItemDetailView itemDetail = new ItemDetailView(detailModel);
            itemDetail.Closed += (s, ea) =>
            {
                if (itemDetail.DialogResult == true)
                {
                    // Confirm changes
                    detailModel.EndEdit();
                }
                else
                {
                    // Reject changes
                    detailModel.CancelEdit();
                }
            };
            itemDetail.Show();
        }

        // Remove item
        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Exit if no product selected
            if (model.SelectedItem == null)
            {
                return;
            }

            // Confirm remove, then remove
            if (MessageBox.Show("Are you sure?", "Remove Item", MessageBoxButton.OKCancel)
                == MessageBoxResult.OK)
            {
                model.RemoveItem();
            }
        }
    }
}
