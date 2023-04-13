using LanguageSchool.Components;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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
    /// Interaction logic for ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        private Service _service;
        private bool isEdit = false;
        public ServicePage(Service service)
        {
            InitializeComponent();

            _service = service;

            if(service != null)
            {
                isEdit= true;
            }
            else
            {
                _service = new Service();
            }

            tbID.Visibility = isEdit? Visibility.Visible : Visibility.Collapsed;
            tblID.Visibility = isEdit? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = _service;
        }

        private void btnGoBackClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnSaveClick(object sender, RoutedEventArgs e)
        {

            if(!isEdit)
            {
                var res = App.Connection.Service.FirstOrDefault(x => x.Title== _service.Title);
                if(res != null)
                {
                    MessageBox.Show("Услуга с таким названием уже существует!");
                    return;
                }
            }

            if(_service.DurationInSeconds * 1.0 / 3600 > 4) 
            {
                MessageBox.Show("Услуга с протяженностью более 4 часов не может быть создана!!");
                return;
            }

            App.Connection.Service.AddOrUpdate(_service);
            App.Connection.SaveChanges();


            MessageBox.Show("Услуга успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService.GoBack();
        }

        private void btnAddImageClick(object sender, RoutedEventArgs e)
        {
            var window = new OpenFileDialog();

            if(window.ShowDialog() != true)
            {
                MessageBox.Show("Изображение не выбрано");
                return;
            }

            var byteArray = File.ReadAllBytes(window.FileName);
            _service.PhotoBytes = byteArray;
        }
    }
}
