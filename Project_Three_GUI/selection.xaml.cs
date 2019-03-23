using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Project_Three_GUI
{
    /// <summary>
    /// Interaction logic for selection.xaml
    /// </summary>
    public partial class selection : Page
    {
        public selection()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            frame3.Content = new newResident();
        }
        newResident page3 = new newResident();
        private void ButtonBase_Enter(object sender, RoutedEventArgs e)
        {

            if (StudentFirstName.ToString().Length >= 1 && StudentSurName.ToString().Length >= 1 && IDnum.ToString().Length >= 1 && (ScholarshipBox.IsChecked == true ^ AthleteBox.IsChecked == true ^ WorkerBox.IsChecked == true))
            {
                try
                {
                    int Rent = 0;
                    string type = "";
                    int ID = Convert.ToInt16(IDnum.Text);
                    string fullName = StudentFirstName.Text.ToString() + " " + StudentSurName.Text.ToString();
                    try
                    {
                        int hours = Convert.ToInt32(Hourly.Text);
                        int workingWage = 14 * hours;
                        if (WorkerBox.IsChecked == true)
                        {
                            Rent = 1245 - (workingWage / 2);
                            type = "WorkStudent";
                        }
                    }
                    catch (Exception exception)
                    {
                       
                    }
                    Residents addResidents = new Residents();
                    if (ScholarshipBox.IsChecked == true)
                    {
                        Rent = 100;
                        type = "Scholarship";
                    }
                    else if (AthleteBox.IsChecked == true)
                    {
                        Rent = 1200;
                        type = "Athlete";
                    }
                    else 
                    {
                        
                    }

                    try
                    {
                        JArray addJObject = new JArray(
                            new JProperty("ID", ID),
                            new JProperty("Name", fullName),
                            new JProperty("StudentType", type),
                            new JProperty("FloorNum", 1),
                            new JProperty("RoomNum", 6),
                            new JProperty("Rent", Rent));

                        File.WriteAllText(@"AddedResident.json", addJObject.ToString());

                        // write JSON directly to a file
                        using (StreamWriter file = File.CreateText(@"AddedResident.json"))
                        using (JsonTextWriter writer = new JsonTextWriter(file))
                        {
                            addJObject.WriteTo(writer);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                    //add Rent to JSON file
                    //return StudentName, id, and type to Json to be saved
                    frame3.Navigate(page3);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }


            }
        }

        private void ScholarshipBox_OnChecked(object sender, RoutedEventArgs e)
        {

            AthleteBox.Visibility = Visibility.Hidden;
            WorkerBox.Visibility = Visibility.Hidden;
        }

        private void AthleteBox_OnChecked(object sender, RoutedEventArgs e)
        {

            WorkerBox.Visibility = Visibility.Hidden;
            ScholarshipBox.Visibility = Visibility.Hidden;
        }

        private void WorkerBox_OnChecked(object sender, RoutedEventArgs e)
        {
 
            ScholarshipBox.Visibility = Visibility.Hidden;
            AthleteBox.Visibility = Visibility.Hidden;
            Hourly.Visibility = Visibility.Visible;
            hourlyDescription.Visibility = Visibility.Visible;

        }

        private void ScholarshipBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            AthleteBox.Visibility = Visibility.Visible;
            WorkerBox.Visibility = Visibility.Visible;
        }

        private void AthleteBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            WorkerBox.Visibility = Visibility.Visible;
            ScholarshipBox.Visibility = Visibility.Visible;
        }

        private void WorkerBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            ScholarshipBox.Visibility = Visibility.Visible;
            AthleteBox.Visibility = Visibility.Visible;
            Hourly.Visibility = Visibility.Hidden;
            hourlyDescription.Visibility = Visibility.Hidden;
        }
    }
}
