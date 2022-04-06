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

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WarHammer40kDBEntities DB = new WarHammer40kDBEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void FacCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Unhiding and hiding stuff
            SubFacCombo.Visibility = Visibility.Visible;
            SubFacCombo.IsHitTestVisible = true;
            SubFactionLabel.Visibility = Visibility.Visible;
            FactionLabel.Visibility = Visibility.Hidden;

            //Query and Output
                int ID = FacCombo.SelectedIndex +  1; 
                var query = from sub in DB.SubFactions
                            where ID.Equals(sub.FactionID)
                            select sub.Name;

                foreach (string subfac in query)
                {
                    SubFacCombo.Items.Add(subfac);
                }
        }

        private void SubFacCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubFactionLabel.Visibility = Visibility.Hidden;
        }
    }
}
