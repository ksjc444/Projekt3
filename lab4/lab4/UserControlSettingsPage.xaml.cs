using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            settings.SaveChanges();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            settings.CancelChanges();
        }
    }
}
