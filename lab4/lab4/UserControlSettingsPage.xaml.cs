using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Interaction logic for UserControlSettingsPage.xaml
    /// </summary>
    public partial class UserControlSettingsPage : UserControl
    {
        Settings settings = new Settings();
        public UserControlSettingsPage()
        {
            InitializeComponent();
            this.DataContext = settings;
        }

        private void CloseSettingsWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    if (settings.ThemeChanged)
                    {
                        (window as MainWindow).ChangeTheme();
                        settings.ThemeChanged = false;
                    }

                    (window as MainWindow).settingsDisplayGrid.Children.Clear();
                }
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            settings.SaveChanges();
            CloseSettingsWindow();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            settings.CancelChanges();
            CloseSettingsWindow();
        }

        private void TestToggle_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ClearDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new ArticleContext())
            {
                db.Articles.RemoveRange(db.Articles);
                db.SaveChanges();
            }
        }

        private void RemoveNewsAfterInDays_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NewsAutoRefreshInHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
