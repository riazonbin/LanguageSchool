using LanguageSchool.Components;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Interaction logic for ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public List<Service> SourceData { get; set; }

        private Predicate<Service> _filterQuery = x => true;

        private Func<Service, object> _sortQuery = x => x.ID;

        public ServicesPage()
        {
            InitializeComponent();
        }

        private void TbSearchNameTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchServicesByName();
        }

        private void SearchServicesByName()
        {
            if(lvServices == null)
            {
                return;
            }

            lvServices.ItemsSource = SourceData.Where(x => x.Title.ToLower().Contains(tbSearchName.Text.ToLower())).ToList();

            EditDataCount();
        }

        private void UpdateData()
        {
            if(lvServices == null)
            {
                return;
            }

           SourceData = App.Connection.Service.ToList()
                .Where(
                x => _filterQuery(x) && x.IsMarkedForDeletion != true)
                .OrderBy(x => _sortQuery(x))
                .ToList();

            SearchServicesByName();
            
        }

        private void CbSortPriceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortPrice();
        }

        private void SortPrice()
        {
            if(lvServices == null)
            {
                return;
            }

            ComboBoxItem item = (ComboBoxItem)cbSortPrice.SelectedItem;
            string selectedSortText = item.Content.ToString();

            switch (selectedSortText)
            {
                case "По возрастанию":
                    _sortQuery = x => x.CostWithDiscount;
                    break;

                case "По убыванию":
                    _sortQuery = x => -x.CostWithDiscount;
                    break;

                default:
                    _sortQuery = x => x.ID;
                    break;
            }

            UpdateData();
        }

        private void FiltrateDiscount()
        {
            if(lvServices == null)
            {
                return;
            }

            ComboBoxItem item = (ComboBoxItem)cbFilterDiscount.SelectedItem;
            string selectedSortText = item.Content.ToString();

            switch (selectedSortText)
            {
                case "от 0 до 5%":
                    _filterQuery = x => x.Discount >= 0 && x.Discount < 5;
                    break;

                case "от 5 до 15%":
                    _filterQuery = x => x.Discount >= 5 && x.Discount < 15;
                    break;

                case "от 15 до 30%":
                    _filterQuery = x => x.Discount >= 15 && x.Discount < 30;
                    break;

                case "от 30 до 70%":
                    _filterQuery = x => x.Discount >= 30 && x.Discount < 70;
                    break;

                case "от 70 до 100%":
                    _filterQuery = x => x.Discount >= 70 && x.Discount < 100;
                    break;

                default:
                    _filterQuery = x => true;
                    break;
            }

            UpdateData();
        }

        private void CbFilterDiscountSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrateDiscount();
        }

        private void EditDataCount()
        {
            tbDataCount.Text = $"{lvServices.Items.Count} из {App.Connection.Service.ToList().Count}";
        }

        private void AdminModeBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminModeEnterPage());
        }

        private void ServicesPageLoaded(object sender, RoutedEventArgs e)
        {
            UpdateData();

            lvServices.ItemsSource = SourceData;

            EditDataCount();

            adminModeBtn.Visibility = App.IsAdminMode ? Visibility.Collapsed : Visibility.Visible;
            btnAddNewService.Visibility = App.IsAdminMode ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BtnEditServiceClick(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;  
            var service = App.Connection.Service.FirstOrDefault(x => x.ID == tag);
            NavigationService.Navigate(new ServicePage(service));
        }

        private void BtnDeleteServiceClick(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            var service = App.Connection.Service.FirstOrDefault(x => x.ID == tag);

            var clientServices = App.Connection.ClientService.ToList().Where(x => x.ServiceID == tag);

            if(clientServices.Any())
            {
                MessageBox.Show("Какие-то записи ещё оформлены на эту услугу!");
                return;
            }

            service.IsMarkedForDeletion= true;
            App.Connection.Service.AddOrUpdate(service);
            App.Connection.SaveChanges();

            UpdateData();

            MessageBox.Show("Услуга успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void BtnAddNewServiceClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage(null));
        }

        private void LvServicesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!App.IsAdminMode)
            {
                return;
            }

            NavigationService.Navigate(new AddClientRecordPage(lvServices.SelectedItem as Service));
        }

        private void BtnShowClientRecordsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsRecordsPage());
        }
    }
}
