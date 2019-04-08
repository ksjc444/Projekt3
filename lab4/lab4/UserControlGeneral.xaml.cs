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
            OpenWebsite(UrlToWebsite);
        }

    }
}
