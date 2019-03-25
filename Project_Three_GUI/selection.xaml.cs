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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            frame3.Content = new newResident();//moves to back to the previous page
        }

        private bool invalidData = false; //determines if resident information is vaild, if "invalidData" is true the "Enter" button will not move to the next dialog option
        newResident page3 = new newResident();
        JArray canContainer = new JArray();
        private int floorVal = 10;
        string[,] Floors =
        {
            {"1","2","3"},
            {"4","5","6"},
            {"7", "8",""}
        };
        string FilePath = @"AddedResident.json";// it should be included in project let me know if it is not
        List<Residents> addedResidentsList = new List<Residents>();
        private void ButtonBase_Enter(object sender, RoutedEventArgs e)
        {
            addedResidentsList = JsonConvert.DeserializeObject<List<Residents>>(File.ReadAllText(FilePath).Replace("][", ","));
            //the "replace" is important as the way i add to the json file is strange, this replace fixes issues,
            //DO NOT EDIT JSON FILE OUTSIDE OF PROGRAM//
            
            //runs code only if there is something inside the textboxes for data
            if (StudentFirstName.ToString().Length >= 1 && StudentSurName.ToString().Length >= 1 && floorText.Text.Length == 1 &&
                IDnum.ToString().Length >= 1 && (ScholarshipBox.IsChecked == true ^ AthleteBox.IsChecked == true ^
                                                 WorkerBox.IsChecked == true ))
            {
                invalidData = false;// set "false" incase the value is changed and never changed back
                //placejolder values, will be set to different values later
                int Rent = 0;
                string type = "";
                int ID = 0;
                int roomNumVal = 1;
                int IDCheckVar = 0;


                try//try so it can detect if "Convert" cant be run
                {

                    ID = Convert.ToInt16(IDnum.Text);
                    if (ID.ToString().Length > 3)//3 is used to prevent numbers greater in charater length of 3
                    {
                        IDnum.Text = "ID is too long";
                        invalidData = true;
                    }
                    for (int i = 0; i < addedResidentsList.Count; i++)
                    {
                        IDCheckVar++;
                        if (ID == addedResidentsList[i].ID)
                        {
                            IDnum.Text = "ID in use";
                            invalidData = true;
                        }
                    }
                }
                catch
                {
                    IDnum.Text = "Invalid ID";
                    invalidData = true;
                }
                //following if/else statements check which student type box is checked, and determins if its correct floor to student type 
                if (ScholarshipBox.IsChecked == true )//start
                {
                    if (floorText.Text.ToString() == Floors[2, 0] ^ floorText.Text.ToString() == Floors[2, 1])//bug where if a number is entered for floor it will ignore these but not displa
                    {
                        floorVal = Convert.ToInt32(floorText.Text.ToString());

                    }
                    else
                    {
                        floorText.Text = "Invalid Floor";
                        invalidData = true;
                    }
                }
                else if (WorkerBox.IsChecked == true)
                {
                    if (Hourly.Text.Length <= 0)
                    {
                        Hourly.Text = "Invalid Value";
                        invalidData = true;
                    }
                    if ((floorText.Text.ToString() == Floors[0, 0] ^ floorText.Text.ToString() == Floors[0, 1] ^ floorText.Text.ToString() == Floors[0, 2]) && Convert.ToInt32(floorText.Text.ToString()) <= 10 )
                    {
                        floorVal = Convert.ToInt32(floorText.Text.ToString());

                    }
                    else
                    {
                        floorText.Text = "Invalid Floor";
                        invalidData = true;
                    }
                }
                else if (AthleteBox.IsChecked == true)
                {
                    if (floorText.Text.ToString() == Floors[1, 0] ^ floorText.Text.ToString() == Floors[1, 1] ^ floorText.Text.ToString() == Floors[1, 2] && Convert.ToInt32(floorText.Text.ToString()) <= 10)
                    {
                        floorVal = Convert.ToInt32(floorText.Text.ToString());
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
                }//end 
            
                
                string fullName = StudentFirstName.Text.ToString() + " " + StudentSurName.Text.ToString();
                try
                {
                var RoomNumOnFL = from resident in addedResidentsList //counts number of residents based on floor and adds for each resident to get the next open room
                        where resident.FloorNum == floorVal
                        select resident;
                    foreach (var VARIABLE in RoomNumOnFL)
                    {
                        roomNumVal++;
                    }
                }
                catch 
                {
                    roomNumVal = 1;// set to one so it can handle no residents on floor
                }
                
                
                try //try so that it can handle cases for non workers
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
                        new JProperty("RoomNum",GenerateRoomNum(roomNumVal,floorVal)),
                        new JProperty("Rent", Rent));


                    canContainer.Add(addJObject);


                    using (JsonTextWriter writer = new JsonTextWriter(tsw))
                    {
                        canContainer.WriteTo(writer);

                        writer.Close();
                        tsw.Close();
                    }

                        frame3.Content = new newResident();
                }
                else
                {
                    AddResidentLabel.Visibility = Visibility.Visible; //label that specifies to enter all un awnsered in cases where user did not enter info
                }
                
            }
            else
            {
                AddResidentLabel.Visibility = Visibility.Visible;
            }
        }

        private int GenerateRoomNum(int room,int floor)
        {
            if (room.ToString().Length > 1)
            {
                room = Convert.ToInt32(Convert.ToString(floor) + Convert.ToString(room));
            }
            else
            {
                room = Convert.ToInt32(Convert.ToString(floor) + "0" + Convert.ToString(room));
            }

            return room;
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

        //following allows the text to reset when textbox is selected
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

        private void Frame3_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Hourly_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            Hourly.Text = "";
        }
    }
}
