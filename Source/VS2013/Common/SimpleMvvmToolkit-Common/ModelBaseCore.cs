using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SimpleMvvmToolkit
{
    // This class may not be necessary if code-generated model classes
    // already implement INotifyPropertyChanged. It is included here
    // in case you are creating model classes from scratch.

    /// <summary>
    /// Provides support to entities for two-way data binding by
    /// implementing INotifyPropertyChanged with a lambda expression.
    /// </summary>
    /// <typeparam name="TModel">Class inheriting from ModelBase</typeparam>
    [JsonObject(IsReference = true)]
    [DataContract(IsReference = true)]
    public abstract class ModelBaseCore<TModel> : INotifyPropertyChanged
    {
        /// <summary>
        /// Dispatcher for cross-thread operations.
        /// </summary>
        protected readonly IDispatcher Dispatcher;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatcher">Dispatcher for cross-thread operations.</param>
        protected ModelBaseCore(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        /// <summary>
        /// PropertyChanged event accessible to dervied classes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChangedField += value; }
            remove { propertyChangedField -= value; }
        }

        /// <summary>
        /// PropertyChanged field accessible to dervied classes.
        /// </summary>
        protected PropertyChangedEventHandler propertyChangedField;

        /// <summary>
        /// Allows you to specify a lambda for notify property changed
        /// </summary>
        /// <typeparam name="TResult">Property type</typeparam>
        /// <param name="property">Property for notification</param>
        protected virtual void NotifyPropertyChanged<TResult>
            (Expression<Func<TModel, TResult>> property)
        {
            // Fire PropertyChanged event
            BindingHelper.NotifyPropertyChanged(property, this, propertyChangedField, Dispatcher);
        }
    }
}
