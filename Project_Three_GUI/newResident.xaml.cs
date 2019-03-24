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
using  Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Project_Three_GUI
{
    /// <summary>
    /// Interaction logic for newResident.xaml
    /// </summary>
    public partial class newResident : Page
    {
        List<Residents> residents = new List<Residents>();
        public newResident()
        {
            InitializeComponent();
            
            residents =  LoadCollectionData();
            HousingGrid.ItemsSource = residents;
            var studentTypeScholarship = from resident in residents
                where resident.StudentType == "Scholarship"
                select resident;
            foreach (var VARIABLE in studentTypeScholarship)
            {
                NumOfScholarships.Content =Convert.ToInt32(NumOfScholarships.Content.ToString()) + 1;
            }
            var studentTypeWorkers = from resident in residents
                where resident.StudentType == "WorkStudent"
                select resident;
            foreach (var VARIABLE in studentTypeWorkers)
            {
                NumOfWorkers.Content = Convert.ToInt32(NumOfWorkers.Content.ToString()) + 1;
            }
            var studentTypeAthletes = from resident in residents
                where resident.StudentType == "Athlete"
                select resident;
            foreach (var VARIABLE in studentTypeAthletes)
            {
                NumOfAtheletes.Content = Convert.ToInt32(NumOfAtheletes.Content.ToString()) + 1;
            }

            var ResidentOnFL1 = from resident in residents
                where resident.FloorNum == 1
                select resident;
            foreach (var VARIABLE in ResidentOnFL1)
            {
                NumOfResidentsFl1.Content = "FL1 : " + Convert.ToInt32(ResidentOnFL1.Count());
            }
            var ResidentOnFL2 = from resident in residents
                where resident.FloorNum == 2
                select resident;
            foreach (var VARIABLE in ResidentOnFL2)
            {
                NumOfResidentsFl2.Content = "FL2 : " + Convert.ToInt32(ResidentOnFL2.Count());
            }
            var ResidentOnFL3 = from resident in residents
                where resident.FloorNum == 3
                select resident;
            foreach (var VARIABLE in ResidentOnFL3)
            {
                NumOfResidentsFl3.Content = "FL3 : " + Convert.ToInt32(ResidentOnFL3.Count());
            }
            var ResidentOnFL4 = from resident in residents
                where resident.FloorNum == 4
                select resident;
            foreach (var VARIABLE in ResidentOnFL4)
            {
                NumOfResidentsFl4.Content = "FL4 : " + Convert.ToInt32(ResidentOnFL4.Count());
            }
            var ResidentOnFL5 = from resident in residents
                where resident.FloorNum == 5
                select resident;
            foreach (var VARIABLE in ResidentOnFL5)
            {
                NumOfResidentsFl5.Content = "FL5 : " + Convert.ToInt32(ResidentOnFL5.Count());
            }
            var ResidentOnFL6 = from resident in residents
                where resident.FloorNum == 6
                select resident;
            foreach (var VARIABLE in ResidentOnFL6)
            {
                NumOfResidentsFl6.Content = "FL6 : " + Convert.ToInt32(ResidentOnFL6.Count());
            }
            var ResidentOnFL7 = from resident in residents
                where resident.FloorNum == 7
                select resident;
            foreach (var VARIABLE in ResidentOnFL7)
            {
                NumOfResidentsFl7.Content = "FL7 : " + Convert.ToInt32(ResidentOnFL7.Count());
            }
            var ResidentOnFL8 = from resident in residents
                where resident.FloorNum == 8
                select resident;
            foreach (var VARIABLE in ResidentOnFL8)
            {
                NumOfResidentsFl1.Content = "FL8 : " + Convert.ToInt32(ResidentOnFL8.Count());
            }
        }

        private List<Residents> LoadCollectionData()
        {
            string FilePath = @"AddedResident.json";
            List<Residents> addedResidentsList = new List<Residents>();
            addedResidentsList =
                JsonConvert.DeserializeObject<List<Residents>>(File.ReadAllText(FilePath).Replace("][",","));
            List<Residents> ResidentsDataList = new List<Residents>();
            ResidentsDataList.Add(new Residents()
            {
                ID = 001,
                Name = "Larry L.",
                StudentType = "Athlete",
                FloorNum = 4,
                RoomNum = 1,
                Rent = 1200,
            });

            ResidentsDataList.Add(new Residents()
            {
                ID = 002,
                Name = "Mike Gold",
                StudentType = "Scholarship",
                FloorNum = 7,
                RoomNum = 1,
                Rent = 100,
            });

            ResidentsDataList.Add(new Residents()
            {
                ID = 003,
                Name = "Mathew Mannen",
                StudentType = "WorkStudent",
                FloorNum = 3,
                RoomNum = 1,
                Rent = 1245 - ((14*(20 * 3))/2),
            });

            try
            {

            
                for (int i = 0; i < addedResidentsList.Count; i++)
                {
                    ResidentsDataList.Add(new Residents()
                    {
                        ID = addedResidentsList[i].ID,
                        Name = addedResidentsList[i].Name,
                        StudentType = addedResidentsList[i].StudentType,
                        FloorNum = addedResidentsList[i].FloorNum,
                        RoomNum = addedResidentsList[i].RoomNum,
                        Rent = addedResidentsList[i].Rent,
                    });
                }
            }
            catch 
            {
               
            }


            return ResidentsDataList;
        }



        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

                frame2.Content = new selection();
            
        }

        private void IDSearch_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var IDSearchQuery = from resident in residents
                    where resident.ID == Convert.ToInt32(SearchTextBox.Text)
                    select resident;

                    HousingGrid.ItemsSource = IDSearchQuery;

            }
            catch 
            {
                SearchTextBox.Text = "Invalid ID";
            }
        }

        private void FloorSearch_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var FloorSearchQuery = from resident in residents
                    where resident.FloorNum == Convert.ToInt32(SearchTextBox.Text)
                    select resident;

                HousingGrid.ItemsSource = FloorSearchQuery;

            }
            catch
            {
                SearchTextBox.Text = "Invalid Floor";
            }
        }

        private void Search_Clear_OnClick(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "Search...";
            HousingGrid.ItemsSource = residents;
        }


        private void SearchTextBox_OnGotMouseCapture(object sender, MouseEventArgs e)
        {
            SearchTextBox.Text = " ";
        }
    }
}
