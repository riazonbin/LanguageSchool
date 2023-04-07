using LanguageSchool.Components;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LanguageSchool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly LanguageSchoolEntities Connection = new LanguageSchoolEntities();

        public static bool IsAdminMode = false;

        public static Visibility IsAdminModeVisible = IsAdminMode ? Visibility.Visible : Visibility.Hidden;
    }
}
