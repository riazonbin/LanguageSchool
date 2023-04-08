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
    /// Interaction logic for ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public List<Service> AllServices { get; set; }

        public List<Service> ModifiedServices { get; set; }

        public ServicesPage()
        {
            InitializeComponent();

            AllServices = App.Connection.Service.ToList();
            ModifiedServices = AllServices;

            lvServices.ItemsSource = AllServices;
        }

        private void tbSearchNameTextChanged(object sender, TextChangedEventArgs e)
        {
            ModifiedServices = AllServices.Where(x => x.Title.Contains(tbSearchName.Text)).ToList();

            lvServices.ItemsSource = ModifiedServices;
            SortPrice();

        }

        private void cbSortPriceSelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    lvServices.ItemsSource = ModifiedServices.OrderBy(x => x.CostWithDiscount);
                    break;

                case "По убыванию":
                    lvServices.ItemsSource = ModifiedServices.OrderByDescending(x => x.CostWithDiscount);
                    break;

                default:
                    lvServices.ItemsSource = ModifiedServices;
                    break;
            }
        }
    }
}
