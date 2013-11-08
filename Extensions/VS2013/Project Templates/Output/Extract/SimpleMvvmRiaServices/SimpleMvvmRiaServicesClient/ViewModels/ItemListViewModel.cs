using System;
using System.Windows;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace $safeprojectname$
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class ItemListViewModel : ViewModelBase<ItemListViewModel>
    {
        #region Initialization and Cleanup

        // Add a member for service agent interface
        private IItemListServiceAgent serviceAgent;

        // Default ctor
        public ItemListViewModel() { }

        // Ctor that accepts service agent interface
        public ItemListViewModel(IItemListServiceAgent serviceAgent)
        {
            this.serviceAgent = serviceAgent;
        }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public event EventHandler<NotificationEventArgs<object, bool>> SaveChangesNotice;
        public event EventHandler<NotificationEventArgs> ItemsSavedNotice;

        #endregion

        #region Properties

        // TODO: Add properties using the mvvmprop code snippet

        private ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyPropertyChanged(m => m.Items);
            }
        }

        private Item selectedItem;
        public Item SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                SetCanProperties();
                NotifyPropertyChanged(m => m.SelectedItem);
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                SetCanProperties();
                NotifyPropertyChanged(m => m.IsBusy);
            }
        }

        private bool canLoad = true;
        public bool CanLoad
        {
            get { return canLoad; }
            set
            {
                canLoad = value;
                NotifyPropertyChanged(m => m.CanLoad);
            }
        }

        private bool canAdd;
        public bool CanAdd
        {
            get { return canAdd; }
            set
            {
                canAdd = value;
                NotifyPropertyChanged(m => m.CanAdd);
            }
        }

        private bool canEdit;
        public bool CanEdit
        {
            get { return canEdit; }
            set
            {
                canEdit = value;
                NotifyPropertyChanged(m => m.CanEdit);
            }
        }

        private bool canRemove;
        public bool CanRemove
        {
            get { return canRemove; }
            set
            {
                canRemove = value;
                NotifyPropertyChanged(m => m.CanRemove);
            }
        }

        #endregion

        #region Methods

        // TODO: Add methods that will be called by the view

        public void LoadItems()
        {
            // Load items
            serviceAgent.GetItems
                (
                    (entities, error) => ItemsLoaded(entities, error)
                );

            // Reset property
            Items = null;

            // Flip busy flag
            IsBusy = true;
        }

        // Add item
        public void Add(Item item)
        {
            if (Items != null)
            {
                serviceAgent.AddItem(item);
                Items.Add(item);
                SelectItem(item);
                SetCanProperties();
            }
        }

        // Remove selected item
        public void RemoveItem()
        {
            if (SelectedItem != null)
            {
                serviceAgent.RemoveItem(SelectedItem);
                Items.Remove(SelectedItem);
                SelectItem(null);
                SetCanProperties();
            }
        }

        // Save changes on the domain content if there are any
        public void SaveChanges()
        {
            // Prompt the user to save changes if there are any
            Notify(SaveChangesNotice, new NotificationEventArgs<object, bool>
                ("There are unsaved changes. Do you wish to save?", null,
                confirm =>
                {
                    if (confirm)
                    {
                        // Save changes
                        serviceAgent.SaveChanges
                            (error => ItemsSaved(error));
                    }
                }));
        }

        // Call RejectChanged on the service agent then reload items
        public void RejectChanges()
        {
            serviceAgent.RejectChanges();
            LoadItems();
        }

        #endregion

        #region Completion Callbacks

        // Optionally add callback methods for async calls to the service agent

        private void ItemsLoaded(List<Item> entities, Exception error)
        {
            // If no error is returned, set the model to entities
            if (error == null)
            {
                Items = new ObservableCollection<Item>(entities);
            }
            // Otherwise notify view that there's an error
            else
            {
                NotifyError("Unable to retrieve items", error);
            }

            // Set SelectedItem to the first item
            if (Items.Count > 0)
            {
                SelectedItem = Items[0];
            }

            // We're done
            IsBusy = false;
        }

        private void ItemsSaved(Exception error)
        {
            if (error == null)
            {
                // Notify view products were saved successfully
                Notify(ItemsSavedNotice, new NotificationEventArgs
                    ("Items were successfully saved"));
            }
            else
            {
                // Notify view if there's an error
                NotifyError("Unable to save items", error);
            }

            // We're done
            IsBusy = false;
        }

        #endregion

        #region Helpers

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        private void SetCanProperties()
        {
            CanLoad = !IsBusy;
            CanEdit = !IsBusy;
            CanAdd = !IsBusy;
            CanRemove = !IsBusy && SelectedItem != null;
        }

        // Set SelectedItem
        private void SelectItem(Item item)
        {
            if (Items != null && Items.Count > 0)
            {
                if (item != null)
                {
                    SelectedItem = item;
                }
                else
                {
                    SelectedItem = Items[0];
                }
            }
        }

        #endregion
    }
}