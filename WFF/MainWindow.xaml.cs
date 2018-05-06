using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WFF
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ObservableCollection<Filter> mFilterList = new ObservableCollection<Filter>();

        public ObservableCollection<Filter> MyFilterList
        {
            get
            {
                return mFilterList;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            filterList.DataContext = this;
            mFilterList = new ObservableCollection<Filter>(FavoriteManager.GetFilters() as List<Filter>);
            this.KeyDown += new KeyEventHandler(KeyDown_Callback);
        }

        private void KeyDown_Callback(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var selectedItems = filterList.SelectedItems
                               .Cast<Filter>()
                               .ToList();

                foreach (Filter eachItem in selectedItems)
                {
                    mFilterList.Remove(eachItem);
                }
            }
        }

        private void addNewFilter(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(newSite.Text))
            {
                MyFilterList.Add(new Filter() { Site = newSite.Text, Status = true });
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save the current configuration?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                // Overwrite the current file hosts
                FavoriteManager.WriteFilters(MyFilterList.ToList());
                HostsWriter.WriteFilters(MyFilterList.Where(x => x.Status == true).ToList());
            }
        }
    }
}
