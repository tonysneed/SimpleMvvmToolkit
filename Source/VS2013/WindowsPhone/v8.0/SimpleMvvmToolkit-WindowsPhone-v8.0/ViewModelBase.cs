using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleMvvmToolkit
{
    /// <summary>
    /// Base class for non-detail view-models
    /// </summary>
    /// <typeparam name="TViewModel">Class inheriting from ViewModelBase</typeparam>
    public abstract class ViewModelBase<TViewModel> : ViewModelBaseCore<TViewModel>, INotifyDataErrorInfo
    {
        readonly Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        /// <summary>
        /// Protected constructor for ViewModelBase.
        /// </summary>
        protected ViewModelBase()
            : base(UIDispatcher.Current, MessageBus.Default)
        {
        }

        /// <summary>
        /// Notification that errors have changed.
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
                return (_errors.Where(c => c.Value.Count > 0).Count() > 0);
            }
        }

            /// <summary>
            /// Allows you to specify a lambda for property validation
            /// </summary>
            /// <typeparam name="TResult">Property type</typeparam>
            /// <param name="property">Property for validation</param>
            /// <param name="value">Value being validated</param>
            /// <param name="validator">Delegate returning error messages if property is invalid</param>
            protected virtual void ValidateProperty<TResult>
                (Expression<Func<TViewModel, TResult>> property, TResult value, Func<TResult, IEnumerable<string>> validator)
            {
                // Convert expression to a property name
                string propertyName = ((MemberExpression)property.Body).Member.Name;

                // Validate property
                InternalValidateProperty(propertyName, value, validator);
            }

            private void InternalValidateProperty<TResult>(string propertyName, TResult value, Func<TResult, IEnumerable<string>> validator)
            {
                var errorMessages = validator(value);
                bool isValid = !errorMessages.Any();
                if (isValid)
                {
                    RemoveErrors(propertyName);
                }
                else
                {
                    AddErrors(propertyName, errorMessages);
                }
                NotifyErrorsChanged(propertyName);
                BindingHelper.InternalNotifyPropertyChanged("HasErrors", this, propertyChangedField, Dispatcher);
            }

            private void AddErrors(string propertyName, IEnumerable<string> errorMessages)
            {
                RemoveErrors(propertyName);
                _errors[propertyName].AddRange(errorMessages);
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
