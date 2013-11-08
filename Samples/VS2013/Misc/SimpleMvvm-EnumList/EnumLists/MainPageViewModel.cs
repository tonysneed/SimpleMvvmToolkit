using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

using SimpleMvvmToolkit;
using SimpleMvvmToolkit.ModelExtensions;

namespace EnumLists
{
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        public MainPageViewModel()
        {
            Weekdays = Extensions.GetEnumItems<Weekday>().ToList();
        }

        private List<EnumItem<Weekday>> weekdays;
        public List<EnumItem<Weekday>> Weekdays
        {
            get { return weekdays; }
            set
            {
                weekdays = value;
                NotifyPropertyChanged(m => m.Weekdays);
            }
        }

        private EnumItem<Weekday> selectedWeekday;
        public EnumItem<Weekday> SelectedWeekday
        {
            get { return selectedWeekday; }
            set
            {
                selectedWeekday = value;
                NotifyPropertyChanged(m => m.SelectedWeekday);

                // Fire PropertyChanged for WeekdayValue, WeekdayName
                NotifyPropertyChanged(m => m.WeekdayValue);
                NotifyPropertyChanged(m => m.WeekdayName);
            }
        }

        public int WeekdayValue
        {
            get
            {
                if (SelectedWeekday != null)
                {
                    return (int)SelectedWeekday.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string WeekdayName
        {
            get
            {
                if (this.IsInDesignMode())
                {
                    return "Unselected";
                }
                if (SelectedWeekday != null)
                {
                    return SelectedWeekday.Name;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
