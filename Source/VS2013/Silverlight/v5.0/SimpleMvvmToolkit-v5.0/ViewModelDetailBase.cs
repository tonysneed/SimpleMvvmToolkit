using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Base class for detail view-models. Also provides support for IEditableDataObject.
    /// </summary>
    /// <typeparam name="TViewModel">Class inheriting from ViewModelBase</typeparam>
    /// <typeparam name="TModel">Detail entity type</typeparam>
    public abstract class ViewModelDetailBase<TViewModel, TModel>
        : ViewModelDetailBaseCore<TViewModel, TModel>
        where TModel : class, INotifyPropertyChanged
    {
        /// <summary>
        /// Protected constructor for ViewModelDetailBase.
        /// </summary>
        protected ViewModelDetailBase() : base(UIDispatcher.Current, MessageBus.Default)
        {
        }

        /// <summary>
        /// Detail entity
        /// </summary>
        public override TModel Model
        {
            get { return modelField; }
            set
            {
                // Fire IsValid property changed on ErrorsChanged
                var notifyDataError = value as INotifyDataErrorInfo;
                if (notifyDataError != null)
                {
                    notifyDataError.ErrorsChanged += (s, ea) =>
                    {
                        if (ModelMetaProperties.Contains(ea.PropertyName))
                        {
                            BindingHelper.InternalNotifyPropertyChanged
                                ("IsValid", this, propertyChangedField, Dispatcher);
                        }
                    };
                }
                base.Model = value;
            }
        }

        /// <summary>
        /// Returns true if there are no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool isValid = true;
                var notifyDataError = Model as INotifyDataErrorInfo;
                if (notifyDataError != null)
                {
                    isValid = !notifyDataError.HasErrors;
                }
                return isValid;
            }
        }

    }
}
