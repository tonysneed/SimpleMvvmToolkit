using System;
using System.Linq;
using System.Collections.Generic;

// Toolkit namespace
using SimpleMvvmToolkit;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel;

namespace $safeprojectname$
{
    public class ItemListServiceAgent : IItemListServiceAgent
    {
        // TODO: Add a field of type MyDomainContext
        //MyDomainContext domainContext = new MyDomainContext();

        // TODO: Load items from domain context
        public void GetItems(Action<List<Item>, Exception> completed)
        {
            //// Load GetItemsQuery
            //EntityQuery<Item> query = domainContext.GetItemsQuery();
            //domainContext.Load(query, loadOp =>
            //{
            //    // Declare error and result
            //    Exception error = null;
            //    IEnumerable<Item> items = null;

            //    // Set error or result
            //    if (loadOp.HasError)
            //    {
            //        error = loadOp.Error;
            //    }
            //    else
            //    {
            //        items = loadOp.Entities;
            //    }

            //    // Invoke completion callback
            //    completed(items.ToList(), error);
            //}, null);
        }

        // TODO: Call Add on domain context items
        public void AddItem(Item item)
        {
            //domainContext.Items.Add(item);
        }

        // TODO: Call Remove on domain context items
        public void RemoveItem(Item item)
        {
            //domainContext.Items.Remove(item);
        }

        // TODO: Save changes on the domain content if there are any
        public void SaveChanges(Action<Exception> completed)
        {
            //// See if any products have changed
            //if (domainContext.Items.HasChanges)
            //{
            //    // Submit bulk update
            //    domainContext.SubmitChanges(submitOp =>
            //    {
            //        // Declare error
            //        Exception error = null;

            //        // Set error or result
            //        if (submitOp.HasError)
            //        {
            //            error = submitOp.Error;
            //        }

            //        // Invoke completion callback
            //        completed(error);
            //    }, null);
            //}
        }

        // TODO: Reject unsaved changes on domain context
        public void RejectChanges()
        {
            //if (this.domainContext.Items.HasChanges)
            //{
            //    this.domainContext.RejectChanges();
            //}
        }
    }
}
