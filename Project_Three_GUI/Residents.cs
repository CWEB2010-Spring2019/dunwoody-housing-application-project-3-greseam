using System.Collections.Generic;

namespace Project_Three_GUI
{
    class Residents
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string StudentType { get; set; }
        public int FloorNum { get; set; }
        public int RoomNum { get; set; }
        public int Rent { get; set; }

        public Residents()
        {

        }
        public Residents(int ID, string Name, string StudentType, int FloorNum, int RoomNum, int Rent)
        {
            this.StudentType = StudentType;
            this.Name = Name;
            this.ID = ID;
            this.FloorNum = FloorNum;
            this.RoomNum = RoomNum;
            this.Rent = Rent;
        }
    }
    
   

}
