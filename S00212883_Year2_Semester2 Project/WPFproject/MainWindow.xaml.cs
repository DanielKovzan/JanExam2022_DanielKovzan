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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace WPFProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// So my project, like, now that i feel like im done the project, i really could have done some fancy shit and 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        WarHammer40kDBEntities DB = new WarHammer40kDBEntities();
        public List<UnitsClass> ChosenUnits = new List<UnitsClass>();
        public UnitsClass LastSelectedUnit;
        public int FilteringID;
        public int MaxPoints = 500;
        public int CurrentUnitValueTotal = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        public SeriesCollection SeriesCollection { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public int ForPieChart(int UnitType)
        {
            int AmountOfUnits = 0;

            foreach (UnitsClass unit in ChosenUnits)
            {
                if(unit.UnitType == UnitType)
                {
                    AmountOfUnits++;
                }
            }

            return AmountOfUnits;
        }

        //God this was one very efficient method
        public void Filtering(int UnitType)
        {
            if (FilteringID == 0)
            {
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
                ChosenUnitLbx.ItemsSource = ChosenUnits;
            }
            else
            {
                SubFactionClass subfaction = SubFacCombo.SelectedItem as SubFactionClass;
                var query = from unit in DB.Units
                            where unit.SubFactionID.Equals(subfaction.SubfactionID) && unit.UnitTypeID.Equals(UnitType)
                            select new UnitsClass()
                            {
                                UnitName = unit.Name,
                                UnitType = unit.UnitTypeID,
                                UnitValue = unit.UnitValue,
                                UnitImage = unit.UnitImage
                            };
                UnitLbx.ItemsSource = query.ToList();

                List<UnitsClass> FilteredChosenUnits = new List<UnitsClass>();
                if (ChosenUnits.Count != 0)
                {
                    foreach (UnitsClass unit in ChosenUnits)
                    {
                        if (unit.UnitType == UnitType)
                        {
                            FilteredChosenUnits.Add(unit);
                        }
                    }
                }
                ChosenUnitLbx.ItemsSource = FilteredChosenUnits;
            }
        }

        //ComboBoxes
        #region
        //Choose a faction
        private void FacCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Unhiding and hiding stuff
            SubFacCombo.Visibility = Visibility.Visible;
            SubFacCombo.IsHitTestVisible = true;
            SubFacCombo.SelectedIndex = -1;
            SubFactionLabel.Visibility = Visibility.Visible;
            FactionLabel.Visibility = Visibility.Hidden;

            //Since we cant have have Cross - Faction armies, i will clear the ChosenUnits list and the ListBoxes
            ChosenUnits.Clear();
            SubFacCombo.Items.Clear();
            ChosenUnitLbx.ItemsSource = null;
            UnitLbx.ItemsSource = null;

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

        //Choose a SubFaction
        private void SubFacCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //revealing everything, im starting to hate this, genuinely
            #region
            SubFactionLabel.Visibility = Visibility.Hidden;
            UnitsColumn.Visibility = Visibility.Visible;
            UnitLabel.Visibility = Visibility.Visible;
            UnitLbxLabel.Visibility = Visibility.Visible;
            UnitLbx.Visibility = Visibility.Visible;
            ChosenUnitLbx.Visibility = Visibility.Visible;
            ChosenUnitLbxLabel.Visibility = Visibility.Visible;
            AddUnitBtn.Visibility = Visibility.Visible;
            RemoveUnitBtn.Visibility = Visibility.Visible;
            PointsTitleLabel.Visibility = Visibility.Visible;
            PointsNumberLabel.Visibility = Visibility.Visible;
            #endregion

            if(SubFacCombo.SelectedIndex != -1)
            {
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
            if (PointsNumberLabel != null)
            {
                PointsNumberLabel.Content = MaxPoints.ToString();
                ChosenUnits.Clear();
                ChosenUnitLbx.ItemsSource = null;
            }
        }
        #endregion

        //This is for filtering
        #region
        private void All_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 0;
            Filtering(FilteringID);
        }
        private void HQ_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 1;
            Filtering(1);
        }

        private void Troops_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 2;
            Filtering(2);
        }

        private void Elites_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 3;
            Filtering(3);
        }

        private void FastAttack_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 4;
            Filtering(4);
        }

        private void HeavySupport_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 5;
            Filtering(5);
        }

        private void LordOfWar_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 6;
            Filtering(6);
        }

        private void Transport_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 7;
            Filtering(7);
        }

        private void Flyer_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 8;
            Filtering(8);
        }

        private void Fortification_Checked(object sender, RoutedEventArgs e)
        {
            FilteringID = 9;
            Filtering(9);
        }
        #endregion

        //Buttons
        #region
        //Remove units from ChosenUntisLbx
        private void RemoveUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            
            UnitsClass SelectedUnit = ChosenUnitLbx.SelectedItem as UnitsClass;

            if(SelectedUnit != null)
            {
                ChosenUnits.Remove(SelectedUnit);

                ChosenUnitLbx.ItemsSource = null;
                ChosenUnits.Sort();
                ChosenUnitLbx.ItemsSource = ChosenUnits;

                Filtering(FilteringID);

                MaxPoints += Convert.ToInt32(SelectedUnit.UnitValue);
                PointsNumberLabel.Content = MaxPoints.ToString();
            }           
        }
        //Add units to ChosentUnitsLbx /  the army roster
        private void AddUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            UnitsClass SelectedUnit = UnitLbx.SelectedItem as UnitsClass;

            if (SelectedUnit != null)
            {
                if (MaxPoints - Convert.ToInt32(SelectedUnit.UnitValue) > 0)
                {
                    LastSelectedUnit = SelectedUnit;
                    ChosenUnits.Add(SelectedUnit);

                    //Must be emptied otherwise it bugs it out and doesnt display the untis
                    ChosenUnitLbx.ItemsSource = null;
                    ChosenUnits.Sort();
                    ChosenUnitLbx.ItemsSource = ChosenUnits;
                    

                    Filtering(FilteringID);

                    MaxPoints -= Convert.ToInt32(SelectedUnit.UnitValue);
                    PointsNumberLabel.Content = MaxPoints.ToString();
                }
                else
                {
                    MessageBox.Show("Insufficient Points");
                }

            }
        }
        #endregion

        private void PieButton_Click(object sender, RoutedEventArgs e)
        {
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "HQ",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(1))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Troops",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(2))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Elites",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(3))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Fast Attack",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(4))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Heavy Support",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(5))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Lord Of War",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(6))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Transport",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(7))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Flyer",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(8))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Fortification",
                    Values = new ChartValues<ObservableValue>{new ObservableValue(ForPieChart(9))},
                    DataLabels = true
                }
            };

            piechart.Series = SeriesCollection;
        }
    }
}


