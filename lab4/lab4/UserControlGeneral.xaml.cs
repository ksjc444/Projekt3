using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for UserControlGeneral.xaml
    /// </summary>
    public partial class UserControlGeneral : UserControl
    {
        public string UrlToWebsite { get; set; }
        public int ArticleID { get; set; }
        public bool IsFavourite { get; set; } = false;
        public UserControlGeneral()
        {
            InitializeComponent();
        }

        public void OpenWebsite(string url)
        {
            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = url;
                myProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void TestControlerButtno_Click(object sender, RoutedEventArgs e)
        {
            if (websiteUrl.Text != null)
                OpenWebsite(websiteUrl.Text);
        }

        private void FavouriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFavourite)
            {
                FavouriteButton.Foreground = (Brush)new BrushConverter().ConvertFromString(Application.Current.Resources["SecondaryAccentBrush"].ToString());
                FavouriteButton.Opacity = 1;
                IsFavourite = true;
            }
            else
            {
                FavouriteButton.Foreground = (Brush)new BrushConverter().ConvertFromString(Application.Current.Resources["PrimaryHueLightBrush"].ToString());
                FavouriteButton.Opacity = 0.5;
                IsFavourite = false;
            }
        }

        private void FavouriteButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
