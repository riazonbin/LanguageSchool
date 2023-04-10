using LanguageSchool.Components;
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
    /// Логика взаимодействия для AddClientRecordPage.xaml
    /// </summary>
    public partial class AddClientRecordPage : Page
    {
        private Service _service;
        public AddClientRecordPage(Service service)
        {
            InitializeComponent();

            _service = service;

            this.DataContext = _service;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            cbClients.ItemsSource = App.Connection.Client.ToList();
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            if(cbClients.SelectedItem == null)
            {
                return;
            }

            ClientService clientService = new ClientService()
            {
                Client = cbClients.SelectedItem as Client,
                Service = _service,

            };
        }
    }
}
