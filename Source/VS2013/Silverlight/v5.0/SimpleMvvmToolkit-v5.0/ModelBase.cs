using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel;
using System.Linq.Expressions;

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
    public abstract class ModelBase<TModel> : ModelBaseCore<TModel>, INotifyDataErrorInfo
    {
        /// <summary>
        /// Public ctor required for serialization.
        /// </summary>
#if XAMARIN
        public ModelBase()
            : base(null)
        {
        }
#else
        public ModelBase() : base(UIDispatcher.Current)
        {
        }
#endif

        readonly Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        /// <summary>
        /// Notification that error list has changed.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Enumeration for sequence of errors.
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns>Sequence of errors.</returns>
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            CheckErrors(propertyName);
            foreach (var error in _errors[propertyName])
            {
                yield return error;
            }
        }

        /// <summary>
        /// Returns true if errors list is not empty.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return (_errors.Any(c => c.Value.Count > 0));
            }
        }

        /// <summary>
        /// Allows you to specify a lambda for property validation
        /// </summary>
        /// <typeparam name="TResult">Property type</typeparam>
        /// <param name="property">Property for validation</param>
        /// <param name="value">Value being validated</param>
        protected virtual void ValidateProperty<TResult>
            (Expression<Func<TModel, TResult>> property, object value)
        {
            // Convert expression to a property name
            string propertyName = ((MemberExpression)property.Body).Member.Name;

            // Validate property
            InternalValidateProperty(propertyName, value);
        }

        private void InternalValidateProperty(string propertyName, object value)
        {
            ICollection<ValidationResult> results = new List<ValidationResult>();
            #if NETFX_CORE || XAMARIN
            var context = new ValidationContext(this) { MemberName = propertyName };
            #else
            var context = new ValidationContext(this, null, null) { MemberName = propertyName };
            #endif
            bool isValid = Validator.TryValidateProperty(value, context, results);
            if (isValid)
            {
                RemoveErrors(propertyName);
            }
            else
            {
                AddErrors(propertyName, results);
            }
            NotifyErrorsChanged(propertyName);
            BindingHelper.InternalNotifyPropertyChanged("HasErrors", 
                this, propertyChangedField, Dispatcher);
        }

        private void AddErrors(string propertyName, IEnumerable<ValidationResult> results)
        {
            RemoveErrors(propertyName);
            _errors[propertyName].AddRange(results.Select(vr => vr.ErrorMessage));
        }

        private void RemoveErrors(string propertyName)
        {
            CheckErrors(propertyName);
            _errors[propertyName].Clear();
        }

        private void CheckErrors(string propertyName)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }
        }

        private void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}
