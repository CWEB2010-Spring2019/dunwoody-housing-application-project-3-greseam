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
        public newResident()
        {
            InitializeComponent();
            List<Residents> residents = new List<Residents>();
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
            foreach (var VARIABLE in studentTypeWorkers)
            {
                NumOfAtheletes.Content = Convert.ToInt32(NumOfAtheletes.Content.ToString()) + 1;
            }

        }

        private List<Residents> LoadCollectionData()
        {
            List<Residents> addedResidentsList = new List<Residents>();
            addedResidentsList =
                JsonConvert.DeserializeObject<List<Residents>>(File.ReadAllText(@"C:\Users\greseam\Documents\Visual Studio 2017\Projects\Adv. Programming\Project_003\Project_Three_GUI\bin\Debug\AddedResident.json"));
            List<Residents> ResidentsDataList = new List<Residents>();
            ResidentsDataList.Add(new Residents()
            {
                ID = 001,
                Name = "Linda Gregor",
                StudentType = "Athlete",
                FloorNum = 4,
                RoomNum = 2,
                Rent = 1200,
            });

            ResidentsDataList.Add(new Residents()
            {
                ID = 002,
                Name = "Mike Gold",
                StudentType = "Scholarship",
                FloorNum = 7,
                RoomNum = 5,
                Rent = 100,
            });

            ResidentsDataList.Add(new Residents()
            {
                ID = 003,
                Name = "Mathew Mannen",
                StudentType = "WorkStudent",
                FloorNum = 3,
                RoomNum = 4,
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        

            return ResidentsDataList;
        }



        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

                frame2.Content = new selection();
            
        }

    }
}
