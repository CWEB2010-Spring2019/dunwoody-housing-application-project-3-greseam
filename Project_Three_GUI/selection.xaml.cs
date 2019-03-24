using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
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

        private bool invalidData = false;
        newResident page3 = new newResident();
        JArray canContainer = new JArray();
        private int floorVal = 0;
        string[,] Floors =
        {
            {"1","2","3"},
            {"4","5","6"},
            {"7", "8",""}
        };
        private int roomNumVal = 1;
        private void ButtonBase_Enter(object sender, RoutedEventArgs e)
        {

            if (StudentFirstName.ToString().Length >= 1 && StudentSurName.ToString().Length >= 1 && floorText.Text.Length == 1 &&
                IDnum.ToString().Length >= 1 && (ScholarshipBox.IsChecked == true ^ AthleteBox.IsChecked == true ^
                                                 WorkerBox.IsChecked == true ))
            {
                invalidData = false;

                if (ScholarshipBox.IsChecked == true)
                {
                    if (floorText.Text.ToString() == Floors[2,0] || floorText.Text.ToString() == Floors[2,1])
                    {
                        floorVal = Convert.ToInt32(floorText.Text.ToString());
                        roomNumVal++;
                    }
                    else
                    {
                        floorText.Text = "Invalid Floor";
                        invalidData = true;
                    }
                }
                else if (WorkerBox.IsChecked == true)
                {
                    if (floorText.Text.ToString() == Floors[0, 0] || floorText.Text.ToString() == Floors[0, 1] || floorText.Text.ToString() == Floors[0, 2])
                    {
                        floorVal = Convert.ToInt32(floorText.Text.ToString());
                        roomNumVal++;

                    }
                    else
                    {
                        floorText.Text = "Invalid Floor";
                        invalidData = true;
                    }
                }
                else if (AthleteBox.IsChecked == true)
                {
                    if (floorText.Text.ToString() == Floors[1, 0] || floorText.Text.ToString() == Floors[1, 1] || floorText.Text.ToString() == Floors[1, 2])
                    {
                        floorVal = Convert.ToInt32(floorText.Text.ToString());
                        roomNumVal++;

                    }
                    else
                    {
                        floorText.Text = "Invalid Floor";
                        invalidData = true;
                    }
                }
                else
                {
                    floorText.Text = "Invalid Floor";
                    invalidData = true;
                }
                string FilePath = @"AddedResident.json";
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
                catch 
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
                if (invalidData == false)
                {
                        TextWriter tsw = new StreamWriter(FilePath, true);
                   
                    JObject addJObject = new JObject(
                        new JProperty("ID", ID),
                        new JProperty("Name", fullName),
                        new JProperty("StudentType", type),
                        new JProperty("FloorNum", floorVal),
                        new JProperty("RoomNum", roomNumVal),
                        new JProperty("Rent", Rent));


                    canContainer.Add(addJObject);


                    using (JsonTextWriter writer = new JsonTextWriter(tsw))
                    {
                        canContainer.WriteTo(writer);

                        writer.Close();
                        tsw.Close();
                    }

                    try
                    {


                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }

                    //add Rent to JSON file
                    //return StudentName, id, and type to Json to be saved
                    
                        frame3.Content = new newResident();
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


        private void FloorText_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            floorText.Text = "";
        }

        private void IDnum_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            IDnum.Text = "";
        }

        private void StudentFirstName_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            StudentFirstName.Text = "";
        }

        private void StudentSurName_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            StudentSurName.Text = "";
        }
    }
}
