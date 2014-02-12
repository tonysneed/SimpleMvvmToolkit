using System;
using System.Linq;
using System.Collections.Generic;

namespace $safeprojectname$
{
    // Represents a typical service agent interface
    public interface IItemListServiceAgent
    {
        // Retrieve entities from the Service
        void GetItems(Action<List<Item>, Exception> completed);

        // Insert an entity
        void AddItem(Item item);

        // Remove an entity
        void RemoveItem(Item item);

        // Save changes to entities
        void SaveChanges(Action<Exception> completed);

        // Reject changes to entities
        void RejectChanges();
    }
}