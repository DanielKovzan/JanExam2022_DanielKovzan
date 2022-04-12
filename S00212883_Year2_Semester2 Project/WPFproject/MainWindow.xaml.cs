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
        public int MaxPoints = 500;
        public int CurrentUnitValueTotal = 0;
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
            SubFacCombo.SelectedIndex = -1;
            SubFactionLabel.Visibility = Visibility.Visible;
            FactionLabel.Visibility = Visibility.Hidden;

            //Query and Output
            int ID = FacCombo.SelectedIndex +  1;
            var query = from sub in DB.SubFactions
                        where ID.Equals(sub.FactionID)
                        select new SubFactionClass()
                        {
                            SubFactionName = sub.Name,
                            SubfactionID = sub.SubFactionID
                        };

            foreach (SubFactionClass subfac in query)
            {
                SubFacCombo.Items.Add(subfac);
            }
        }

        private void SubFacCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //revealing everything, im starting to hate this, genuinely
            SubFactionLabel.Visibility = Visibility.Hidden;
            UnitsColumn.Visibility = Visibility.Visible;
            UnitLabel.Visibility = Visibility.Visible;
            UnitLbx.Visibility = Visibility.Visible;
            ChosenUnitLbx.Visibility = Visibility.Visible;
            ChosenUnitLbxLabel.Visibility = Visibility.Visible;

            //Actual usefull query
            SubFactionClass subfaction = SubFacCombo.SelectedItem as SubFactionClass;
            var query = from unit in DB.Units
                        where unit.SubFactionID.Equals(subfaction.SubfactionID)
                        select new UnitsClass()
                        {
                            UnitName = unit.Name,
                            UnitType = unit.UnitTypeID,
                            UnitValue = unit.UnitValue,
                            UnitImage = unit.UnitImage
                        };

            UnitLbx.ItemsSource = query.ToList();
            
        }

        //This is for when the combo box changes so i need to adjust the variable that keeps track of Max Points in the game
        private void PointCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedPoints = PointCombo.SelectedIndex;

            if (SelectedPoints == 0)
            {
                MaxPoints = 500;
            }
            else if(SelectedPoints == 1)
            {
                MaxPoints = 1000;
            }
            else if (SelectedPoints == 2)
            {
                MaxPoints = 2000;
            }
            else if (SelectedPoints == 3)
            {
                MaxPoints = 3000;
            }
        }
        //This is for filtering
        private void HQ_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void Troops_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Elites_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void FastAttack_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void HeavySupport_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void LordOfWar_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Transport_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Flyer_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Fortification_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
