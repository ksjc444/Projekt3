using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Settings : INotifyPropertyChanged
    {
        private int removeNewsAfterInDays = Properties.Settings.Default.RemoveNewsAfterInDays;
        private int newsAutoRefreshInHours = Properties.Settings.Default.NewsAutoRefreshInHours;
        private bool refreshNewsOnStartup = Properties.Settings.Default.RefreshNewsOnStartup;
        private string theme = Properties.Settings.Default.Theme;

        public int RemoveNewsAfterInDays
        {
            get
            {
                return removeNewsAfterInDays;
            }
            set
            {
                if (removeNewsAfterInDays != value)
                {
                    removeNewsAfterInDays = value;
                    OnPropertyChanged();
                }
            }
        }
        public int NewsAutoRefreshInHours
        {
            get
            {
                return newsAutoRefreshInHours;
            }
            set
            {
                if (newsAutoRefreshInHours != value)
                {
                    newsAutoRefreshInHours = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool RefreshNewsOnStartup
        {
            get
            {
                return refreshNewsOnStartup;
            }
            set
            {
                if (refreshNewsOnStartup != value)
                {
                    refreshNewsOnStartup = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Theme
        {
            get
            {
                return theme;
            }
            set
            {
                if (theme != value)
                {
                    theme = value;
                    OnPropertyChanged();
                }
            }
        }

        //public void setAll(int RemoveNewsAfterInDays, int NewsAutoRefreshInHours, bool RefreshNewsOnStartup, string Theme)
        //{
        //    this.RemoveNewsAfterInDays = RemoveNewsAfterInDays;
        //    this.NewsAutoRefreshInHours = NewsAutoRefreshInHours;
        //    this.RefreshNewsOnStartup = RefreshNewsOnStartup;
        //    this.Theme = Theme;
        //}

        public void SaveChanges()
        {
            Properties.Settings.Default.RemoveNewsAfterInDays = removeNewsAfterInDays;
            Properties.Settings.Default.NewsAutoRefreshInHours = newsAutoRefreshInHours;
            Properties.Settings.Default.RefreshNewsOnStartup = refreshNewsOnStartup;
            Properties.Settings.Default.Theme = theme;

            Properties.Settings.Default.Save();
        }

        public void CancelChanges()
        {
            RemoveNewsAfterInDays = Properties.Settings.Default.RemoveNewsAfterInDays;
            NewsAutoRefreshInHours = Properties.Settings.Default.NewsAutoRefreshInHours;
            RefreshNewsOnStartup = Properties.Settings.Default.RefreshNewsOnStartup;
            Theme = Properties.Settings.Default.Theme;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
