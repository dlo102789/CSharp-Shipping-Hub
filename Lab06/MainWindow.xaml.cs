// Derek Lo
// CSC470
// Lab 06

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

namespace Lab06
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        ShippingHub Hub = new ShippingHub();    // ShippingHub Class holds most of the code-behind.
        List<Package> subPackages = null;   // A sub-list for holding packages for a specific state
        int pageCount, maxPage;
        bool editFlag = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            pageCount -= 1;
            FillEntries(subPackages[pageCount]);

            CheckButtons();

        }

        private void ScanNew_Click(object sender, RoutedEventArgs e)
        {
            ClearEntries();
            MakeSelectable();
            Add.IsEnabled = true;
            Address.Focus();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (editFlag)
            {
                Hub.EditPackage(PackageID.Text, Address.Text, City.Text, State.SelectedIndex, Zip.Text);
                LoadSelectedState(State.SelectedIndex);
                StateSort.SelectedIndex = State.SelectedIndex;
                CheckButtons();
                editFlag = false;
            }
            else
            {
                Hub.AddPackage(Address.Text, City.Text, State.SelectedIndex, Zip.Text);
                LoadSelectedState(State.SelectedIndex);
                StateSort.SelectedIndex = State.SelectedIndex;
                CheckButtons();
            }
            
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Hub.RemovePackage(PackageID.Text);
            LoadSelectedState(State.SelectedIndex);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MakeSelectable();
            Edit.IsEnabled = false;
            Remove.IsEnabled = false;
            Add.IsEnabled = true;
            editFlag = true;
            //Hub.RemovePackage(PackageID.Text);
            Address.Focus();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            pageCount++;
            FillEntries(subPackages[pageCount]);
            CheckButtons();
        }

        /// <summary>
        /// Loads ComboBox on right. Hub.States is a list of strings that
        /// contains the states that have distribution centers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StateSort_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = Hub.States;
            comboBox.SelectedIndex = 0;
        }

        private void StateSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            int selectedState = comboBox.SelectedIndex;
            LoadSelectedState(selectedState);
        }

        private void State_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.ItemsSource = Hub.States;
            comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Upon selection of a state, a sub-list of packages gets loaded.
        /// </summary>
        /// <param name="selectedState"></param>
        private void LoadSelectedState(int selectedState)
        {
            subPackages = Hub.PackagesByState(selectedState);
            if (subPackages == null)
            {
                MakeUnselectable();
                ClearEntries();
                PackageView.Background = Brushes.Gainsboro;
                PackageView.Text = "";
                CheckButtons();
            }
            else
            {
                FillEntries(subPackages[0]);

                pageCount = 0;
                maxPage = subPackages.Count - 1;

                PackageView.Background = Brushes.White;
                PackageView.Text = "Package ID's:\n\n";
                int lineCount = 1;
                foreach (Package package in subPackages)
                {
                    PackageView.Text += $"{lineCount}) {package.PackageID}\n";
                    lineCount++;
                }

                CheckButtons();
            }
        }

        private void MakeSelectable()
        {
            Arrived.IsReadOnly = false;
            Arrived.Background = Brushes.White;
            PackageInfo.Foreground = Brushes.Black;
            InfoBorder.BorderBrush = Brushes.Black;
            PackageID.Background = Brushes.White;
            PackageID.IsReadOnly = false;
            Address.Background = Brushes.White;
            Address.IsReadOnly = false;
            City.Background = Brushes.White;
            City.IsReadOnly = false;
            Zip.Background = Brushes.White;
            Zip.IsReadOnly = false;
            State.Background = Brushes.White;
        }

        private void MakeUnselectable()
        {
            Arrived.IsReadOnly = true;
            Arrived.Background = Brushes.Gainsboro;
            PackageInfo.Foreground = Brushes.DarkGray;
            InfoBorder.BorderBrush = Brushes.DarkGray;
            PackageID.Background = Brushes.Gainsboro;
            PackageID.IsReadOnly = true;
            Address.Background = Brushes.Gainsboro;
            Address.IsReadOnly = true;
            City.Background = Brushes.Gainsboro;
            City.IsReadOnly = true;
            Zip.Background = Brushes.Gainsboro;
            Zip.IsReadOnly = true;
            State.SelectedIndex = -1;
            State.Background = Brushes.Gainsboro;
        }

        private void ClearEntries()
        {
            Arrived.Text = "";
            PackageID.Text = "";
            Address.Text = "";
            City.Text = "";
            Zip.Text = "";
            State.SelectedIndex = -1;
        }

       
        private void FillEntries(Package currentPackage)
        {
            PackageInfo.Foreground = Brushes.Black;
            InfoBorder.BorderBrush = Brushes.Black;
            Arrived.Background = Brushes.White;
            Arrived.Text = currentPackage.arrivedAt.ToString();
            PackageID.Background = Brushes.White;
            PackageID.Text = currentPackage.PackageID.ToString();
            Address.Background = Brushes.White;
            Address.Text = currentPackage.Address.ToString();
            City.Background = Brushes.White;
            City.Text = currentPackage.City.ToString();
            State.SelectedIndex = currentPackage.State;
            Zip.Background = Brushes.White;
            Zip.Text = currentPackage.Zip.ToString();
        }

        private void CheckButtons()
        {
            if (pageCount == 0)
            {
                Back.IsEnabled = false;
            }
            else
            {
                Back.IsEnabled = true;
            }
            if (pageCount == maxPage)
            {
                Next.IsEnabled = false;
            }
            else
            {
                Next.IsEnabled = true;
            }
            if (subPackages == null)
            {
                Add.IsEnabled = false;
                Edit.IsEnabled = false;
                Remove.IsEnabled = false;
                ScanNew.IsEnabled = true;
            }
            else
            {
                Edit.IsEnabled = true;
                Remove.IsEnabled = true;
                Add.IsEnabled = false;
                ScanNew.IsEnabled = true;
            }
        }
    }
}
