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

namespace LanguageSchool.Pages
{
    /// <summary>
    /// Interaction logic for AdminModeEnterPage.xaml
    /// </summary>
    public partial class AdminModeEnterPage : Page
    {
        public AdminModeEnterPage()
        {
            InitializeComponent();
        }

        private void enterAdminModeBtnClick(object sender, RoutedEventArgs e)
        {
            if(tbAdminPassword.Text == "0000")
            {
                App.IsAdminMode= true;
            }

            NavigationService.GoBack();
        }
    }
}
