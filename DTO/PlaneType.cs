using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class PlaneType
    {
        public PlaneType(string type) { PlaneTypeName = type; }

        public long PlaneTypeID { get; set; }
        public string PlaneTypeName { get; set; }
        public List<Seat> Seats { get; set; }

        //Combo box feltöltése
        public static object CreateComboBox()
        {
            string[] strArray =
                {
                "Airbus A380",
                "Boeing 747",
                "Boeing 777",
                "Antonov 124",
                //További repülő típusok
            };
            return strArray;
        }
    }
}
